
namespace ConsoleApp
{
    public class EquationSolver2
    {
        public static Tuple<long, long>? Solve(
            int AX,
            int BX,
            int AY,
            int BY,
            long TX,
            long TY
        )
        {
            double dAX = AX;
            double dBX = BX;
            double dAY = AY;
            double dBY = BY;
            double dTX = TX;
            double dTY = TY;

            //actual solution if we allowed non-int values
            double A = (dTX - (dBX * dTY / dBY)) / (dAX - (dAY * dBX / dBY));
            double B = (dTY - (dAY * dTX / dAX)) / (dBY - (dAY * dBX / dAX));

            Console.WriteLine("A :" + A + " B: " + B);

            // possible int solution
            long nearestA = (long)Math.Round(A);
            long nearestB = (long)Math.Round(B);

            if (nearestA < 0 || nearestB < 0)
            {
                return null;

            }

            //try the solution
            if (TX != (nearestA * AX + nearestB * BX) || TY != (nearestA * AY + nearestB * BY))
            {
                return null;
            }


            //business rule : no more than 100 times per button
            //if (nearestA > 100 || nearestB > 100)
            //{
            //    return null;
            //}


            return Tuple.Create(nearestA, nearestB);
        }

        public static bool DoubleIsInteger(double d)
        {
            //return Math.Abs(d % 1) <= (Double.Epsilon * 100);
            return Math.Abs(d - (int)Math.Round(d)) < 0.00001;
        }
    }



}
