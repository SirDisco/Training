using System;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i <= 100; i++)
            {
                bool hasWritten = false;

                if (i % 3 == 0)
                {
                    Console.Write("Fizz");
                    hasWritten = true;
                }
                
                if (i % 5 == 0)
                {
                    Console.Write("Buzz");
                    hasWritten = true;
                }
                
                if (!hasWritten)
                    Console.Write(i);
                
                Console.WriteLine();
            }
        }
    }
}