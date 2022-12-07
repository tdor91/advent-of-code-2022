class Directory
{
    public Directory(string name, Directory parent)
    {
        Name = name;
        Parent = parent;
        SubDirectories = new();
        Files = new();
    }

    public string Name { get; init; }
    public Directory Parent { get; }
    public List<Directory> SubDirectories { get; }
    public List<File> Files { get; }

    public override string ToString() => $"Directory: {Name}";
}
