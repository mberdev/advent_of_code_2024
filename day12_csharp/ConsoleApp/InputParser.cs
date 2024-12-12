using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class InputParser
    {

        public static string[] ParseInput(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllLines(filePath);
            }
            else
            {
                throw new FileNotFoundException("The specified file was not found.", filePath);
            }
        }
    }
}
