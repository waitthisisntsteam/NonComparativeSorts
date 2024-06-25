﻿using System;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using System.Windows.Markup;

namespace NonComparativeSorts
{
    class Program
    {
        static int GetMax(int[] values)
        {
            int maxValue = values[0];

            foreach (var num in values)
            {
                if (maxValue < num)
                {
                    maxValue = num;
                }
            }

            return maxValue;
        }
        static int GetMin(int[] values)
        {
            int minValue = values[0];

            foreach (var num in values)
            {
                if (minValue > num)
                {
                    minValue = num;
                }
            }

            return minValue;
        }

        static void CountingSort(int[] values)
        {
            int maxValue = GetMax(values);
            int minValue = GetMin(values);

            int[] counts = new int[maxValue - minValue + 1];
            foreach (var num in values)
            {
                counts[num - minValue]++;
            }

            int n = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                for (int j = 0; j < counts[i]; j++)
                {
                    values[n] = i + minValue;
                    n++;
                }
            }
        }

        interface IKeyable
        {
            int Key { get; }
        }
        class KeyableString : IKeyable
        {
            string myString;

            public int Key => myString.Length;

            public static implicit operator KeyableString(string str) => new KeyableString() { myString = str };
        }
        static void PigeonHoleSort<T>(T[] values) where T : IKeyable
        {
            int maxValue = values[0].Key;
            int minValue = values[0].Key;

            foreach (var num in values)
            {
                if (maxValue < num.Key)
                {
                    maxValue = num.Key;
                }
                if (minValue > num.Key)
                {
                    minValue = num.Key;
                }
            }

            List<T>[] buckets = new List<T>[maxValue - minValue + 1];
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<T>();
            }
            foreach (var curr in values)
            {
                buckets[curr.Key - minValue].Add(curr);
            }

            int n = 0;
            foreach (var bucket in buckets)
            {
                foreach (var item in bucket)
                {
                    values[n] = item;
                    n++;
                }
            }
        }

        static List<int> InsertionSort(List<int> values)
        {
            if (values.Count < 2)
            {
                return values;
            }
            var sorted = new List<int>();
            for (int i = 0; i < values.Count; i++)
            {
                sorted.Add(values[i]);
                for (int j = sorted.Count - 1; j > 1; j--)
                {
                    if (sorted[j] < sorted[j - 1])
                    {
                        int temp = sorted[j];
                        sorted[j] = sorted[j - 1];
                        sorted[j - 1] = temp;
                    }
                }
            }

            return sorted;
        }
        static void BucketSort(int[] values)
        {
            int maxValue = GetMax(values);
            int minValue = GetMin(values);

            List<int>[] buckets = new List<int>[((maxValue - minValue) / values.Length) + 1];
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<int>();
            }
            foreach (var num in values)
            {
                buckets[num / values.Length].Add(num);
            }

            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = InsertionSort(buckets[i]);
            }

            int n = 0;
            foreach (var bucket in buckets)
            {
                foreach (var item in bucket)
                {
                    values[n] = item;
                    n++;
                }
            }
        }

        static void RadixSort(int[] values, int baseVariable)
        {
            int maxValue = GetMax(values);
            int minValue = GetMin(values);
            int range = maxValue - minValue;
            int maxDigit = (int)Math.Log(range, baseVariable) + 1;

            for (int d = 0; d < maxDigit; d++)
            {
                int[] buckets = new int[baseVariable];
                foreach (var num in values)
                {
                    buckets[(num - minValue) / ((int)Math.Pow(baseVariable, d)) % baseVariable]++;
                }

                int indexer = buckets[0];
                for (int i = 1; i < buckets.Length; i++)
                {
                    if (buckets[i] > 0)
                    {
                        indexer += buckets[i];
                    }
                    buckets[i] = indexer;
                }

                int[] output = new int[values.Length];
                for (int i = values.Length - 1; i > -1; i--)
                {
                    output[--buckets[(values[i] - minValue) / ((int)Math.Pow(baseVariable, d)) % baseVariable]] = values[i];
                }

                for (int i = 0; i < output.Length; i++)
                {
                    values[i] = output[i];
                }
            }

        }

        static void Main(string[] args)
        {
            //CountingSort test
            /*int[] ints = { 2, 0, 1, 2, 0, 5, 1, 5, 5, 4, -5, -9, -3 };


            CountingSort(ints);

            foreach (var num in ints)
            {
                Console.WriteLine(num);
            }*/

            //PigeonHoleSort test
            /*KeyableString[] names = { "bo", "", "b", "bo", "", "bobob", "b", "bobob", "bobob", "bob", "obobo" };


            PigeonHoleSort(names);

            foreach (var num in names)
            {
                Console.WriteLine(num.Key);
            }*/

            //BucketSort test
            /*int[] ints = { 0, 25, 13, 7, 89, 52, 3, 78, 100, 30 };

            BucketSort(ints);

            foreach (var num in ints)
            {
                Console.WriteLine(num);
            }*/

            //RadixSort test
            int[] intsR = { 100, 42, 12, 99, 884, 3, 70, 10, 50, -5 };

            RadixSort(intsR, 7);

            foreach (var num in intsR)
            {
                Console.WriteLine(num);
            }



            Console.ReadKey();
        }
    }
}
