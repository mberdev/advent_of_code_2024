﻿
using System.ComponentModel.DataAnnotations;
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


// Control
//Console.WriteLine(data);

var d = InputParser.ParseInput(lines);

//part1(d);
part2(d);

void part1(Dictionary<string, HashSet<string>> d)
{
    var lan = new Lan(d);
    var allTriplets = new HashSet<NSet>();
    foreach (var k in d.Keys)
    {
        var newTripets = lan.FindTriplets(k);
        foreach (var t in newTripets)
        {
            allTriplets.Add(t);
        }
    }

    //foreach (var t in allTriplets)
    //{
    //    Console.WriteLine(string.Join(",", t));
    //}

    Console.WriteLine("Start with t: " + allTriplets.Count(t => t.hasStartsWithT())); 
}

void part2(Dictionary<string, HashSet<string>> d)
{
    var lan = new Lan(d);
    var allSets = new HashSet<NSet>();
    foreach (var k in d.Keys)
    {
        var set = lan.FindNSet(k);
        allSets.Add(set);
        //Console.WriteLine($"{k}: " + set);
    }

    var longest = allSets.OrderByDescending(s => s.Count).First();
    Console.WriteLine("Most computers: " + longest);


    //Console.WriteLine("Start with t: " + allSets.Count(t => t.hasStartsWithT()));
}

Console.WriteLine("Done.");




