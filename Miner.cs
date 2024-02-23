using Nethereum.Hex.HexConvertors.Extensions;
using Org.BouncyCastle.Crypto.Digests;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Channels;

namespace HuffPlugMiner;

public static class Miner {
	private static readonly TimeSpan frecuency = TimeSpan.FromSeconds(1);
	private static ulong totalCycles = 0;

	public static Task<(string, BigInteger)>[] MineSequential(string difficultyString, byte[] dataBytes, ChannelWriter<(int, string)> consoleWriter, int threads, CancellationToken cancellationToken) {
		Task<(string, BigInteger)>[] tasks = new Task<(string, BigInteger)>[threads];
		for (int i = 0; i < threads; i++) {
			int index = i;
			INextValue initialNonce = new SequentialNonce(new BigInteger(ulong.MaxValue) * index);
			tasks[i] = Task.Run(() => Mine(difficultyString, dataBytes, initialNonce, index, consoleWriter, cancellationToken));
		}


		_ = DisplayTotalCyclesAsync(consoleWriter, cancellationToken);

		return tasks;
	}

	public static Task<(string, BigInteger)>[] MineRandom(string difficultyString, byte[] dataBytes, ChannelWriter<(int, string)> consoleWriter, int threads, CancellationToken cancellationToken) {
		Task<(string, BigInteger)>[] tasks = new Task<(string, BigInteger)>[threads];
		for (int i = 0; i < threads; i++) {
			int index = i;
			INextValue initialNonce = new RandomNonce();
			tasks[i] = Task.Run(() => Mine(difficultyString, dataBytes, initialNonce, index, consoleWriter, cancellationToken));
		}

		_ = DisplayTotalCyclesAsync(consoleWriter, cancellationToken);

		return tasks;
	}

	private static async Task DisplayTotalCyclesAsync(ChannelWriter<(int, string)> consoleWriter, CancellationToken cancellationToken) {
		int pos = Environment.ProcessorCount + 1;
		while (!cancellationToken.IsCancellationRequested) {
			await Task.Delay(1000, CancellationToken.None);
			consoleWriter.TryWrite((pos, $"Total: {totalCycles / 1000f,10:N2} kop/s"));
			Interlocked.Exchange(ref totalCycles, 0);
		}
	}

	//	address user = 0xD3C7b59402848Ad572C2379Cf32F4B7ff4a4f4f0
	//	bytes32 salt = 0xccdb3470ba804f6ee0dc236767040a16711c5c7e3bd8a989a04dc64f297bd9b0
	//	uint256 nonce = 64951574175560012347877612999036161923027079464199987187540833584290081260959 
	//  keccak256(abi.encodePacked(user,salt,nonce))
	//	hash: 0xfe8ae584dab896f54a78680e3b3de8a700d3200c96dd1a24c40a8183b55d70e2

	private static (string, BigInteger) Mine(string difficultyString, byte[] baseData, INextValue nextValue, int index, ChannelWriter<(int, string)> writer, CancellationToken cancellationToken) {
		KeccakDigest keccakDigest = new(256);
		byte[] buffer = new byte[keccakDigest.GetDigestSize()];
		string hash = string.Empty;

		byte[] bytes = new byte[baseData.Length + 32];
		baseData.CopyTo(bytes, 0);
		long cycleStart = Stopwatch.GetTimestamp();
		BigInteger nonce = 0;
		ulong cycles = 0;
		while (!cancellationToken.IsCancellationRequested) {
			nonce = nextValue.FillNext(bytes);
			keccakDigest.BlockUpdate(bytes, 0, bytes.Length);
			keccakDigest.DoFinal(buffer, 0);
			hash = buffer.ToHex();

			if (hash.StartsWith(difficultyString)) {
				break;
			}

			if (cancellationToken.IsCancellationRequested) {
				return (string.Empty, 0);
			}

			TimeSpan elapsed = Stopwatch.GetElapsedTime(cycleStart);
			cycles++;
			Interlocked.Increment(ref totalCycles);
			if (elapsed > frecuency) {
				writer.TryWrite((index, $"Task: {index,3} hash: 0x{hash}   nonce: {nonce,80}  {cycles / elapsed.TotalSeconds / 1000,10:N2} kop/s"));
				cycleStart = Stopwatch.GetTimestamp();
				cycles = 0;
			}
			keccakDigest.Reset();
		}

		return (hash, nonce);
	}

	interface INextValue {
		BigInteger FillNext(Span<byte> data);
	}

	private class SequentialNonce(BigInteger nonce) : INextValue {
		private BigInteger nonce = nonce;

		public BigInteger FillNext(Span<byte> data) {
			byte[] newData = nonce.ToByteArray(false, true);
			newData.CopyTo(data[(data.Length - newData.Length)..]);
			return nonce++;
		}
	}

	private class RandomNonce() : INextValue {
		private readonly Random random = new();

		public BigInteger FillNext(Span<byte> data) {
			Span<byte> nonce = data[^32..];
			random.NextBytes(nonce);
			return new BigInteger(nonce, true, true);
		}
	}
}


