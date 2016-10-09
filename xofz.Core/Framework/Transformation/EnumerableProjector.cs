namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class EnumerableProjector
    {
        public virtual T[][] Project2<T>(IEnumerable<T> source)
        {
            var queue = new ConcurrentQueue<T>(source);
            var magnitude = (int) (Math.Sqrt(queue.Count) + 1);
            var twoDArray = new T[magnitude][];
            for (var i = 0; i < magnitude; ++i)
            {
                twoDArray[i] = new T[magnitude];
            }

            int counter1 = 0, counter2 = 0;
            T item;
            while (queue.TryDequeue(out item))
            {
                twoDArray[counter1][counter2] = item;
                ++counter2;
                if (counter2 != twoDArray[counter1].Length)
                {
                    continue;
                }

                ++counter1;
                counter2 = 0;
            }

            return twoDArray;
        }

        public virtual T[][][] Project3<T>(IEnumerable<T> source)
        {
            var queue = new ConcurrentQueue<T>(source);
            var magnitude = (int)(Math.Pow(queue.Count, 1 / (double)3) + 1);
            var threeDArray = new T[magnitude][][];
            for (var i = 0; i < magnitude; ++i)
            {
                threeDArray[i] = new T[magnitude][];
                for (var j = 0; j < magnitude; ++j)
                {
                    threeDArray[i][j] = new T[magnitude];
                }
            }

            int counter1 = 0, counter2 = 0, counter3 = 0;
            T item;
            while (queue.TryDequeue(out item))
            {
                threeDArray[counter1][counter2][counter3] = item;
                ++counter3;
                if (counter3 != threeDArray[counter1][counter2].Length)
                {
                    continue;
                }

                ++counter2;
                counter3 = 0;

                if (counter2 != threeDArray[counter1].Length)
                {
                    continue;
                }

                ++counter1;
                counter2 = 0;
            }

            return threeDArray;
        }

        public virtual T[][][][] Project4<T>(IEnumerable<T> source)
        {
            var queue = new ConcurrentQueue<T>(source);
            var magnitude = (int)(Math.Pow(queue.Count, 1 / (double)4) + 1);
            var fourDArray = new T[magnitude][][][];
            for (var i = 0; i < magnitude; ++i)
            {
                fourDArray[i] = new T[magnitude][][];
                for (var j = 0; j < magnitude; ++j)
                {
                    fourDArray[i][j] = new T[magnitude][];
                    for (var k = 0; k < magnitude; ++k)
                    {
                        fourDArray[i][j][k] = new T[magnitude];
                    }
                }
            }

            int counter1 = 0, counter2 = 0, counter3 = 0, counter4 = 0;
            T item;
            while (queue.TryDequeue(out item))
            {
                fourDArray[counter1][counter2][counter3][counter4] = item;
                ++counter4;
                if (counter4 != fourDArray[counter1][counter2][counter3].Length)
                {
                    continue;
                }

                ++counter3;
                counter4 = 0;

                if (counter3 != fourDArray[counter1][counter2].Length)
                {
                    continue;
                }

                ++counter2;
                counter3 = 0;

                if (counter2 != fourDArray[counter1].Length)
                {
                    continue;
                }

                ++counter1;
                counter2 = 0;
            }

            return fourDArray;
        }
    }
}
