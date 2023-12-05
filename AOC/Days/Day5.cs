using System.Text.RegularExpressions;

namespace AOC.Days;

public class Day5 : Day
{
    public record Mapping(long DstStart, long SrcStart, long Length);

    public record Seed(long Start, long End);

    public override string Part1()
    {
        var input = File.ReadAllLines(FileInput);

        var seeds = Regex.Matches(input[0], @"\d+").Select(match => long.Parse(match.Value));

        var soilMap = GetMappings(input, "seed-to-soil map");
        var fertilizerMap = GetMappings(input, "soil-to-fertilizer map");
        var waterMap = GetMappings(input, "fertilizer-to-water map");
        var lightMap = GetMappings(input, "water-to-light map");
        var temperatureMap = GetMappings(input, "light-to-temperature map");
        var humidityMap = GetMappings(input, "temperature-to-humidity map");
        var locationMap = GetMappings(input, "humidity-to-location map");

        Dictionary<long, long> seedsToLocations = seeds.ToDictionary(seed => seed,
            seed => locationMap
                .Map(humidityMap
                    .Map(temperatureMap
                        .Map(lightMap
                            .Map(waterMap
                                .Map(fertilizerMap
                                    .Map(soilMap
                                        .Map(seed))))))));

        return seedsToLocations.MinBy(pair => pair.Value).Value.ToString();
    }

    public override string Part2()
    {
        var input = File.ReadAllLines(FileInput);

        var seedValues = Regex.Matches(input[0], @"\d+")
            .Select(match => long.Parse(match.Value)).ToList();

        var seedRanges = new List<Seed>();
        for (var i = 0; i < seedValues.Count; i += 2)
            seedRanges.Add(new Seed(seedValues[i], seedValues[i] + seedValues[i + 1]));

        var soilMap = GetMappings(input, "seed-to-soil map").ToArray();
        var fertilizerMap = GetMappings(input, "soil-to-fertilizer map").ToArray();
        var waterMap = GetMappings(input, "fertilizer-to-water map").ToArray();
        var lightMap = GetMappings(input, "water-to-light map").ToArray();
        var temperatureMap = GetMappings(input, "light-to-temperature map").ToArray();
        var humidityMap = GetMappings(input, "temperature-to-humidity map").ToArray();
        var locationMap = GetMappings(input, "humidity-to-location map").ToArray();

        var minLocation = long.MaxValue;
        
        // very bad performance, but it works
        foreach (var range in seedRanges)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write($"Range: {range.Start} - {range.End} ({(seedRanges.IndexOf(range) + 1) * 1.0 / seedRanges.Count * 100}% done)     ");
            
            for (var i = range.Start; i < range.End; i++)
            {
                var location = locationMap
                    .Map(humidityMap
                        .Map(temperatureMap
                            .Map(lightMap
                                .Map(waterMap
                                    .Map(fertilizerMap
                                        .Map(soilMap
                                            .Map(i)))))));

                if (location < minLocation)
                    minLocation = location;
            }
        }
        
        Console.SetCursorPosition(0, Console.CursorTop);
        
        return minLocation.ToString();
    }

    private static IEnumerable<Mapping> GetMappings(IEnumerable<string> input, string mappingName)
    {
        var mappingLines = input
            .SkipWhile(line => line != $"{mappingName}:").Skip(1)
            .TakeWhile(line => !string.IsNullOrWhiteSpace(line));

        var mappings = new List<Mapping>();

        foreach (var line in mappingLines)
        {
            var mappingValues = Regex.Matches(line, @"\d+")
                .Select(match => long.Parse(match.Value)).ToList();

            mappings.Add(new Mapping(mappingValues[0], mappingValues[1], mappingValues[2]));
        }

        return mappings;
    }
}

public static class MappingExtension
{
    public static long Map(this IEnumerable<Day5.Mapping> mappings, long index)
    {
        foreach (var mapping in mappings)
            if (index >= mapping.SrcStart && index < mapping.SrcStart + mapping.Length)
                return index - mapping.SrcStart + mapping.DstStart;

        return index;
    }
    
    // could increase performance by searching in reverse (not implemented)
    public static long MapReverse(this IEnumerable<Day5.Mapping> mappings, long index)
    {
        foreach (var mapping in mappings)
            if (index >= mapping.DstStart && index < mapping.DstStart + mapping.Length)
                return index - mapping.DstStart + mapping.SrcStart;

        return index;
    }
}