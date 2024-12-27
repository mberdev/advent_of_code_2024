using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public enum OP
    {
        XOR,
        OR,
        AND
    }

    public class Expression
    {
        public OP Op { get; init; }

        public string Left { get; init; } = "";
        public string Right { get; init; } = "";

        public Expression(string left, string right, string op)
        {
            Left = left;
            Right = right;
            Op = parseOp(op);
        }

        private static OP parseOp(string op)
        {
            switch (op)
            {
                case "XOR": return OP.XOR;
                case "OR": return OP.OR;
                case "AND": return OP.AND;
                default: throw new Exception("");
            }
        }

        public string toString()
        {
            return $"{Left} {Op} {Right}";
        }

    }

    public class Register
    {
        public required string Name { get; init; } = "";
        public bool? Value { get; set; }

        public Expression? Exp { get; set; }

        public string toString()
        {
            string exp = Exp != null ? Exp.toString() : "";
            return $"{Name}: {Value} ({Exp})";
        }
    }
}
