using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class SecretNumber
    {
        public static long Next(long n)
        {

            //Calculate the result of multiplying the secret number by 64.Then,
            //mix this result into the secret number.Finally, prune the secret number.

            //Calculate the result of dividing the secret number by 32.Round
            //the result down to the nearest integer.Then, mix this result into the
            //secret number. Finally, prune the secret number.

            //Calculate the result of multiplying the secret number by 2048.Then,
            //mix this result into the secret number.Finally, prune the secret number.
            n = mix(n, n * 64);
            n = prune(n);

            n = mix(n, n / 32);
            n = prune(n);

            n = mix(n, n * 2048);
            n = prune(n);

            return n;
        }

        private static long mix(long n, long v)
        {
            return n ^ v;
        }

        private static long prune(long n)
        {
            return n % 16777216;
        }

    }
}
