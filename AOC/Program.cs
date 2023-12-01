namespace AOC;

internal class Program
{
    public static void Main()
    {
        try
        {
            Console.Write("Day: ");

            var day = Day.GetDay(int.Parse(Console.ReadLine()!));
            
            Console.WriteLine();

            Console.WriteLine($"Part 1: {day.Part1()}");
            Console.WriteLine($"Part 2: {day.Part2()}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Day not implemented or out of range");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}