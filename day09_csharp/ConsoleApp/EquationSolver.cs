//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApp
//{
//    public class EquationSolver
//    {
//        public static Tuple<int, int> Solve(
//            double AX,
//            double BX,
//            double AY,
//            double BY,
//            double TX,
//            double TY
//        ) {


//            double[,] coefficients = { { AX, BX }, { AY, BY } };
//            double[] constants = { TX, TY };

//            double[] result = SolveLinearEquations(coefficients, constants);

//            return new Tuple<int, int>((int)result[0], (int)result[1]);
//        }

//        private static double[] SolveLinearEquations(double[,] coefficients, double[] constants)
//        {
//            int n = constants.Length;
//            double[] result = new double[n];
//            double[,] matrix = new double[n, n + 1];

//            for (int i = 0; i < n; i++)
//            {
//                for (int j = 0; j < n; j++)
//                {
//                    matrix[i, j] = coefficients[i, j];
//                }
//                matrix[i, n] = constants[i];
//            }

//            for (int i = 0; i < n; i++)
//            {
//                double maxElement = Math.Abs(matrix[i, i]);
//                int maxRow = i;
//                for (int k = i + 1; k < n; k++)
//                {
//                    if (Math.Abs(matrix[k, i]) > maxElement)
//                    {
//                        maxElement = Math.Abs(matrix[k, i]);
//                        maxRow = k;
//                    }
//                }

//                for (int k = i; k < n + 1; k++)
//                {
//                    double tmp = matrix[maxRow, k];
//                    matrix[maxRow, k] = matrix[i, k];
//                    matrix[i, k] = tmp;
//                }

//                for (int k = i + 1; k < n; k++)
//                {
//                    double factor = -matrix[k, i] / matrix[i, i];
//                    for (int j = i; j < n + 1; j++)
//                    {
//                        if (i == j)
//                        {
//                            matrix[k, j] = 0;
//                        }
//                        else
//                        {
//                            matrix[k, j] += factor * matrix[i, j];
//                        }
//                    }
//                }
//            }

//            for (int i = n - 1; i >= 0; i--)
//            {
//                result[i] = matrix[i, n] / matrix[i, i];
//                for (int k = i - 1; k >= 0; k--)
//                {
//                    matrix[k, n] -= matrix[k, i] * result[i];
//                }
//            }

//            return result;
//        }
//    }

//}
