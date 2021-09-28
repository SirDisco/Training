using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace FizzBuzzOther
{
    // Fizz function takes in list of current FizzBuzz objects
    using outputList = List<string>;
    using FizzAction = Tuple<int, Action<List<string>>>;

    class Program
    {
        // Prints "Fizz" on mod 3
        private static void Fizz(outputList output) { output.Add("Fizz"); }
        
        // Prints "Buzz" on mod 5
        private static void Buzz(outputList output) { output.Add("Buzz"); }
        
        // Prints "Bang" on mod 7
        private static void Bang(outputList output) { output.Add("Bang"); }
        
        // Prints only "Bong" on mod 11
        private static void Bong(outputList output) { output.Clear(); output.Add("Bong"); }
        
        // Prints "Fezz" before the first 'B' on mod 13
        private static void Fezz(outputList output)
        {
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
        
        // Swaps the order of actions on mod 17
        private static void Reverse(outputList output) { output.Reverse(); }
        
        // Actions to perform (order matters)
        private static List<FizzAction> actions = new List<FizzAction>()
        {
            Tuple.Create<int, Action<List<string>>>(3, Fizz),
            Tuple.Create<int, Action<List<string>>>(5, Buzz),
            Tuple.Create<int, Action<List<string>>>(7, Bang),
            
            Tuple.Create<int, Action<List<string>>>(13, Fezz),
            Tuple.Create<int, Action<List<string>>>(17, Reverse),
            Tuple.Create<int, Action<List<string>>>(11, Bong)
        };

        private static void PrintFizz(int value)
        {
            var output = new List<string>();
            
            // Apply each action in the array
            foreach (FizzAction act in actions)
                if (value % act.Item1 == 0)
                    act.Item2.Invoke(output);
            
            if (output.Count == 0)
                Console.WriteLine(value);
            else
                Console.WriteLine(string.Join("", output));
        }

        private static int GetUserInputIterations()
        {
            bool isValid = false;
            int value = 0;

            while (!isValid)
            {
                Console.Write("Enter number of iterations: ");
                string input = Console.ReadLine();

                isValid = int.TryParse(input, out value);
                
                if (!isValid)
                    Console.WriteLine("Invalid input, try again!");
            }

            return value;
        }

        private static void ActivateActions(int[] actionsToUse)
        {
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                if (!actionsToUse.Contains(actions[i].Item1))
                    actions.RemoveAt(i);
            }
        }

        private static void Main(string[] args)
        {
            ActivateActions(args.Select(int.Parse).ToArray());
            int totalIterations = GetUserInputIterations();
            
            for (int i = 1; i <= totalIterations; i++)
                PrintFizz(i);
        }
    }
}

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

        static void Main2(string[] args)
        {
            // Get user input for total numbers to print
            bool valid = false;
            int max = 0;
            
            while (!valid)
            {
                Console.Write("Enter total numbers: ");
                string input = Console.ReadLine();
                
                if (!int.TryParse(input, out max))
                    Console.WriteLine("Invalid Input");
                else
                    valid = true;
            }

            for (int i = 1; i <= max; i++)
                PrintCorrectFizz(i);
        }
    }
}