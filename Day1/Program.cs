using System.Text.RegularExpressions;

namespace Day1;

internal partial class Program
{
    public static void Main()
    {
        var lines = File.ReadAllLines("../../../input.txt").ToList();

        var sum = lines
            .Select(line => NumRegex()
                .Matches(line)
                .Select(match => match.Groups[1].Value switch
                {
                    "one" => 1,
                    "two" => 2,
                    "three" => 3,
                    "four" => 4,
                    "five" => 5,
                    "six" => 6,
                    "seven" => 7,
                    "eight" => 8,
                    "nine" => 9,
                    _ => int.Parse(match.Groups[1].Value)
                })
                .ToList())
            .Select(numList => int.Parse("" + numList.First() + numList.Last()))
            .Sum();

        Console.WriteLine(sum);
    }

    [GeneratedRegex("(?=(one|two|three|four|five|six|seven|eight|nine|\\d))")]
    private static partial Regex NumRegex();
}