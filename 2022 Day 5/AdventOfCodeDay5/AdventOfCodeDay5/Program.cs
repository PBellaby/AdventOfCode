namespace AdventOfCodeDay5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //initial input parsing
            string filepath = @"C:\Users\bella\Documents\SpartaGlobal\Advent of Code\2022 Day 5\input.txt";
            var commands = ParseCommands(filepath);
            var stacks = ParseStacks(filepath);

            ExecuteCommands(commands, stacks);

            string answer = GetTopCrateFromEachCol(stacks);

            Console.WriteLine(answer);
        }

        private static string GetTopCrateFromEachCol(Stack<char>[] stacks)
        {
            string output = "";
            for (int i = 1; i < stacks.Length; i++)
            {
                output += stacks[i].Peek();
            }

            return output;
        }

        private static void ExecuteCommands(List<int[]> commands, Stack<char>[] stacks)
        {
            foreach(var command in commands)
            {
                //move command[0] crates from col command[1] to col command[2]

                //Part 1
                ////the "crane" only moves one box at a time,
                ////so the popped crate needs to be immediately pushed to the target stack
                //int num = command[0];
                //int col1 = command[1];
                //int col2 = command[2];
                //for (int i = 1; i <= num; i++)
                //{
                //    char crate = stacks[col1].Pop();
                //    stacks[col2].Push(crate);
                //}

                //Part 2
                //Now we have a crane that can hold more than one box.
                //pushing and popping from stacks reverses the order, but if the "crane" is a stack too, then the effect is good
                Stack<char> crane = new();
                int num = command[0];
                int col1 = command[1];
                int col2 = command[2];
                for (int i = 1; i <= num; i++)
                {
                    char crate = stacks[col1].Pop();
                    crane.Push(crate);
                }

                for(int i = 1; i <=num; i++)
                {
                    char crate = crane.Pop();
                    stacks[col2].Push(crate);
                }
            }
        }

        private static List<int[]> ParseCommands(string filePath)
        {
            string[] data = File.ReadAllLines(filePath);
            List<int[]> commands = new();
            bool atCommands = false;

            foreach(string line in data)
            {
                line.Trim();
                if(line == "")
                {
                    atCommands = true;
                    continue;
                }

                if (!atCommands) continue;

                //commands come in the form "move X (crates) from (col) Y to (col) Z"
                //store in array of integers {X, Y, Z} where 0 is "move X crates", 1 is "from col Y", and 2 is "to col Z"
                string[] words = line.Split(" ");
                
                int[] command = { int.Parse(words[1]), int.Parse(words[3]), int.Parse(words[5]) };
                commands.Add(command);
            }

            return commands;
        }

        private static Stack<char>[] ParseStacks(string filePath)
        {
            string[] data = File.ReadAllLines(filePath);
            List<string> crateMap = new();
            foreach (string line in data)
            {
                line.Trim();
                if (line == "") break;
                crateMap.Add(line);
            }

            //setup stacks for each col, in an array. As arrays are 0 indexed we'll have a secret "0" column that never gets anything,
            //saving us from doing integer manipulation all the time
            //so stacks[1] refers to column 1, stacks[9] to column 9 etc.
            Stack<char>[] stacks = new Stack<char>[10];
            for (int i = 0; i < stacks.Length; i++)
            {
                stacks[i] = new Stack<char>();
            }

            //final line of crateMap is just the column labels 1 - 9, that can be removed
            crateMap.RemoveAt(crateMap.Count - 1);
            //and reverse it so that stacks are made from the bottom
            crateMap.Reverse();

            //and push the char that represents a crate to the appropriate stack
            foreach (string line in crateMap)
            {
                //each column is 4 characters wide, '[' 'name' ']' ' ',
                //looping in steps of 4 allows us to get the char that represents the crate easily
                int col = 0;
                for (int i = 0; i < line.Length; i += 4)
                {
                    col++;
                    char name = line[i + 1];  //i is just the closing bracket
                    //don't count empty spaces
                    if (name == ' ') continue;
                    stacks[col].Push(name);
                }
            }

            return stacks;
        }
    }
}