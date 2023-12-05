using System.Diagnostics;

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

            var stopwatch = new Stopwatch();
            
            Console.WriteLine($"Part 1: {day.Part1()} (Time: {stopwatch.Elapsed})");
            stopwatch.Restart();
            
            Console.WriteLine($"Part 2: {day.Part2()} (Time: {stopwatch.Elapsed})");
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