using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using Benchmarks.Model;

namespace Benchmarks.Benchmarks;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser]
public class RecordsBenchmarks
{
	private readonly Random _random = new(1567);

	[GlobalSetup]
	public void Setup()
	{
		
	}

	[Benchmark(Baseline = true)]
	[ArgumentsSource(nameof(Ints))]
	public void InsertIntKeys(int[] keysToInsert)
	{
		var dictionary = new Dictionary<int, HashSet<string>>();

		foreach (var key in keysToInsert)
		{
			if (!dictionary.ContainsKey(key))
			{
				dictionary.TryAdd(key, new());
			}

			dictionary[key].Add("TestValue");
		}
	}

	[Benchmark]
	[ArgumentsSource(nameof(Strings))]
	public void InsertStringKeys(string[] keysToInsert)
	{
		var dictionary = new Dictionary<string, HashSet<string>>();

		foreach (var key in keysToInsert)
		{
			if (!dictionary.ContainsKey(key))
			{
				dictionary.TryAdd(key, new());
			}

			dictionary[key].Add("TestValue");
		}
	}

	[Benchmark]
	[ArgumentsSource(nameof(RecordsWithoutEqualityMembers))]
	public void InsertRecordsWithoutEqualityMembersKeys((RecordWithoutEqualityMembers Key, string Value)[] valuesToInsert)
	{
		var dictionary = new Dictionary<RecordWithoutEqualityMembers, HashSet<string>>();

		foreach (var kv in valuesToInsert)
		{
			if (!dictionary.ContainsKey(kv.Key))
			{
				dictionary.TryAdd(kv.Key, new());
			}

			dictionary[kv.Key].Add(kv.Value);
		}
	}

	[Benchmark]
	[ArgumentsSource(nameof(RecordsWithEqualityMembers))]
	public void InsertRecordsWithEqualityMembersKeys((RecordWithEqualityMembers Key, string Value)[] valuesToInsert)
	{
		var dictionary = new Dictionary<RecordWithEqualityMembers, HashSet<string>>();

		foreach (var kv in valuesToInsert)
		{
			if (!dictionary.ContainsKey(kv.Key))
			{
				dictionary.TryAdd(kv.Key, new());
			}

			dictionary[kv.Key].Add(kv.Value);
		}
	}

	public IEnumerable<(RecordWithEqualityMembers Key, string Value)[]> RecordsWithEqualityMembers()
	{
		yield return Enumerable.Range(1, 1_000)
			.Select(
				_ => (
					new RecordWithEqualityMembers() {
						A = new string((char)_random.Next(65, 69), 3),
						B = new string((char)_random.Next(65, 69), 3),
					},
					new string((char)_random.Next(65, 69), 3))
			).ToArray();

		yield return Enumerable.Range(1, 10_000)
			.Select(
				_ => (
					new RecordWithEqualityMembers() {
						A = new string((char)_random.Next(65, 69), 3),
						B = new string((char)_random.Next(65, 69), 3),
					},
					new string((char)_random.Next(65, 69), 3))
			).ToArray();

		yield return Enumerable.Range(1, 100_000)
			.Select(
				_ => (
					new RecordWithEqualityMembers() {
						A = new string((char)_random.Next(65, 69), 3),
						B = new string((char)_random.Next(65, 69), 3),
					},
					new string((char)_random.Next(65, 69), 3))
			).ToArray();
	}

	public IEnumerable<(RecordWithoutEqualityMembers Key, string Value)[]> RecordsWithoutEqualityMembers()
	{
		yield return Enumerable.Range(1, 1_000)
			.Select(
				_ => (
					new RecordWithoutEqualityMembers() {
						A = new string((char)_random.Next(65, 69), 3),
						B = new string((char)_random.Next(65, 69), 3),
					},
					new string((char)_random.Next(65, 69), 3))
			).ToArray();

		yield return Enumerable.Range(1, 10_000)
			.Select(
				_ => (
					new RecordWithoutEqualityMembers() {
						A = new string((char)_random.Next(65, 69), 3),
						B = new string((char)_random.Next(65, 69), 3),
					},
					new string((char)_random.Next(65, 69), 3))
			).ToArray();

		yield return Enumerable.Range(1, 100_000)
			.Select(
				_ => (
					new RecordWithoutEqualityMembers() {
						A = new string((char)_random.Next(65, 69), 3),
						B = new string((char)_random.Next(65, 69), 3),
					},
					new string((char)_random.Next(65, 69), 3))
			).ToArray();
	}

	public IEnumerable<string[]> Strings()
	{
		yield return Enumerable.Range(1, 1_000)
			.Select(
				_ => new string((char)_random.Next(65, 69), 3)).ToArray();

		yield return Enumerable.Range(1, 10_000)
			.Select(
				_ => new string((char)_random.Next(65, 69), 3)).ToArray();

		yield return Enumerable.Range(1, 100_000)
			.Select(
				_ => new string((char)_random.Next(65, 69), 3)).ToArray();
	}

	public IEnumerable<int[]> Ints()
	{
		yield return Enumerable.Range(1, 1_000)
			.Select(_ => _random.Next(0, 5)).ToArray();

		yield return Enumerable.Range(1, 10_000)
			.Select(_ => _random.Next(0, 5)).ToArray();

		yield return Enumerable.Range(1, 100_000)
			.Select(_ => _random.Next(0, 5)).ToArray();
	}
}