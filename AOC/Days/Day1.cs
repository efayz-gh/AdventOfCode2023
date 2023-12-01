using System.Text.RegularExpressions;

namespace AOC.Days;

internal partial class Day1 : Day
{
    public override string Part1()
    {
        var lines = File.ReadAllLines(FileInput).ToList();

        return lines
            .Select(line => string.Concat(line
                .Where(char.IsDigit)
                .Select(c => int.Parse(c.ToString()))))
            .Select(numList => int.Parse("" + numList.First() + numList.Last()))
            .Sum()
            .ToString();
    }

    public override string Part2()
    {
        var lines = File.ReadAllLines(FileInput).ToList();

        return lines
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
            .Sum()
            .ToString();
    }

    [GeneratedRegex("(?=(one|two|three|four|five|six|seven|eight|nine|\\d))")]
    private static partial Regex NumRegex();
}