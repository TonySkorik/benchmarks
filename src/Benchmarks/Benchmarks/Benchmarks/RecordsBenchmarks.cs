using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.Benchmarks;

[SimpleJob(RuntimeMoniker.Net60)]
[MemoryDiagnoser(displayGenColumns: true)]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class RecordsBenchmarks
{
	[GlobalSetup]
	public void Setup()
	{

	}

	[Benchmark]
	[ArgumentsSource(nameof(ShardKeys))]
	public async Task ResolveShard(string[] shardKeyValues)
	{
		foreach (var key in shardKeyValues)
		{
			
		}
	}

	public IEnumerable<object[]> ShardKeys()
	{
		yield return Enumerable.Range(1, 1).Select(v => v.ToString()).ToArray();
		yield return Enumerable.Range(1, 100).Select(v => v.ToString()).ToArray();
		yield return Enumerable.Range(1, 1_000).Select(v => v.ToString()).ToArray();
		yield return Enumerable.Range(1, 10_000).Select(v => v.ToString()).ToArray();
		yield return Enumerable.Range(1, 100_000).Select(v => v.ToString()).ToArray();
	}
}