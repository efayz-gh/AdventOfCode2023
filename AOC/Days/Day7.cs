using System.Collections;

namespace AOC.Days;

public class Day7 : Day
{
    public override string Part1() =>
        File.ReadAllLines(FileInput)
            .Select(line => line.Split(" "))
            .Select(split => (split[0], int.Parse(split[1])))
            .Order(Comparer<(string, int)>.Create((a, b) =>
            {
                foreach (var (aChar, bChar) in a.Item1.Zip(b.Item1))
                {
                    if (aChar == bChar)
                        continue;

                    return GetValue(aChar).CompareTo(GetValue(bChar));
                }

                return 0;
            }))
            .OrderBy(handTuple =>
            {
                var counts = handTuple.Item1
                    .GroupBy(c => c)
                    .ToDictionary(g => g.Key, g => g.Count());

                if (counts.ContainsValue(5))
                    return 6;

                if (counts.ContainsValue(4))
                    return 5;

                if (counts.ContainsValue(3))
                    return counts.ContainsValue(2) ? 4 : 3;

                if (counts.ContainsValue(2))
                    return counts.Count == 3 ? 2 : 1;

                return 0;
            })
            .Select((handTuple, index) => handTuple.Item2 * (index + 1))
            .Sum()
            .ToString();

    public override string Part2() =>
        File.ReadAllLines(FileInput)
            .Select(line => line.Split(" "))
            .Select(split => (split[0], int.Parse(split[1])))
            .Order(Comparer<(string, int)>.Create((a, b) =>
            {
                foreach (var (aChar, bChar) in a.Item1.Zip(b.Item1))
                {
                    if (aChar == bChar)
                        continue;

                    return GetValue2(aChar).CompareTo(GetValue2(bChar));
                }

                return 0;
            }))
            .OrderBy(handTuple =>
            {
                var counts = handTuple.Item1
                    .GroupBy(c => c)
                    .ToDictionary(g => g.Key, g => g.Count());

                if (counts.ContainsKey('J') && counts.Count != 1)
                {
                    var jokers = counts['J'];
                    counts.Remove('J');
                    counts[counts.MaxBy(kvp => kvp.Value).Key] += jokers;
                }

                if (counts.ContainsValue(5))
                    return 6;

                if (counts.ContainsValue(4))
                    return 5;

                if (counts.ContainsValue(3))
                    return counts.ContainsValue(2) ? 4 : 3;

                if (counts.ContainsValue(2))
                    return counts.Count == 3 ? 2 : 1;

                return 0;
            })
            .Select((handTuple, index) => handTuple.Item2 * (index + 1))
            .Sum()
            .ToString();

    private static int GetValue(char c) => c switch
    {
        'A' => 14,
        'K' => 13,
        'Q' => 12,
        'J' => 11,
        'T' => 10,
        _ => int.Parse(c.ToString())
    };

    private static int GetValue2(char c) => c switch
    {
        'A' => 14,
        'K' => 13,
        'Q' => 12,
        'J' => -1,
        'T' => 10,
        _ => int.Parse(c.ToString())
    };
}