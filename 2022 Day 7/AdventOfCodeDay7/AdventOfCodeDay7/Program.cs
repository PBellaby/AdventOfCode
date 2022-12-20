namespace AdventOfCodeDay7;

public class FileSystemItem
{
    public readonly string filepath;  //filepath from root to the dir containing this item (may just be root)
    public readonly string name;
    public List<FileSystemItem> contents; //file system items contained within
    public bool isDir = false;
    public int size = 0;

    public FileSystemItem(string fp, string name)
    {
        filepath = fp;
        this.name = name;
        contents = new();
    }

    public override string ToString()
    {
        string output = $"{filepath}{name} - {size}\n";
        foreach(var content in contents)
        {
            output += "|_" + content.ToString();
        }
        return output;
    }

    public void PrintSize()
    {
        Console.WriteLine($"{filepath}{name} = {size}");
        foreach(var content in contents)
        {
            content.PrintSize();
        }
    }

    public FileSystemItem Find(string fp)
    {
        if (fp == filepath + name + "/") return this;

        FileSystemItem target = this;

        foreach(var content in contents)
        {
            target = content.Find(fp);
        }

        return target;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        string[] data = File.ReadAllLines(@"C:\Users\bella\Documents\SpartaGlobal\Advent of Code\2022 Day 7\input.txt");

        // measure the size of directories by all the files contained within (inc. contained directories)
        // sum up the sizes of all directories that are at most 100_000 in size

        string curDir = @"";
        Stack<string> prevDirs = new();
        prevDirs.Push(curDir);

        //fp of nothing as root, name is just "/"
        FileSystemItem fs = new("", "/");

        Console.WriteLine(fs);

        //process commands to build filesystem (fs)
        foreach(var line in data)
        {
            //change directory
            if (line.Contains("$ cd"))
            {
                string newDir = line.Remove(0, 5);
                //pop prev directory if "$ cd .."
                if (newDir == "..") curDir = prevDirs.Pop();
                else if (newDir == "/")
                {
                    prevDirs.Clear();
                    curDir = "/";
                }
                // FLAG the below is suspect >:(
                else {
                    prevDirs.Push(curDir);
                    curDir = curDir + newDir + "/"; 
                }
            }
            // "$ ls" can be ignored, file does this for us essentially
            else if (line.Contains("$ ls"));
            else if (line.ToLower().StartsWith("dir"))
            {
                //add directory to filesystem
                string name = line.Remove(0, 4);
                FileSystemItem newDir = new(curDir, name);
                newDir.isDir = true;

                //find current directory using curDir string, add new item to that particular FileSystemItem
                FileSystemItem curFSI = fs.Find(curDir);
                curFSI.contents.Add(newDir);
            }

            else
            {
                //add new file to fs
                int size = int.Parse(line.Split(" ")[0]);
                string name = line.Split(" ")[1];

                FileSystemItem curFSI = fs.Find(curDir);
                FileSystemItem newFile = new(curDir, name);
                curFSI.contents.Add(newFile);
                newFile.size = size;
                curFSI.size += size;
            }
        }

        Console.WriteLine(fs);

        //count the sizes of all directories that are at or below 100_000 in size
        int sum = CountSizes(fs);
        Console.WriteLine($"Sum {sum}");
    }

    private static int CountSizes(FileSystemItem fileSystemItem)
    {
        // >= 100_000

        int sum = 0;

        if (fileSystemItem.isDir && fileSystemItem.size <= 100000) sum += fileSystemItem.size;

        if(fileSystemItem.contents.Count > 0)
        {
            foreach (var item in fileSystemItem.contents) sum += CountSizes(item);
        }

        return sum;
    }
}