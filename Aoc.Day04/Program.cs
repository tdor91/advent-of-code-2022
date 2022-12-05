var lines = await File.ReadAllLinesAsync("input.txt");

var completelyOverlappingAssignments = lines
    .Select(line => new Assignment(line))
    .Where(assignment => assignment.IsCompletelyOverlapping())
    .Count();

Console.WriteLine($"Part 1: Number of completely overlapping sections: {completelyOverlappingAssignments}");

var partiallyOverlappingAssignments = lines
    .Select(line => new Assignment(line))
    .Where(assignment => assignment.IsPartiallyOverlapping())
    .Count();

Console.WriteLine($"Part 2: Number of completely overlapping sections: {partiallyOverlappingAssignments}");

class Assignment
{
    private Section section1;
    private Section section2;

    public Assignment(string assignment)
    {
        var parts = assignment.Split(",");
        section1 = new Section(parts[0]);
        section2 = new Section(parts[1]);
    }

    public bool IsCompletelyOverlapping()
    {
        return section1.OverlapsCompletely(section2);
    }

    public bool IsPartiallyOverlapping()
    {
        return !section1.AreDistinct(section2); // partially overlapping if the are *not* distinct
    }
}

class Section
{
    public Section(string section)
    {
        var parts = section.Split("-");
        Start = int.Parse(parts[0]);
        End = int.Parse(parts[1]);
    }

    public int Start { get; set; }
    public int End { get; set; }
    public int Diff => End - Start;

    public bool OverlapsCompletely(Section other)
    {
        return Start >= other.Start && End <= other.End || other.Start >= Start && other.End <= End;
    }

    public bool OverlapsPartially(Section other)
    {
        return Start >= other.Start && Start <= other.End || other.Start >= Start && other.Start <= End;
    }

    public bool AreDistinct(Section other)
    {
        return Start > other.End || End < other.Start;
    }
}