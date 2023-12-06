using System.Text.RegularExpressions;

namespace AOC.Days;

public class Day6 : Day
{
    public override string Part1()
    {
        var input = File.ReadAllLines(FileInput);

        var times = Regex.Matches(input[0], @"\d+").Select(match => int.Parse(match.Value));
        var distances = Regex.Matches(input[1], @"\d+").Select(match => int.Parse(match.Value));

        var timeAndBestDistance = times.Zip(distances, (t, d) => (t, d)).ToList();

        return timeAndBestDistance
            .Select(pair => GetPossibleMs(pair.t, pair.d))
            .Aggregate((a, b) => a * b)
            .ToString();
    }

    public override string Part2()
    {
        var input = File.ReadAllLines(FileInput);

        var time = int.Parse(Regex.Matches(input[0], @"\d+")
            .Select(match => match.Value)
            .Aggregate((a, b) => a + b));

        var distance = long.Parse(Regex.Matches(input[1], @"\d+")
            .Select(match => match.Value)
            .Aggregate((a, b) => a + b));

        return GetPossibleMs(time, distance).ToString();
    }

    private static long GetPossibleMs(long time, long distance)
    {
        var possibleMs = 0;

        for (var speed = 0; speed < time; speed++)
        {
            var remainingTime = time - speed;

            if (remainingTime * speed > distance)
                possibleMs++;
        }

        return possibleMs;
    }
}