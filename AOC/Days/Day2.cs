using System.Text.RegularExpressions;

namespace AOC.Days;

public partial class Day2 : Day
{
    public override string Part1()
    {
        const int maxRed = 12, maxGreen = 13, maxBlue = 14;

        var lines = File.ReadAllLines(FileInput).ToList();

        var idSum = 0;

        foreach (var line in lines)
        {
            var gameId = int.Parse(GameIdRegex().Match(line).Value);

            var possible = true;

            foreach (var cube in line.Split(';'))
            {
                var redResult = RedRegex().Match(cube);
                var greenResult = GreenRegex().Match(cube);
                var blueResult = BlueRegex().Match(cube);
                
                var red = redResult.Success ? int.Parse(redResult.Value) : 0;
                var green = greenResult.Success ? int.Parse(greenResult.Value) : 0;
                var blue = blueResult.Success ? int.Parse(blueResult.Value) : 0;

                if (red > maxRed || green > maxGreen || blue > maxBlue)
                {
                    possible = false;
                    break;
                }
            }

            if (possible)
                idSum += gameId;
        }

        return idSum.ToString();
    }

    public override string Part2()
    {
        var lines = File.ReadAllLines(FileInput).ToList();

        var cubeSum = 0;

        foreach (var line in lines)
        {
            int maxRed = 0, maxGreen = 0, maxBlue = 0;
            
            foreach (var cube in line.Split(';'))
            {
                var redResult = RedRegex().Match(cube);
                var greenResult = GreenRegex().Match(cube);
                var blueResult = BlueRegex().Match(cube);
                
                var red = redResult.Success ? int.Parse(redResult.Value) : 0;
                var green = greenResult.Success ? int.Parse(greenResult.Value) : 0;
                var blue = blueResult.Success ? int.Parse(blueResult.Value) : 0;

                maxRed = Math.Max(maxRed, red);
                maxGreen = Math.Max(maxGreen, green);
                maxBlue = Math.Max(maxBlue, blue);
            }

            cubeSum += maxRed * maxGreen * maxBlue;
        }

        return cubeSum.ToString();
    }

    [GeneratedRegex(@"\d+(?= red)")]
    private static partial Regex RedRegex();
    
    [GeneratedRegex(@"\d+(?= green)")]
    private static partial Regex GreenRegex();
    
    [GeneratedRegex(@"\d+(?= blue)")]
    private static partial Regex BlueRegex();

    [GeneratedRegex(@"(?<=Game )\d+")]
    private static partial Regex GameIdRegex();
}