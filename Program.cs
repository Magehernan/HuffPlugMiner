using HuffPlugMiner;
using HuffPlugMiner.IHuffplug;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Web3;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Channels;

Console.Clear();

IWeb3 web3 = new Web3("https://eth.llamarpc.com");
IHuffplugService buttplugService = new(web3, "0x0000420538CD5AbfBC7Db219B6A1d125f5892Ab0");
BigInteger diff = await buttplugService.CurrentDifficultyQueryAsync();

int difficulty = (int)diff;
if (args.Length == 0) {
	Console.WriteLine("Pass the address as first parameter to generate hashes");
	return;
}
string user = args[0];
if (user.StartsWith("0x")) {
	user = user[2..];
}

string salt = (await buttplugService.SaltQueryAsync()).ToHex();
string difficultyString = Enumerable.Repeat('0', difficulty).ToArray().AsSpan().ToString();

Console.WriteLine($"User: 0x{user}\nSalt: 0x{salt}\nDifficulty: {difficulty}\nStart Mining");
byte[] baseData = $"{user}{salt}".HexToByteArray();

CancellationTokenSource cancellationTokenSource = new();
Channel<(int, string)> consoleLogger = Channel.CreateUnbounded<(int, string)>(new() { SingleReader = true });
_ = ProcessConsoleAsync(consoleLogger.Reader, cancellationTokenSource.Token);

int taskAmount = Environment.ProcessorCount;
Task<(string, BigInteger)>[] tasks = Miner.MineRandom(difficultyString, baseData, consoleLogger.Writer, taskAmount, cancellationTokenSource.Token);

long start = Stopwatch.GetTimestamp();
Task<(string, BigInteger)> finishTask = await Task.WhenAny(tasks);
cancellationTokenSource.Cancel();
(string hash, BigInteger nonce) = await finishTask;

Console.CursorLeft = 0;
Console.CursorTop = 20;
Console.WriteLine($"Elapsed time {Stopwatch.GetElapsedTime(start).TotalMinutes:N2}");
Console.WriteLine("--------------------------------------------------------------------------");
Console.WriteLine($"Winner Hash: 0x{hash}   -   nonce: {nonce}");

static async Task ProcessConsoleAsync(ChannelReader<(int, string)> reader, CancellationToken token) {
	Console.CursorVisible = false;
	await foreach ((int index, string text) in reader.ReadAllAsync(CancellationToken.None)) {
		if (token.IsCancellationRequested) {
			return;
		}
		Console.CursorLeft = 0;
		Console.CursorTop = index + 5;
		int bufferWidth = Console.BufferWidth;
		if (text.Length > bufferWidth) {
			Console.WriteLine(text[..bufferWidth]);
			continue;
		}

		Console.WriteLine(text.PadRight(bufferWidth));
	}
}
