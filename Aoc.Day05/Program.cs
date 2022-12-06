using System.Text.RegularExpressions;

var lines = await File.ReadAllLinesAsync("input.txt");
var stackLines = lines.Where(line => line.Contains("["));
var numberOfStacks = lines.First(line => line.Trim().StartsWith("1")).Split("   ").Select(int.Parse).Last();
var operations = lines.SkipWhile(line => !line.StartsWith("move")).Select(line => new Operation(line)).ToList();

var stackGroup1 = new StackGroup(numberOfStacks, stackLines);
operations.ForEach(stackGroup1.Act1);
var result1 = stackGroup1.ReadResult();
Console.WriteLine($"Part 1: {result1}");

var stackGroup2 = new StackGroup(numberOfStacks, stackLines);
operations.ForEach(stackGroup2.Act2);
var result2 = stackGroup2.ReadResult();

Console.WriteLine($"Part 2: {result2}");


class StackGroup
{
    private List<Stack<char>> stacks;

    public StackGroup(int numberOfStacks, IEnumerable<string> stackLines)
    {
        stacks = new List<Stack<char>>();
        for (int i = 0; i < numberOfStacks + 1; i++) // create an additional stack but start at index 1 later
        {
            stacks.Add(new Stack<char>());
        }

        foreach (var stackLine in stackLines.Reverse())
        {
            int stackPos = 1;
            for (int i = 1; i < stackLine.Length; i += 4)
            {
                var c = stackLine[i];
                if (c != ' ')
                {
                    stacks[stackPos].Push(c);
                }
                stackPos++;
            }
        }
    }

    public void Act1(Operation operation)
    {
        for (int i = 0; i < operation.Count; i++)
        {
            var c = stacks[operation.From].Pop();
            stacks[operation.To].Push(c);
        }
    }

    public void Act2(Operation operation)
    {
        List<char> chars = new();
        for (int i = 0; i < operation.Count; i++)
        {
            chars.Insert(0, stacks[operation.From].Pop());
        }
        chars.ForEach(stacks[operation.To].Push);
    }

    public string ReadResult()
    {
        var chars = stacks.Skip(1).Select(s => s.Peek());
        return string.Concat(chars);
    }
}

class Operation
{
    private Regex regex = new Regex(@"move (?<count>\d+) from (?<from>\d+) to (?<to>\d+)");

    public Operation(string operation)
    {
        var match = regex.Match(operation);
        Count = int.Parse(match.Groups["count"].Value);
        From = int.Parse(match.Groups["from"].Value);
        To = int.Parse(match.Groups["to"].Value);
    }

    public int Count { get; init; }
    public int From { get; init; }
    public int To { get; init; }
}
