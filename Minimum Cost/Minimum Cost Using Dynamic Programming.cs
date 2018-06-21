using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimumCost
{
    /// <summary>
    /// problem statement:
    /// Given a matrix of positive integers, you have to reach from the top left corner to the bottom right in minimum cost.
    // You can only go one square right, down or diagonally right-down. Cost is the sum of squares you've covered. 
    // Return the minimum cost. 
    // e.g. 
    // 4 5 6 
    // 1 2 3 
    // 0 1 2 
    // My mock interview analysis is here: http://www.mplighting.com/tools/RecommendedDrivers.aspx?fixtureSelected=L01

    // Answer: 8 (4+1+0+1+2)
    // May 14, 2018
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            RunTestcase();
        }

        public static void RunTestcase()
        {
            var matrix = new int[3, 3];

            matrix[0, 0] = 4;
            matrix[0, 1] = 5;
            matrix[0, 2] = 6;

            matrix[1, 0] = 1;
            matrix[1, 1] = 2;
            matrix[1, 2] = 3;

            matrix[2, 0] = 0;
            matrix[2, 1] = 1;
            matrix[2, 2] = 2;

            var minimumCost = CalculateMinimumCostFromTopLeftToBottomRight(matrix);
            Debug.Assert(minimumCost == 8);
        }

        /// <summary>
        /// using dynamic programming method
        /// time complexity:  O(rows * columns)
        /// space complexity: O(rows) 
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static int CalculateMinimumCostFromTopLeftToBottomRight(int[,] matrix)
        {
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
            {
                return 0;
            }

            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);

            var previous = new int[columns];
            var current = new int[columns];

            for (int row = 0; row < rows; row++)
            {
                current[0] = previous[0] + matrix[row, 0];

                for (int col = 1; col < columns; col++)
                {
                    var previousSteps = new int[] { previous[col - 1], previous[col], current[col - 1] };
                    current[col] = matrix[row, col] + previousSteps.Min();
                }

                // update previous
                for (int col = 0; col < columns; col++)
                {
                    previous[col] = current[col];
                }
            }

            return current[columns - 1];
        }
    }
}