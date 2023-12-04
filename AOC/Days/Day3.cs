using System.Text.RegularExpressions;

namespace AOC.Days;

public partial class Day3 : Day
{
    private class Number
    {
        public int Value { get; }
        public int Line { get; }
        public int[] Indices { get; }

        public Number(int value, int line, int index, int length)
        {
            Value = value;
            Line = line;
            Indices = new int[length];
            for (var i = 0; i < length; i++)
                Indices[i] = index + i;
        }

        public bool IsAdjacent(int line, int index)
        {
            (int x, int y)[] adjacent =
            {
                (-1, -1), (-1, 0), (-1, 1),
                (0, -1), (0, 1),
                (1, -1), (1, 0), (1, 1)
            };

            return adjacent.Any(pos => Line + pos.x == line && Indices.Any(i => i + pos.y == index));
        }
    }

    public override string Part1()
    {
        var input = File.ReadAllLines(FileInput);

        var numIndices = input.Select(line => NumberRegex().Matches(line)
            .Select(match => (match.Index, match.Index + match.Length))).ToList();

        int sum = 0;

        for (var i = 0; i < numIndices.Count; i++)
        {
            foreach (var (from, to) in numIndices[i])
            {
                var num = input[i][from..to];

                bool isSymbolAdjacent = false;

                for (var j = from; j < to; j++)
                {
                    if (SymbolAdjacent(input, i, j))
                    {
                        isSymbolAdjacent = true;
                        break;
                    }
                }

                if (isSymbolAdjacent)
                    sum += int.Parse(num);
            }
        }

        return sum.ToString();
    }

    public override string Part2()
    {
        var input = File.ReadAllLines(FileInput).ToList();

        var gearIndices = input.Select(line => Regex.Matches(line, @"\*")
            .Select(match => match.Index)).ToList();

        var numbers = input.Select((line, index) => NumberRegex().Matches(line)
            .Select(match => new Number(int.Parse(match.Value), index, match.Index, match.Length)));

        return gearIndices.Select((indices, line) => indices
                .Select(index => numbers
                    .Where(num => num.Any(n =>
                        n.Indices.Contains(index) ||
                        n.Indices.Contains(index + 1) ||
                        n.Indices.Contains(index - 1)))
                    .SelectMany(num => num)
                    .Where(num => num.IsAdjacent(line, index))
                    .Select(num => num.Value)
                    .ToList())
                .Select(numList => numList.Count == 2 ? numList.Aggregate((a, b) => a * b) : 0)
                .Sum()).Sum()
            .ToString();
    }

    private bool SymbolAdjacent(IReadOnlyList<string> map, int line, int index)
    {
        (int, int)[] adjacent =
        {
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1), (0, 1),
            (1, -1), (1, 0), (1, 1)
        };

        foreach (var (x, y) in adjacent)
        {
            if (line + x < 0 || line + x >= map.Count || index + y < 0 || index + y >= map[line].Length)
                continue;

            if (!".0123456789".Contains(map[line + x][index + y]))
                return true;
        }

        return false;
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();
}