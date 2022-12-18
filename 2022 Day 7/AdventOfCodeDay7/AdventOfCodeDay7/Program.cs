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
        string output = $"{filepath}{name}\n\t";
        foreach(var content in contents)
        {
            output += content.ToString();
        }
        return output;
    }

    public bool Exists (string name)
    {
        if (name == this.name) return true;
        foreach(var content in contents)
        {
            if (content.Exists(name)) return true;
        }
        return false;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        string[] data = File.ReadAllLines(@"C:\Users\bella\Documents\SpartaGlobal\Advent of Code\2022 Day 7\input.txt");

        // measure the size of directories by all the files contained within (inc. contained directories)
        // sum up the sizes of all directories that are at most 100_000 in size

        string curDir = @"/";
        string prevDir = curDir;

        FileSystemItem fs = new(curDir, "");

        Console.WriteLine(fs);

        //process commands to build filesystem (fs)
        foreach(var line in data)
        {
            //change directory
            if(line.Contains("$ cd"))
            {
                string newDir = line.Remove(0, 5);
                //swap prev and current directory if "$ cd .."
                if (newDir == "..") (prevDir, curDir) = (curDir, prevDir);
                else if (newDir == "/")
                {
                    prevDir = curDir;
                    curDir = "/";
                }
                else curDir = curDir + "/" + newDir;
            }
            // "$ ls" can be ignored, file does this for us essentially

            else if(line.ToLower().StartsWith("dir"))
            {
                //add directory to filesystem
                string name = line.Remove(0, 4);
                FileSystemItem newDir = new(curDir, name);
                newDir.isDir = true;

                //find current directory using curDir string, add new item to that particular FileSystemItem
                // TODO
            }

            else
            {
                //add new file to fs

                // TODO
            }
        }
    }
}