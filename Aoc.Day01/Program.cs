var lines = await File.ReadAllLinesAsync("input.txt");

var calsPerElf = Split(lines).ToList();
var maxCalsPerElf = calsPerElf.Select(f => f.Sum()).Max();

Console.WriteLine($"Part 1: Highest calories are {maxCalsPerElf}.");

var calsOfTopThree = calsPerElf.Select(f => f.Sum()).OrderByDescending(x => x).Take(3).Sum();
Console.WriteLine($"Part 2: Combines calories of the top 3 elves are {calsOfTopThree}.");

IEnumerable<IList<int>> Split(IEnumerable<string> lines)
{
    var result = new List<int>();
    foreach (var line in lines)
    {
        if (int.TryParse(line, out var number))
        {
            result.Add(number);
        }
        else
        {
            yield return result;
            result = new List<int>();
        }
    }
}