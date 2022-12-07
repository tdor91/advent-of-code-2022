class File
{
    public File(string name, int size)
    {
        Name = name;
        Size = size;
    }

    public string Name { get; }
    public int Size { get; }

    public override string ToString() => $"File: {Name} ({Size})";
}
