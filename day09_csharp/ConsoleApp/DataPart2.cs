using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Item
    {
        private File? file;
        private Gap? gap;

        public Item(File file)
        {
            this.file = file;
        }

        public Item(Gap gap)
        {
            this.gap = gap;
        }

        public File File => file != null ? file : throw new InvalidOperationException("Item is not a file");
        public Gap Gap => gap != null ? gap : throw new InvalidOperationException("Item is not a gap");

        public bool IsFile => file != null;

        public bool IsGap => gap != null;       
    }

    public class File
    {
        public int ID { get; init; }
        public int Size { get; init; }

        public bool Moved { get; set; }
    }

    public class Gap
    {
        public int Size { get; init; }
    }

    //internal class DataPart2
    //{
    //    public required Dictionary<int, File> files;
    //    public required Dictionary<int, Gap> gaps;
    //}
}
