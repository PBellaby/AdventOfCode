namespace AdventOfCodeDay3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines(@"C:\Users\bella\Documents\SpartaGlobal\Advent of Code\2022 Day 3\input.txt");
            List<char> sameLetters = new();

            foreach (string line in data)
            {
                //split into two halves
                string firstCompartment = line.Remove(line.Length / 2);
                string secondCompartment = line.Remove(0, line.Length / 2);

                //find which letter turns up in both
                sameLetters.Add(GetSameLetter(firstCompartment, secondCompartment));
            }

            //map letters to priority values
            int prioritySum = GetPrioritySum(sameLetters);

            Console.WriteLine("The sum of all priorities is:");
            Console.WriteLine(prioritySum);


            //Part 2
            List<char> badgeLetters = new();
            //loop through groups
            for(int i = 0; i < data.Length; i+=3)
            {
                //which letter is common to all three lines?
                badgeLetters.Add(GetCommonLetter(data[i], data[i + 1], data[i + 2]));
            }

            int badgePrioritySum = GetPrioritySum(badgeLetters);
            Console.WriteLine("The sum of all badge priorities is:");
            Console.WriteLine(badgePrioritySum);
        }

        private static char GetCommonLetter(string elf1, string elf2, string elf3)
        {
            List<char> commonLetters = new();

            foreach (char c in elf2)
            {
                if (elf1.Contains(c)) commonLetters.Add(c);
            }
            foreach (char c in elf3)
            {
                if (commonLetters.Contains(c)) return c;
            }

            return '\0';
        }

        private static int GetPrioritySum(List<char> sameLetters)
        {
            int prioritySum = 0;
            foreach (char c in sameLetters)
            {
                //in the range a - z
                if (c >= 97 && c <= 122) prioritySum += ((int)c - 96);

                //in the range A - Z
                if (c >= 65 && c <= 90) prioritySum += ((int)c - 38);
            }

            return prioritySum;
        }

        private static char GetSameLetter(string firstCompartment, string secondCompartment)
        {
            foreach (char c in firstCompartment)
            {
                if (secondCompartment.Contains(c)) return c;
            }
            return '\0';
        }
    }
}