var lines = await System.IO.File.ReadAllLinesAsync("input.txt");

var fileSystem = new FileSystem();
fileSystem.Initialize(lines);
var dirSizes = fileSystem.CalcDirSizes();

var result1 = dirSizes.Where(kvp => kvp.Value <= 100_000).Select(kvp => kvp.Value).Sum();

Console.WriteLine($"Part 1: {result1}");

const int totalSpace = 70000000;
const int requiredSpace = 30000000;

var sortedDirSizes = dirSizes.OrderBy(kvp => kvp.Value);
var used = sortedDirSizes.Last().Value;
var unused = totalSpace - used;
var spaceNeeded = requiredSpace - unused;
var dirToDelete = sortedDirSizes.First(kvp => kvp.Value >= spaceNeeded);

Console.WriteLine($"Part 2: {dirToDelete.Value}");
