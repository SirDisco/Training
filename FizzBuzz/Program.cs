using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace FizzBuzz
{
    class Program
    {
        private static Tuple<int, string>[] conversion =
        {
            Tuple.Create(3, "Fizz"),
            Tuple.Create(5, "Buzz"),
            Tuple.Create(7, "Bang")
        };

        static void AddFezzToOutput(List<string> output)
        {
            // Insert Fezz before first 'B'
            for (int i = 0; i < output.Count; i++)
            {
                if (output[i].StartsWith('B'))
                {
                    output.Insert(i, "Fezz");
                    return;
                }
            }
            
            // If no 'B' found then append to output
            output.Add("Fezz");
        }

        static void PrintCorrectFizz(int value)
        {
            var output = new List<string>();

            // Check against all Fizz/Buzz entries in array
            foreach (var entry in conversion)
                if ((value % entry.Item1) == 0)
                    output.Add(entry.Item2);
            
            // Check for multiple of 13
            if (value % 13 == 0)
                AddFezzToOutput(output);

            // Reverse array if multiple of 17
            if (value % 17 == 0)
                output.Reverse();
            
            // Wipe array and print Bong if multiple of 11
            if (value % 11 == 0)
            {
                output.Clear();
                output.Add("Bong");
            }

            // Print number if nothing else exists
            if (output.Count == 0)
                output.Add(value.ToString());
            
            Console.WriteLine(string.Join("", output));
        }

        static void Main(string[] args)
        {
            for (int i = 1; i <= 256; i++)
                PrintCorrectFizz(i);
        }
    }
}