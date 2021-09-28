using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FizzBuzz
{
    // Fizz function takes in list of current FizzBuzz objects
    using outputList = List<string>;
    using FizzAction = Tuple<int, Action<List<string>>>;

    class Fizzer : IEnumerable
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

        private int m_Iterations;

        public Fizzer(int iterations)
        {
            m_Iterations = iterations;
        }

        public string GetFizz(int value)
        {
            var output = new List<string>();
            
            // Apply each action in the array
            foreach (FizzAction act in actions)
                if (value % act.Item1 == 0)
                    act.Item2.Invoke(output);

            if (output.Count == 0)
                return value.ToString();
            else
                return string.Join("", output);
        }

        public static int GetUserInputIterations()
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

        public void ActivateActions(int[] actionsToUse)
        {
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                if (!actionsToUse.Contains(actions[i].Item1))
                    actions.RemoveAt(i);
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 1; i <= m_Iterations; i++)
            {
                yield return GetFizz(i);
            }
        }
    }

    class Program
    {
        private static void Main(string[] args)
        {
            int totalIterations = Fizzer.GetUserInputIterations();
            var fizzbuzz = new Fizzer(totalIterations);
            
            fizzbuzz.ActivateActions(args.Select(int.Parse).ToArray());

            foreach (var it in fizzbuzz)
                Console.WriteLine(it);
        }
    }
}