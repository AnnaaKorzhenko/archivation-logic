var testFile = "/Users/juliamelnykovych/RiderProjects/assignment4/kse.txt";

var text = File.ReadAllText(testFile);
var freq_char = new Dictionary<char, int>();
foreach (var letter in text)
{
    if (freq_char.ContainsKey(letter)) freq_char[letter] += 1;
    else freq_char[letter] = 1;
}
Console.WriteLine("Finish");

//Priority Queue 
//Min Heap 

var min_heap = new MinHeap();
foreach (var pair in freq_char)
{
    var node = new Node()
    {
        Symbol = pair.Key.ToString(),
        Frequeue = pair.Value,
        LeftCgild = null,
        RightChild = null 
    };
    min_heap.Add(pair);

}

min_heap.Print();
while (min_heap.data.Count > 1)
{
    var min_1 = min_heap.Pop();
    var min_2 = min_heap.Pop();
    
}
var min_1 = min_heap.Pop();
min_heap.Print();
var min_2 = min_heap.Pop();

var command_node = new Node()
    {
        Symbol = min_1.Sympol.Replace('\n' , "NewLine"+ min_2.Sympol,
        Frequeue = min_1.Frequency + min_2.Frequency,
        LeftCgild = min_1,
        RightChild = min_2 
    };
min_heap.Add(command_node);


Console.WriteLine($"{min_1.Sympol} {min_1.Frequency}");
Console.WriteLine($"{min_2.Sympol} {min_2.Frequency}");

Console.WriteLine();