namespace AdventOfCodeDay2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines(@"C:\Users\bella\Documents\SpartaGlobal\Advent of Code\2022 Day 2\input.txt");

            Part1(data);

            Part2(data);
        }

        private static void Part1(string[] data)
        {
            //A is rock is 1
            //B is paper is 2
            //C is scissors is 3

            //X is rock is 1
            //Y is paper is 2
            //Z is scissors is 3

            //loss is 0
            //draw is 3
            //win is 6

            int score = 0;
            foreach (var line in data)
            {
                score += GetRoundPart1(line);
            }

            Console.WriteLine("Part 1");
            Console.WriteLine("The total score of the strategy guide is:");
            Console.WriteLine(score);
        }

        private static int GetRoundPart1(string line)
        {
            int score = 0;
            switch (line[2]) 
            {
                case 'X':
                    score += 1;
                    break;
                case 'Y':
                    score += 2;
                    break;
                case 'Z':
                    score += 3;
                    break;
            }

            string[] winningMoves = { "C X", "A Y", "B Z" };
            if (LineContainsAny(line, winningMoves)) score += 6;

            string[] drawingMoves = { "A X", "B Y", "C Z" };
            if (LineContainsAny(line, drawingMoves)) score += 3;

            return score;
        }

        private static bool LineContainsAny(string line, string[] winningMoves)
        {
            bool doesContain = false;
            
            foreach (var move in winningMoves)
            {
                if (line.Contains(move)) return true;
            }
            return doesContain;
        }

        private static void Part2(string[] data)
        {
            //A is enemy rock
            //B is enemy paper
            //C is enemy scissors

            //my rock is 1
            //my paper is 2
            //my scissors is 3

            //X is I lose, 0
            //Y is I draw, 3
            //Z is I win, 6

            int score = 0;

            foreach(var line in data)
            {
                if (line[2] == 'Y') score += 3;
                if (line[2] == 'Z') score += 6;

                score += GetScoreForMyHand(line);
            }

            Console.WriteLine("Part 2");
            Console.WriteLine("The total score of the strategy guide is:");
            Console.WriteLine(score);
        }

        private static int GetScoreForMyHand(string line)
        {
            //rock
            if (line[0] == 'A')
            {
                switch(line[2])
                {
                    case 'X':
                        //must lose, scissors
                        return 3;
                    case 'Y':
                        //must draw, rock
                        return 1;
                    case 'Z':
                        //must win, paper
                        return 2;
                }
            }
            //paper
            else if (line[0] == 'B')
            {
                switch (line[2])
                {
                    case 'X':
                        //must lose, rock
                        return 1;
                    case 'Y':
                        //must draw, paper
                        return 2;
                    case 'Z':
                        //must win, scissors
                        return 3;
                }
            }
            //scissors
            else
            {
                switch (line[2])
                {
                    case 'X':
                        //must lose, paper
                        return 2;
                    case 'Y':
                        //must draw, scissors
                        return 3;
                    case 'Z':
                        //must win, rock
                        return 1;
                }
            }
            return 0;
        }
    }
}