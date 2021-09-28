using System;
using System.Collections.Generic;

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

        static bool CheckAndPrintMultipleOf13(int value)
        {
            if ((value % 11) == 0)
            {
                Console.WriteLine("Bong");
                return true;
            }

            return false;
        }
    
        static void PrintCorrectFizz(int value)
        {
            // Check for 'Bong' and then return if needed
            if (CheckAndPrintMultipleOf13(value))
                return;
            
            bool hasPrintedWord = false;
            
            // Check against all Fizz/Buzz entries in array
            foreach (var entry in conversion)
            {
                if ((value % entry.Item1) == 0)
                {
                    Console.Write(entry.Item2);
                    hasPrintedWord = true;
                }
            }
            
            if (!hasPrintedWord)
                Console.Write(value);
            
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            for (int i = 1; i <= 100; i++)
            {
                PrintCorrectFizz(i);
            }
        }
    }
}