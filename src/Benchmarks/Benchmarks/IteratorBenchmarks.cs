using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmarks.Benchmarks;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
public class IteratorBenchmarks
{
	// check whether the iterator over KeyValuePair values allocate a KeyValuePair copy on each iteration

	private readonly Random _random = new(1567);

	[Benchmark(Baseline = true)]
	[ArgumentsSource(nameof(Ints))]
	public void IterateOverInts(int[] arrayToIterateOver)
	{
		foreach (var i in arrayToIterateOver)
		{ }
	}

	[Benchmark]
	[ArgumentsSource(nameof(KeyValuePairs))]
	public void IterateOverKeyValuePairs(KeyValuePair<string, string>[] arrayToIterateOver)
	{
		var dictionary = new Dictionary<string, HashSet<string>>();

		foreach (var kv in arrayToIterateOver)
		{
			if (!dictionary.ContainsKey(kv.Key))
			{
				dictionary.TryAdd(kv.Key, new());
			}

			dictionary[kv.Key].Add("TestValue");
		}
	}

	public IEnumerable<KeyValuePair<string, string>[]> KeyValuePairs()
	{
		yield return Enumerable.Range(1, 10_000)
			.Select(
				_ => new KeyValuePair<string, string>(
					new string((char)_random.Next(65, 69), 3),
					Guid.NewGuid().ToString())).ToArray();

		yield return Enumerable.Range(1, 100_000)
			.Select(
				_ => new KeyValuePair<string, string>(
					new string((char)_random.Next(65, 69), 3),
					Guid.NewGuid().ToString())).ToArray();

		yield return Enumerable.Range(1, 1_000_000)
			.Select(
				_ => new KeyValuePair<string, string>(
					new string((char)_random.Next(65, 69), 3),
					Guid.NewGuid().ToString())).ToArray();
	}

	public IEnumerable<int[]> Ints()
	{
		yield return Enumerable.Range(1, 10_000)
			.Select(_ => _random.Next(0, 5)).ToArray();
	}
}