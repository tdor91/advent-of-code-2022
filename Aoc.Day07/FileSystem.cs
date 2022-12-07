class FileSystem
{
    private Directory root;
    private Directory currentDirectory;

    public FileSystem()
    {
        root = new Directory("/", parent: null);
        currentDirectory = root;
    }

    public void Initialize(string[] lines)
    {
        int i = 0;
        while (i < lines.Length)
        {
            var parts = lines[i].Split(" ");

            if (parts[0] != "$")
            {
                throw new Exception("this should not happen");
            }

            if (parts[1] == "cd")
            {
                ChangeDirectory(parts[2]);
                i++;
            }
            else if (parts[1] == "ls")
            {
                i++;
                var listOutput = lines.Skip(i).TakeWhile(line => !line.StartsWith("$"));
                ParseListOutput(listOutput);
                i += listOutput.Count();
            }
        }
    }

    public Dictionary<Directory, int> CalcDirSizes()
    {
        var dirSizes = new Dictionary<Directory, int>();
        CalcDirSizesRec(root, dirSizes);
        return dirSizes;
    }

    private int CalcDirSizesRec(Directory dir, Dictionary<Directory, int> dirSizes)
    {
        var size = 0;
        foreach (var d in dir.SubDirectories)
        {
            size += CalcDirSizesRec(d, dirSizes);
        }

        size += dir.Files.Select(file => file.Size).Sum();
        dirSizes.Add(dir, size);
        return size;
    }

    private void ChangeDirectory(string targetDir)
    {
        if (targetDir == "/")
        {
            currentDirectory = root;
        }
        else if (targetDir == "..")
        {
            currentDirectory = currentDirectory.Parent;
        }
        else
        {
            currentDirectory = currentDirectory.SubDirectories.First(dir => dir.Name == targetDir);
        }
    }

    private void ParseListOutput(IEnumerable<string> listOutput)
    {
        foreach (var item in listOutput)
        {
            var parts = item.Split(" ");
            if (parts[0] == "dir")
            {
                var dir = new Directory(parts[1], currentDirectory);
                currentDirectory.SubDirectories.Add(dir);
            }
            else
            {
                var file = new File(parts[1], int.Parse(parts[0]));
                currentDirectory.Files.Add(file);
            }
        }
    }
}
