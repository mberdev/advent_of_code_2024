
using ConsoleApp;

Test();

void Test()
{
    var x = new List<int> { 2,3, 10, 20,21,22,29,31};
    var result = Topology.FindHorizontalContinuousLine(x, 3, 0, 35);
    int start = result!.Value.Item1;
    int end = result!.Value.Item2;
    if (start != 20 || end != 22)
    {
        throw new Exception("Test failed");
    }
}

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



//part1(data, width, height);
part2(data, width, height);

Console.WriteLine("Done.");


static void part1(List<(Position, Vector)> data, int width, int height)
{
    var grid = new PacmanGrid(width, height);
    var robots = data.Select(d => d.Item1).ToList();
    var velocities = data.Select(d => d.Item2).ToList();
    var updated = Simulate(robots, velocities, grid, 100);
    (int a, int b, int c, int d) = CountQuadrants(grid, updated);
    int result = a * b * c * d;
    Console.WriteLine("Part1 : " + result);
}

static void part2(List<(Position, Vector)> data, int width, int height)
{
    var robots = data.Select(d => d.Item1).ToList();
    var velocities = data.Select(d => d.Item2).ToList();

    var grid = new PacmanGrid(width, height);

    for (int t = 0; t < 1000000; t++)
    {
        var horizontalSlices = robots.GroupBy(r => r.Y);
        (int, int)? horizontalSegment = null;
        foreach (var slice in horizontalSlices)
        {
            var y = slice.Key;
            var xValues = slice.Select(r => r.X).ToList();
            int minLineLength = 15;
            horizontalSegment = Topology.FindHorizontalContinuousLine(xValues, minLineLength, 0, width);
            if (horizontalSegment!=null)
            {
                var start = horizontalSegment!.Value.Item1;
                var end = horizontalSegment!.Value.Item2;

                //Console.WriteLine($"Found line: ({start}->{end},{y}");

                var robotsToHighlight = Enumerable
                    .Range(start, end)
                    .Select(x => new Position(x, y)).ToList();

                grid.Print(robots, robotsToHighlight);
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine($"Part 2: {t}");

                break;
            }

        }


        robots = Simulate(robots, velocities, grid, 1);

        if(t%1000 == 999)
        {
            Console.WriteLine("Time: " + t);
        }
    }

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

static List<Position> Simulate(List<Position> robots, List<Vector> velocities, PacmanGrid grid, int time)
{
    var updated = new List<Position>();
    for(int r = 0; r < robots.Count; r++)
    {
        var position = robots[r];
        var velocity = velocities[r];

        var x = (position.X + time * velocity.X)%grid.Width;
        if(x < 0)
            x += grid.Width;
        var y = (position.Y + time * velocity.Y)%grid.Height;
        if (y < 0)
            y += grid.Height;
        updated.Add(new Position(x, y));
    }

    //foreach (var position in positions)
    //{
    //    Console.WriteLine(position);
    //}

    return updated;
}

