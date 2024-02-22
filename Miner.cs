using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Util;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Channels;

namespace HuffPlugMiner;

public static class Miner {
	private static readonly TimeSpan frecuency = TimeSpan.FromSeconds(1);

	public static Task<(string, BigInteger)>[] MineSequential(string difficultyString, byte[] dataBytes, ChannelWriter<(int, string)> consoleChannel, int threads, CancellationToken cancellationToken) {
		Task<(string, BigInteger)>[] tasks = new Task<(string, BigInteger)>[threads];
		for (int i = 0; i < threads; i++) {
			int index = i;
			INextValue initialNonce = new SequentialNonce(new BigInteger(ulong.MaxValue) * index);
			tasks[i] = Task.Run(() => Mine(difficultyString, dataBytes, initialNonce, index, consoleChannel, cancellationToken));
		}

		return tasks;
	}

	public static Task<(string, BigInteger)>[] MineRandom(string difficultyString, byte[] dataBytes, ChannelWriter<(int, string)> consoleChannel, int threads, CancellationToken cancellationToken) {
		Task<(string, BigInteger)>[] tasks = new Task<(string, BigInteger)>[threads];
		for (int i = 0; i < threads; i++) {
			int index = i;
			INextValue initialNonce = new RandomNonce();
			tasks[i] = Task.Run(() => Mine(difficultyString, dataBytes, initialNonce, index, consoleChannel, cancellationToken));
		}

		return tasks;
	}

	private static (string, BigInteger) Mine(string difficultyString, byte[] baseData, INextValue nextValue, int index, ChannelWriter<(int, string)> writer, CancellationToken token) {
		Sha3Keccack sha3Keccack = new();
		string hash = string.Empty;

		byte[] bytes = new byte[baseData.Length + 32];
		baseData.CopyTo(bytes, 0);
		long cycleStart = Stopwatch.GetTimestamp();
		BigInteger nonce = 0;
		ulong cycles = 0;
		while (!token.IsCancellationRequested) {
			nonce = nextValue.FillNext(bytes);
			hash = sha3Keccack.CalculateHash(bytes).ToHex();

			if (hash.StartsWith(difficultyString)) {
				break;
			}

			if (token.IsCancellationRequested) {
				return (string.Empty, 0);
			}

			TimeSpan elapsed = Stopwatch.GetElapsedTime(cycleStart);
			cycles++;
			if (elapsed > frecuency) {
				writer.TryWrite((index, $"Task: {index,3} hash: 0x{hash}   nonce: {nonce,80}  {cycles / elapsed.TotalSeconds / 1000,10:N2} kop/s"));
				cycleStart = Stopwatch.GetTimestamp();
				cycles = 0;
			}
		}

		return (hash, nonce);
	}

	interface INextValue {
		BigInteger FillNext(byte[] data);
	}

	private class SequentialNonce(BigInteger nonce) : INextValue {
		private BigInteger nonce = nonce;

		public BigInteger FillNext(byte[] data) {
			byte[] newData = nonce.ToByteArray(false, true);
			newData.CopyTo(data, data.Length - newData.Length);
			return nonce++;
		}
	}

	private class RandomNonce() : INextValue {
		private readonly Random random = new();

		public BigInteger FillNext(byte[] data) {
			Span<byte> nonce = data.AsSpan()[^32..];
			random.NextBytes(nonce);
			return new BigInteger(nonce, true, true);
		}
	}
}


