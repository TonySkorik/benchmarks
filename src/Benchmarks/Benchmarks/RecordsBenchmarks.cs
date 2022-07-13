using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using Benchmarks.Model;

namespace Benchmarks.Benchmarks;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser(displayGenColumns: true)]
//[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
//[CategoriesColumn]
public class RecordsBenchmarks
{
	private readonly Random _random = new(1567);

	[GlobalSetup]
	public void Setup()
	{

	}

	[Benchmark(Baseline = true)]
	[ArgumentsSource(nameof(Ints))]
	public void InsertIntKeys(KeyValuePair<int, string>[] valuesToInsert)
	{
		var dictionary = new Dictionary<int, HashSet<string>>();

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
	[ArgumentsSource(nameof(Strings))]
	public void InsertStringKeys(KeyValuePair<string, string>[] valuesToInsert)
	{
		var dictionary = new Dictionary<string, HashSet<string>>();

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
		//yield return Enumerable.Range(1, 1)
		//	.Select(
		//		_ => (
		//			new RecordWithEqualityMembers() {
		//				A = new string((char)_random.Next(65, 69), 3),
		//				B = new string((char)_random.Next(65, 69), 3),
		//			},
		//			new string((char)_random.Next(65, 69), 3))
		//	).ToArray();

		//yield return Enumerable.Range(1, 100)
		//	.Select(
		//		_ => (
		//			new RecordWithEqualityMembers() {
		//				A = new string((char)_random.Next(65, 69), 3),
		//				B = new string((char)_random.Next(65, 69), 3),
		//			},
		//			new string((char)_random.Next(65, 69), 3))
		//	).ToArray();

		yield return Enumerable.Range(1, 1_000)
			.Select(
				_ => (
					new RecordWithEqualityMembers() {
						A = new string((char)_random.Next(65, 69), 3),
						B = new string((char)_random.Next(65, 69), 3),
					},
					new string((char)_random.Next(65, 69), 3))
			).ToArray();

		//yield return Enumerable.Range(1, 10_000)
		//	.Select(
		//		_ => (
		//			new RecordWithEqualityMembers() {
		//				A = new string((char)_random.Next(65, 69), 3),
		//				B = new string((char)_random.Next(65, 69), 3),
		//			},
		//			new string((char)_random.Next(65, 69), 3))
		//	).ToArray();

		//yield return Enumerable.Range(1, 100_000)
		//	.Select(
		//		_ => (
		//			new RecordWithEqualityMembers() {
		//				A = new string((char)_random.Next(65, 69), 3),
		//				B = new string((char)_random.Next(65, 69), 3),
		//			},
		//			new string((char)_random.Next(65, 69), 3))
		//	).ToArray();
	}

	public IEnumerable<(RecordWithoutEqualityMembers Key, string Value)[]> RecordsWithoutEqualityMembers()
	{
		//yield return Enumerable.Range(1, 1)
		//	.Select(
		//		_ => (
		//			new RecordWithoutEqualityMembers()
		//			{
		//				A = new string((char) _random.Next(65, 69), 3),
		//				B = new string((char) _random.Next(65, 69), 3),
		//			},
		//			new string((char) _random.Next(65, 69), 3))
		//	).ToArray();

		//yield return Enumerable.Range(1, 100)
		//	.Select(
		//		_ => (
		//			new RecordWithoutEqualityMembers() {
		//				A = new string((char)_random.Next(65, 69), 3),
		//				B = new string((char)_random.Next(65, 69), 3),
		//			},
		//			new string((char)_random.Next(65, 69), 3))
		//	).ToArray();

		yield return Enumerable.Range(1, 1_000)
			.Select(
				_ => (
					new RecordWithoutEqualityMembers() {
						A = new string((char)_random.Next(65, 69), 3),
						B = new string((char)_random.Next(65, 69), 3),
					},
					new string((char)_random.Next(65, 69), 3))
			).ToArray();

		//yield return Enumerable.Range(1, 10_000)
		//	.Select(
		//		_ => (
		//			new RecordWithoutEqualityMembers() {
		//				A = new string((char)_random.Next(65, 69), 3),
		//				B = new string((char)_random.Next(65, 69), 3),
		//			},
		//			new string((char)_random.Next(65, 69), 3))
		//	).ToArray();

		//yield return Enumerable.Range(1, 100_000)
		//	.Select(
		//		_ => (
		//			new RecordWithoutEqualityMembers() {
		//				A = new string((char)_random.Next(65, 69), 3),
		//				B = new string((char)_random.Next(65, 69), 3),
		//			},
		//			new string((char)_random.Next(65, 69), 3))
		//	).ToArray();
	}

	public IEnumerable<KeyValuePair<string, string>[]> Strings()
	{
		//yield return Enumerable.Range(1, 1)
		//	.Select(
		//		_ => new KeyValuePair<string, string>(
		//			new string((char) _random.Next(65, 69), 3),
		//			Guid.NewGuid().ToString())).ToArray();

		//yield return Enumerable.Range(1, 100)
		//	.Select(
		//		_ => new KeyValuePair<string, string>(
		//			new string((char)_random.Next(65, 69), 3),
		//			Guid.NewGuid().ToString())).ToArray();

		yield return Enumerable.Range(1, 1_000)
			.Select(
				_ => new KeyValuePair<string, string>(
					new string((char)_random.Next(65, 69), 3),
					Guid.NewGuid().ToString())).ToArray();
		
		//yield return Enumerable.Range(1, 10_000)
		//	.Select(
		//		_ => new KeyValuePair<string, string>(
		//			new string((char)_random.Next(65, 69), 3),
		//			Guid.NewGuid().ToString())).ToArray();

		//yield return Enumerable.Range(1, 100_000)
		//	.Select(
		//		_ => new KeyValuePair<string, string>(
		//			new string((char)_random.Next(65, 69), 3),
		//			Guid.NewGuid().ToString())).ToArray();
	}

	public IEnumerable<KeyValuePair<int, string>[]> Ints()
	{
		//yield return Enumerable.Range(1, 1)
		//	.Select(_ => new KeyValuePair<int, string>(_random.Next(0, 5), Guid.NewGuid().ToString())).ToArray();

		//yield return Enumerable.Range(1, 100)
		//	.Select(_ => new KeyValuePair<int, string>(_random.Next(0, 5), Guid.NewGuid().ToString())).ToArray();

		yield return Enumerable.Range(1, 1_000)
			.Select(_ => new KeyValuePair<int, string>(_random.Next(0, 5), Guid.NewGuid().ToString())).ToArray();

		//yield return Enumerable.Range(1, 10_000)
		//	.Select(_ => new KeyValuePair<int, string>(_random.Next(0, 5), Guid.NewGuid().ToString())).ToArray();

		//yield return Enumerable.Range(1, 100_000)
		//	.Select(_ => new KeyValuePair<int, string>(_random.Next(0, 5), Guid.NewGuid().ToString())).ToArray();
	}
}