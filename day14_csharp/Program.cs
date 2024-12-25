
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

var data = InputParser.ParseInput(lines);
var width = filePath.Contains("test_input1") ? 11 : 101;
var height = filePath.Contains("test_input1") ? 7 : 103;
// Control
//Console.WriteLine(data);


part1(data, width, height);
//part2(grid);

Console.WriteLine("Done.");


static void part1(List<(Position, Vector)> robots, int width, int height)
{
    var grid = new PacmanGrid(width, height);
    var updated = Simulate(robots, grid, 100);
    (int a, int b, int c, int d) = CountQuadrants(grid, updated);
    int result = a * b * c * d;
    Console.WriteLine("Part1 : " + result);
}

static (int a, int b, int c, int d) CountQuadrants(PacmanGrid grid, List<Position> updated)
{
    var topLeftMinX = 0;
    var topLeftMaxX = grid.Width / 2 -1;
    var topLeftMinY = 0;
    var topLeftMaxY = grid.Height / 2 -1;

    Console.WriteLine($"A: {topLeftMinX} {topLeftMaxX} {topLeftMinY} {topLeftMaxY}");

    var topRightMinX = grid.Width / 2 +1;
    var topRightMaxX = grid.Width -1;
    var topRightMinY = 0;
    var topRightMaxY = grid.Height / 2 -1;

    Console.WriteLine($"B: {topRightMinX} {topRightMaxX} {topRightMinY} {topRightMaxY}");

    var bottomLeftMinX = 0;
    var bottomLeftMaxX = grid.Width / 2 - 1;
    var bottomLeftMinY = grid.Height / 2 +1;
    var bottomLeftMaxY = grid.Height -1;

    Console.WriteLine($"C: {bottomLeftMinX} {bottomLeftMaxX} {bottomLeftMinY} {bottomLeftMaxY}");

    var bottomRightMinX = grid.Width / 2 + 1;
    var bottomRightMaxX = grid.Width - 1;
    var bottomRightMinY = grid.Height / 2 + 1;
    var bottomRightMaxY = grid.Height - 1;

    Console.WriteLine($"D: {bottomRightMinX} {bottomRightMaxX} {bottomRightMinY} {bottomRightMaxY}");

    //var aItems = updated.Where(p => p.X >= topLeftMinX && p.X <= topLeftMaxX && p.Y >= topLeftMinY && p.Y <= topLeftMaxY);
    //var bItems = updated.Where(p => p.X >= topRightMinX && p.X <= topRightMaxX && p.Y >= topRightMinY && p.Y <= topRightMaxY);
    //var cItems = updated.Where(p => p.X >= bottomLeftMinX && p.X <= bottomLeftMaxX && p.Y >= bottomLeftMinY && p.Y <= bottomLeftMaxY);
    //var dItems = updated.Where(p => p.X >= bottomRightMinX && p.X <= bottomRightMaxX && p.Y >= bottomRightMinY && p.Y <= bottomRightMaxY);

    //Console.Write("A:");
    //foreach (var item in aItems)
    //{
    //    Console.Write(item);
    //}
    //Console.WriteLine();
    //Console.Write("B:");
    //foreach (var item in bItems)
    //{
    //    Console.Write(item);
    //}
    //Console.WriteLine();
    //Console.Write("C:");
    //foreach (var item in cItems)
    //{
    //    Console.Write(item);
    //}
    //Console.WriteLine();
    //Console.Write("D:");
    //foreach (var item in dItems)
    //{
    //    Console.Write(item);
    //}
    //Console.WriteLine();

    //var a = aItems.Count();
    //var b = bItems.Count();
    //var c = cItems.Count();
    //var d = dItems.Count();

    var a = updated.Count(p => p.X >= topLeftMinX && p.X <= topLeftMaxX && p.Y >= topLeftMinY && p.Y <= topLeftMaxY);
    var b = updated.Count(p => p.X >= topRightMinX && p.X <= topRightMaxX && p.Y >= topRightMinY && p.Y <= topRightMaxY);
    var c = updated.Count(p => p.X >= bottomLeftMinX && p.X <= bottomLeftMaxX && p.Y >= bottomLeftMinY && p.Y <= bottomLeftMaxY);
    var d = updated.Count(p => p.X >= bottomRightMinX && p.X <= bottomRightMaxX && p.Y >= bottomRightMinY && p.Y <= bottomRightMaxY);

    return (a, b, c, d);
}

static List<Position> Simulate(List<(Position, Vector)> robots, PacmanGrid grid, int totalTime)
{
    var positions = new List<Position>();
    foreach ((var position, var velocity) in robots)
    {
        var x = (position.X + totalTime * velocity.X)%grid.Width;
        if(x < 0)
            x += grid.Width;
        var y = (position.Y + totalTime * velocity.Y)%grid.Height;
        if (y < 0)
            y += grid.Height;
        positions.Add(new Position(x, y));
    }

    //foreach (var position in positions)
    //{
    //    Console.WriteLine(position);
    //}

    return positions;
}

static void part2(TextBasedGrid grid)
{
}

