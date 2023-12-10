namespace AOC.Days;

public class Day8 : Day
{
    public override string Part1()
    {
        var input = File.ReadAllLines(FileInput);

        var directions = new Queue<char>(input[0]);

        Dictionary<string, (string l, string r)> nodes = input
            .Skip(2)
            .ToDictionary(line => line[..3], line => (line[7..10], line[12..15]));

        var node = "AAA";
        var steps = 0;

        while (node != "ZZZ")
        {
            var (l, r) = nodes[node];

            var direction = directions.Dequeue();
            if (directions.Count == 0)
                directions = new Queue<char>(input[0]);

            node = direction == 'L' ? l : r;

            steps++;
        }
        
        return steps.ToString();
    }

    public override string Part2()
    {
        var input = File.ReadAllLines(FileInput);

        var directions = new Queue<char>(input[0]);

        Dictionary<string, (string l, string r)> nodes = input
            .Skip(2)
            .ToDictionary(line => line[..3], line => (line[7..10], line[12..15]));

        var nodePaths = nodes.Keys.Where(key => key.EndsWith("A")).ToDictionary(key => key, _ => 0L);
        
        foreach (var node in nodePaths)
        {
            var currNode = node.Key;
            while (!currNode.EndsWith("Z"))
            {
                var (l, r) = nodes[currNode];

                var direction = directions.Dequeue();
                if (directions.Count == 0)
                    directions = new Queue<char>(input[0]);

                currNode = direction == 'L' ? l : r;

                nodePaths[node.Key]++;
            }
        }

        return nodePaths.Values.Aggregate(Lcm).ToString();
    }
    
    private static long Lcm(long a, long b) => Math.Abs(a * b) / Gcd(a, b);

    private static long Gcd(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}