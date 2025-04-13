namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Rating
{
    public decimal Rate { get; private set; }
    public int Count { get; private set; }

    public Rating(decimal rate, int count)
    {
        Rate = rate;
        Count = count;
    } 
}