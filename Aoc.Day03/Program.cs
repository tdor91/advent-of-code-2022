var lines = await File.ReadAllLinesAsync("input.txt");

var sum = 0;

foreach (var line in lines)
{
    var p1 = line.Substring(0, line.Length / 2);
    var p2 = line.Substring(p1.Length);

    var commonElement = p1.Intersect(p2).Single();
    sum += GetPriority(commonElement);
}

Console.WriteLine($"Part 1: Total sum is {sum}.");

sum = 0;
foreach (var chunk in lines.Chunk(3))
{
    var commonElement = chunk[0].Intersect(chunk[1]).Intersect(chunk[2]).Single();
    sum += GetPriority(commonElement);
}

Console.WriteLine($"Part 1: Total sum is {sum}.");

int GetPriority(char c)
{
    if ((int)c < 97) return (int)c - 64 + 26; // A == 65
    else return (int)c - 96;                  // a == 97
}