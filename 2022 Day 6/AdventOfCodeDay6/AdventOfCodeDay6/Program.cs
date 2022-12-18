namespace AdventOfCodeDay6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string data = File.ReadAllText(@"C:\Users\bella\Documents\SpartaGlobal\Advent of Code\2022 Day 6\input.txt");

            int marker = 0;
            //find the index after the first 14 letters that are all different
            const int range = 14;
            for(int i = 0; i < data.Length - range; i++)
            {
                //check if characters in range are unique
                if (UniqueChars(data.Substring(i, range).ToCharArray()))
                {
                    marker = i + range;
                    break;
                }
            }

                Console.WriteLine(marker);
        }

        //Part 1
        private static bool FourUniqueChars(char c1, char c2, char c3, char c4)
        {
            return 
                (
                    c1 != c2 &&
                    c1 != c3 &&
                    c1 != c4 &&
                    c2 != c3 &&
                    c2 != c4 &&
                    c3 != c4
                );
        }
        //Part 2
        private static bool UniqueChars(char[] chars)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                for (int j = i + 1; j < chars.Length; j++)
                {
                    if (chars[i] == chars[j]) return false;
                }
            }
            return true;
        }

    }
}