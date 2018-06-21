using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMinimumNumbers
{
    /*
     * May 28, 2018
    Given a matrix consists of 0 and 1, find the distance of the nearest 0 for each cell.
    The distance between two adjacent cells is 1.
    Example 1: 
    Input:
    0 0 0
    0 1 0
    0 0 0
    Output:
    0 0 0
    0 1 0
    0 0 0
    Example 2: 
    Input:
    0 0 0
    0 1 0
    1 1 1
    */
    class Program
    {
        static void Main(string[] args)
        {
            //RunTestcase1();
            //RunTestcase2();
            RunTestcase3();
        }

        public static void RunTestcase1()
        {
            var numbers = new int[3, 3];
            // first row
            numbers[1, 1] = 1;

            var minimumDistance = FindDistanceOfNearestZeroForEachCell(numbers);
        }

        public static void RunTestcase2()
        {
            var numbers = new int[3, 3];
            // second row
            numbers[1, 1] = 1;

            // third row
            numbers[2, 0] = 1;
            numbers[2, 1] = 1;
            numbers[2, 2] = 1;

            var minimumDistance = FindDistanceOfNearestZeroForEachCell(numbers);
        }

        public static void RunTestcase3()
        {
            var numbers = new int[4, 4];
            // second row
            numbers[1, 1] = 1;

            // third row
            for (int col = 0; col <= 3; col++)
            {
                numbers[2, col] = 1;
                numbers[3, col] = 1;
            }

            var minimumDistance = FindDistanceOfNearestZeroForEachCell(numbers);
        }

        /// <summary>
        /// the algorithm is given by my coach, and then I have to work on time complexity. 
        /// In order to get minimum time complexity, for each cell with nonzero, we like that it will be 
        /// visited only once and populated with the distance of the nearest 0 for each cell. 
        ///        
        /// Later the coach gave me major hint; Get together all elements in matrix with 
        /// zero value, and then apply breadth first search in the same time. 
        /// There are a few issues to consider in the design. Julia found the issue with test case 3, 4 x 4 matrix.
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int[,] FindDistanceOfNearestZeroForEachCell(int[,] numbers)
        {
            if (numbers == null || numbers.GetLength(0) == 0 || numbers.GetLength(1) == 0)
            {
                return new int[0, 0];
            }

            var zeroElements = enqueueAllZeroElements(numbers);

            var rows = numbers.GetLength(0);
            var columns = numbers.GetLength(1);

            var queue = new Queue<int[]>();
            var visited = new bool[rows, columns];

            enqueueGivenNodes(zeroElements, queue);

            int layerIndex = 0;
            while (queue.Count > 0)
            {
                var layerCount = queue.Count; // size for each layer

                for (int i = 0; i < layerCount; i++)
                {
                    var hashSet = currentLayersToHashSet(queue);

                    var visit = queue.Dequeue();
                    var currentRow = visit[0];
                    var currentCol = visit[1];

                    // mark visit
                    visited[currentRow, currentCol] = true;

                    // save distance
                    if (numbers[currentRow, currentCol] == 1) // 0 or 1 two possible values
                    {
                        numbers[currentRow, currentCol] = layerIndex;
                    }

                    pushNeighborToQueue(queue, numbers, visited, hashSet, currentRow - 1, currentCol);     // up
                    pushNeighborToQueue(queue, numbers, visited, hashSet, currentRow, currentCol + 1); // right
                    pushNeighborToQueue(queue, numbers, visited, hashSet, currentRow + 1, currentCol);     // down
                    pushNeighborToQueue(queue, numbers, visited, hashSet, currentRow, currentCol - 1); // left
                }

                layerIndex++;
            }

            return numbers;
        }

        private static HashSet<string> currentLayersToHashSet(Queue<int[]> queue)
        {
            var hashSet = new HashSet<string>();

            var list = queue.ToArray();

            foreach (var item in list)
            {
                hashSet.Add(item[0].ToString() + "," + item[1].ToString());
            }

            return hashSet;
        }

        /// <summary>
        /// extra work:
        /// Do not add current layer nodes into next round. 
        /// The design is updated to add current layer nodes to a hashset with unique key. 
        /// It is ok to add same node to next round more than once. 
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="numbers"></param>
        /// <param name="visited"></param>
        /// <param name="hashSet"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private static void pushNeighborToQueue(Queue<int[]> queue, int[,] numbers, bool[,] visited, HashSet<string> hashSet, int row, int col)
        {
            var rows = numbers.GetLength(0);
            var columns = numbers.GetLength(1);

            var outOfRange = row < 0 || row >= rows || col < 0 || col >= columns;
            var key = row.ToString() + "," + col.ToString();

            if (outOfRange || visited[row, col] || numbers[row, col] != 1 ||
                hashSet.Contains(key))
            {
                return;
            }

            queue.Enqueue(new int[] { row, col });
        }

        private static void enqueueGivenNodes(IList<int[]> elements, Queue<int[]> queue)
        {
            foreach (var item in elements)
            {
                queue.Enqueue(new int[] { item[0], item[1] });
            }
        }

        /// <summary>
        /// the major hint was given by the interviewer. 
        /// In order to lower the time complexity to O(rows * cols)
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private static IList<int[]> enqueueAllZeroElements(int[,] numbers)
        {
            var rows = numbers.GetLength(0);
            var columns = numbers.GetLength(1);

            var zeroNodes = new List<int[]>();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    if (numbers[row, col] != 0)
                    {
                        continue;
                    }

                    zeroNodes.Add(new int[] { row, col });
                }
            }

            return zeroNodes;
        }
    }
}