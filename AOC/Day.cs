namespace AOC;

public abstract class Day
{
    public int DayNumber => int.Parse(GetType().Name[3..]);
    protected string FileInput => $"../../../inputs/day{DayNumber}.txt";

    public abstract string Part1();
    public abstract string Part2();

    public static Day GetDay(int day)
    {
        Type dayType = Type.GetType($"AOC.Days.Day{day}") ??
                       throw new ArgumentOutOfRangeException(nameof(day), "Day not implemented or out of range");

        return (Day)Activator.CreateInstance(dayType)!;
    }
}