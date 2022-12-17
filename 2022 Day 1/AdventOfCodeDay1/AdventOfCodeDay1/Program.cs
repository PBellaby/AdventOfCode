namespace AdventOfCodeDay1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string[] data = File.ReadAllLines(@"C:\Users\bella\Documents\SpartaGlobal\Advent of Code\2022 Day 1\input.txt");

            List<int> caloriesPerElf = new() { 0 };
            int index = 0;

            foreach(string line in data)
            {
                if (line == "")
                {
                    caloriesPerElf.Add(0);
                    index++;
                }
                else caloriesPerElf[index] += int.Parse(line);
            }

            caloriesPerElf.Sort();
            caloriesPerElf.Reverse();
            Console.WriteLine(caloriesPerElf[0]);

            int totalCaloriesHeldByTop3Elves = caloriesPerElf[0] + caloriesPerElf[1] + caloriesPerElf[2];
            Console.WriteLine(totalCaloriesHeldByTop3Elves);
        }
    }
}