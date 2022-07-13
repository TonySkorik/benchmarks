namespace Benchmarks.Model;

public record RecordWithEqualityMembers
{
	public string A { get; init; }

	public string B { get; init; }

	public virtual bool Equals(RecordWithEqualityMembers? other)
	{
		if (ReferenceEquals(null, other))
		{
			return false;
		}

		if (ReferenceEquals(this, other))
		{
			return true;
		}

		return A == other.A && B == other.B;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(A, B);
	}
}
