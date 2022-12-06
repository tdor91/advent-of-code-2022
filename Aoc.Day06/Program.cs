var lines = await File.ReadAllLinesAsync("input.txt");
var dataStream = lines.First();

var ringBuffer = new RingBuffer(4);
int i = 0;
while(!ringBuffer.HasMaxUniqueElements())
{
    ringBuffer.Add(dataStream[i]);
    i++;
}
Console.WriteLine($"Part 1: Start-of-packet marker position is {i}.");

ringBuffer = new RingBuffer(14);
i = 0;
while (!ringBuffer.HasMaxUniqueElements())
{
    ringBuffer.Add(dataStream[i]);
    i++;
}
Console.WriteLine($"Part 2: Start-of-message marker position is {i}.");

class RingBuffer
{
    private readonly int size;
    private readonly Queue<char> buffer;

    public RingBuffer(int size)
    {
        this.size = size;
        buffer = new Queue<char>(size);
    }

    public bool IsFull => buffer.Count == size;

    public void Add(char c)
    {
        if (IsFull)
        {
            buffer.Dequeue();
        }

        buffer.Enqueue(c);
    }

    public bool HasMaxUniqueElements() => buffer.GroupBy(x => x).Count() == size;
}