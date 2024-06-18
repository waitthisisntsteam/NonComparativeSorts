using System;
using System.Runtime.InteropServices;
using System.Windows.Markup;

namespace NonComparativeSorts
{
    class Program
    {
        static void CountingSort(int[] values)
        {
            int maxValue = values[0];
            int minValue = values[0];

            foreach (var num in values)
            {
                if (maxValue < num)
                {
                    maxValue = num;
                }
                if (minValue > num)
                {
                    minValue = num;
                }
            }

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
        class KeyableString : IKeyable
        {
            string myString;

            public int Key => myString.Length - (myString.Length > 0? myString[0]: 1000000);

            public static implicit operator KeyableString(string str) => new KeyableString() { myString = str };
        }

        static void Main(string[] args)
        {
            //CountingSort test
            /*int[] ints = { 2, 0, 1, 2, 0, 5, 1, 5, 5, 4, -5 };


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
            }

            Console.ReadKey();*/

            //BucketSort test

        }
    }
}
