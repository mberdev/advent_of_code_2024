
using ConsoleApp;


var filePath = "./input_data/input.txt";
string[] lines;
if (System.IO.File.Exists(filePath))
{
    lines = System.IO.File.ReadAllLines(filePath);
}
else
{
    throw new FileNotFoundException("The specified file was not found.", filePath);
}

var grid = new TextBasedGrid(lines);

// Control
//Console.WriteLine(data);


part1(grid);
//part2(grid, start, end, knownBestScore);

Console.WriteLine("Done.");


static void part1(TextBasedGrid grid)
{
    var antennas = InputParser.ParseInput(grid);

    HashSet<Position> allAntinodes = new();

    foreach (var (key, positions) in antennas)
    {
        var antinodes = new List<Position>();

        //Console.WriteLine($"Antenna {key}: {string.Join(", ", value)}");
        var allPairs = FindAllPairs(positions);

        foreach (var (start, end) in allPairs)
        {
            //Console.WriteLine($"   Antenna {key}: {start} -> {end}");
            Vector vector = new Vector(start, end);
            var antiNode1 = start.Subtract(vector);
            var antiNode2 = end.Add(vector);

            antinodes.Add(antiNode1);
            antinodes.Add(antiNode2);
        }

        var validAntinodes = antinodes.Where(p => grid.IsInGrid(p)).ToList();

        //Console.WriteLine($"Antenna {key} antinodes: {string.Join(", ", validAntinodes)}");
        //foreach (var antinode in validAntinodes)
        //{
        //    grid.SetAt(antinode, '#');
        //}
        //grid.Print();
        //Console.WriteLine();

        foreach (var antinode in validAntinodes)
        {
            allAntinodes.Add(antinode);
        }
    }

    Console.WriteLine($"Antinodes count: {allAntinodes.Count}");

}

static List<(Position, Position)> FindAllPairs(List<Position> positions)
{
    var pairs = new List<(Position, Position)>();
    for (int i = 0; i < positions.Count; i++)
    {
        for (int j = i + 1; j < positions.Count; j++)
        {
            pairs.Add((positions[i], positions[j]));
        }
    }
    return pairs;

}

//static void part2(TextBasedGrid textGrid, Position start, Position end, long knownBestScore)
//{
//}
