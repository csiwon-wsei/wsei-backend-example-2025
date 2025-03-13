namespace ApplicationCore.Application.Commons;

public abstract class BaseIdentity: IComparable<BaseIdentity>
{
    public Guid Id { get; set; }

    public int CompareTo(BaseIdentity? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (other is null) return 1;
        return Id.CompareTo(other.Id);
    }

    protected bool Equals(BaseIdentity other)
    {
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BaseIdentity)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}