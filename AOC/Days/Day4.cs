using System.Text.RegularExpressions;

namespace AOC.Days;

public class Day4 : Day
{
    public override string Part1()
    {
        var input = File.ReadAllLines(FileInput);

        var sum = 0;
        foreach (var line in input)
        {
            var numbers = Regex.Matches(line.Split('|')[0].Split(':')[1], @"\d+")
                .Select(match => int.Parse(match.Value));
            var winning = Regex.Matches(line.Split('|')[1], @"\d+").Select(match => int.Parse(match.Value));

            var common = numbers.Intersect(winning).Count();
            if (common > 0)
                sum += 1 << (common - 1);
        }

        return sum.ToString();
    }

    public override string Part2()
    {
        var input = File.ReadAllLines(FileInput);

        var copies = input.Select(_ => 1).ToList();
        
        for (var i = 0; i < input.Length; i++)
        {
            var numbers = Regex.Matches(input[i].Split('|')[0].Split(':')[1], @"\d+")
                .Select(match => int.Parse(match.Value));
            var winning = Regex.Matches(input[i].Split('|')[1], @"\d+")
                .Select(match => int.Parse(match.Value));

            var common = numbers.Intersect(winning).Count();
            for (var j = 1; j <= common; j++)
                copies[i + j] += copies[i];
        }

        return copies.Sum().ToString();
    }
}