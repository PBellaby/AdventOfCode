namespace AdventOfCodeDay4;

public class Program
{
    static void Main(string[] args)
    {
        string[] data = File.ReadAllLines(@"C:\Users\bella\Documents\SpartaGlobal\Advent of Code\2022 Day 4\input.txt");

        List<int[]> firstRanges = new();
        List<int[]> secondRanges = new();

        foreach(string line in data)
        {
            var ranges = line.Split(',');
            firstRanges.Add(GetRangeBounds(ranges[0]));
            secondRanges.Add(GetRangeBounds(ranges[1]));
        }

        //part 1, count how many pairs have ranges where one fully contains the other
        //part 2, either range overlaps with the other at all
        int containsSum = 0;
        int overlapsSum = 0;
        for (int i = 0; i < data.Length; i++)
        {
            if (OneRangeContainsOther(firstRanges[i], secondRanges[i])) containsSum++;
            if (OneRangeOverlapsOther(firstRanges[i], secondRanges[i])) overlapsSum++;
        }
        Console.WriteLine(containsSum);
        Console.WriteLine(overlapsSum);
    }

    private static bool OneRangeContainsOther(int[] range1, int[] range2)
    {
        return
            (range1[0] <= range2[0] && range1[1] >= range2[1]) ||
            (range2[0] <= range1[0] && range2[1] >= range1[1]);
    }

    private static bool OneRangeOverlapsOther(int[] range1, int[] range2)
    {
        return
            !((range1[1] < range2[0]) ||
            (range2[1] < range1[0]));
    }

    private static int[] GetRangeBounds(string range)
    {
        int[] bounds = new int[2];
        string[] stringBounds = range.Split("-");
        bounds[0] = int.Parse(stringBounds[0]);
        bounds[1] = int.Parse(stringBounds[1]);
        return bounds;
    }
}