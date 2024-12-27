
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    public class InputParser
    {

        public static Dictionary<string, Register> ParseInput(string[] lines)
        {
            var result = new Dictionary<string, Register>();

            var beforeEmptyLine = new List<string>();
            var afterEmptyLine = new List<string>();
            bool emptyLineFound = false;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    emptyLineFound = true;
                    continue;
                }

                if (!emptyLineFound)
                {
                    beforeEmptyLine.Add(line);
                }
                else
                {
                    afterEmptyLine.Add(line);
                }
            }

            foreach (var line in beforeEmptyLine)
            {
                if (string.IsNullOrWhiteSpace(line)) { continue; }

                var parts = line.Split(':');
                if (parts.Length != 2)
                {
                    throw new Exception("bad format");
                }
                var key = parts[0].Trim();
                var value = int.Parse(parts[1].Trim());
                result[key] = new Register
                {
                    Name = key,
                    Value = value == 1 ? true : false,
                };
            }



            string pattern = @"(\w+)\s+(XOR|OR|AND)\s+(\w+)\s*->\s*(\w+)";
            Regex regex = new(pattern);

            foreach (var line in afterEmptyLine)
            {
                if (string.IsNullOrEmpty(line)) { continue; }

                Match match = regex.Match(line);
                if (!match.Success)
                {
                    throw new Exception("");
                }

                string operand1 = match.Groups[1].Value;
                string operation = match.Groups[2].Value;
                string operand2 = match.Groups[3].Value;
                string right = match.Groups[4].Value;
                result[right] = new Register
                {
                    Name = right,
                    Exp = new Expression(operand1, operand2, operation)
                };
            }

            return result;
        }
    }
}
