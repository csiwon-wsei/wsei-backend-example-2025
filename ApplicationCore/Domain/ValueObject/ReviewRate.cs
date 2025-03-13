namespace ApplicationCore.Domain.ValueObject;

public record ReviewRate
{
    public const uint Max = 10;
    private readonly uint _rate;
    public uint Value => _rate;
    private ReviewRate(uint points)
    {
        _rate = points;
    }

    public static ReviewRate Of(uint points)
    {
        if (points > Max) throw new ArgumentOutOfRangeException(nameof(points));
        return new ReviewRate(points);
    }
    
    public static  implicit operator uint(ReviewRate rate)
    {
        return rate.Value;
    }
    
    public static  implicit operator decimal(ReviewRate rate)
    {
        return rate.Value;
    }
    
};