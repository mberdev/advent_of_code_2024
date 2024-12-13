using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public record Button(int price, int X, int Y)
    {
        public static Button parse(int price, string button)
        {
            var parts = button.Split(' ');
            return new Button(price, int.Parse(parts[2].Replace("X+", "").Replace(",", "")), int.Parse(parts[3].Replace("Y+", "")));
        }

        public override string ToString()
        {
            return $"Button: {price}, X: {X}, Y: {Y}";
        }
    }

    public record Prize(Position p)
    {
        public static Prize parse(string prize)
        {
            var parts = prize.Split(' ');
            return new Prize(new Position(int.Parse(parts[1].Replace("X=", "").Replace(",", "")), int.Parse(parts[2].Replace("Y=", ""))));
        }

        public override string ToString()
        {
            return $"Prize: {p}";
        }
    }

    public class Machine
    {
        public Button[] Buttons { get; init; } = new Button[0];
        public Prize Prize { get; init; } = new Prize(new Position(0, 0));

        public override string ToString()
        {
            return $"Machine: {string.Join(", ", Buttons.ToList())}, {Prize}";
        }
    }

    public class Data
    {
        public Machine[] Machines { get; init; } = new Machine[0];

        public override string ToString()
        {

            return string.Join("\n", Machines.ToList());
        }
    }

    public class InputParser
    {

        public static Data ParseInput(string[] lines)
        {
            List<Machine> machines = new();

            int i = 0;
            while (i < lines.Length)
            {
                string buttonA = lines[i++];
                string buttonB = lines[i++];
                string prize = lines[i++];
                while (i < lines.Length && string.IsNullOrWhiteSpace(lines[i]))
                    i++;

                machines.Add(new Machine
                {
                    Buttons = new Button[]
                    {
                        Button.parse(3, buttonA),
                        Button.parse(1, buttonB),
                    },
                    Prize = Prize.parse(prize),
                });
            }

            var data =  new Data { Machines = machines.ToArray() };
            
            return data;
        }
    }
}
