
using System.ComponentModel.DataAnnotations;
using ConsoleApp;



var filePath = "./input_data/test_input1.txt";
string[] lines;
if (System.IO.File.Exists(filePath))
{
    lines = System.IO.File.ReadAllLines(filePath);
}
else
{
    throw new FileNotFoundException("The specified file was not found.", filePath);
}


// Control
//Console.WriteLine(data);

var d = InputParser.ParseInput(lines);

part1(d);

void part1(Dictionary<string, string> data)
{

}

Console.WriteLine("Done.");




