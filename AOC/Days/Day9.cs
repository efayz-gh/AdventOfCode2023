namespace AOC.Days;

public class Day9 : Day
{
    public override string Part1() =>
        File.ReadAllLines(FileInput)
            .Select(line => line
                .Split(" ")
                .Select(long.Parse)
                .ToList())
            .Select(Predict)
            .Sum()
            .ToString();

    public override string Part2() =>
        File.ReadAllLines(FileInput)
            .Select(line => line
                .Split(" ")
                .Select(long.Parse)
                .Reverse()
                .ToList())
            .Select(Predict)
            .Sum()
            .ToString();

    private static long Predict(List<long> sequence) =>
        sequence.All(x => x == 0)
            ? 0
            : sequence[^1] + Predict(sequence.Zip(sequence.Skip(1), (a, b) => b - a).ToList());
}