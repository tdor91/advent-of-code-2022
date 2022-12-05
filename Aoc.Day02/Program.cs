Dictionary<string, int> pickValue = new()
{
    ["A"] = 1,
    ["B"] = 2,
    ["C"] = 3,
    ["X"] = 1,
    ["Y"] = 2,
    ["Z"] = 3,
};

var lines = await File.ReadAllLinesAsync("input.txt");

var score = 0;
foreach (var line in lines)
{
    var other = line.Split(" ")[0];
    var you = line.Split(" ")[1];

    score += CalcRoundScorePart1(other, you);
}

Console.WriteLine($"Part 1: Total score is {score}.");

score = 0;
foreach (var line in lines)
{
    var other = line.Split(" ")[0];
    var you = line.Split(" ")[1];

    score += CalcRoundScorePart2(other, you);
}

Console.WriteLine($"Part 2: Total score is {score}.");

int CalcRoundScorePart1(string other, string you)
{
    var result = pickValue[you] - pickValue[other];

    if (pickValue[you] == pickValue[other])
    {
        // draw
        return 3 + pickValue[you];
    }
    else if (you == "X" && other == "B" || you == "Y" && other == "C" || you == "Z" && other == "A")
    {
        // lose
        return 0 + pickValue[you];
    }
    else
    {
        // win
        return 6 + pickValue[you];
    }
}

int CalcRoundScorePart2(string other, string desiredOutcome)
{
    if (desiredOutcome == "Y")
    {
        // draw
        return 3 + pickValue[other];
    }
    else if (desiredOutcome == "X")
    {
        // lose
        return other switch
        {
            "A" => 0 + pickValue["Z"],
            "B" => 0 + pickValue["X"],
            "C" => 0 + pickValue["Y"],
        };
    }
    else {
        // win
        return other switch
        {
            "A" => 6 + pickValue["Y"],
            "B" => 6 + pickValue["Z"],
            "C" => 6 + pickValue["X"],
        };
    }
}
