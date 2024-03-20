var textFile = "/Users/juliamelnykovych/RiderProjects/assignment4/kse.txt";
var text = File.ReadAllText(textFile);

var dictOfChars = new Dictionary<char, int>();
foreach (char ch in text)
{
    if (dictOfChars.ContainsKey(ch))
    {
        dictOfChars[ch] += 1;
    }
    else
    {
        dictOfChars[ch] = 1;
    }
}

var minHeap = new SortedSet<Node>();
foreach (var data in dictOfChars)
{
    var node = new Node()
    {
        Symbol = data.Key.ToString(),
        Frequency = data.Value,
        LeftChild = null,
        RightChild = null
    };
    minHeap.Add(node);
}

while (minHeap.Count > 1)
{
    var min1 = GetMinNode(minHeap);
    var min2 = GetMinNode(minHeap);

    var commonNode = new Node()
    {
        Symbol = min1.Symbol.Replace("\n", "\\n") + "_" + min2.Symbol.Replace("\n", "\\n"),
        Frequency = min1.Frequency + min2.Frequency,
        LeftChild = min1,
        RightChild = min2
    };
    minHeap.Add(commonNode);
}


var root = minHeap.Min;

var letterFreq = new Dictionary<char, int>();
foreach (var pair in dictOfChars)
{
    var path = root.Search(pair.Key.ToString(), new List<int>());
    Console.Write($"{pair.Key.ToString().Replace("\n", "\\n").Replace(" ", "SPACE")}\t");
    if (path != null)
    {
        foreach (var bit in path)
        {
            Console.Write(bit);
        }
    }
    Console.WriteLine();
}

static Node GetMinNode(SortedSet<Node> minHeap)
{
    var minNode = minHeap.Min;
    minHeap.Remove(minNode);
    return minNode;
}
public class Node : IComparable<Node>
{
    public string Symbol;
    public int Frequency;
    public Node LeftChild;
    public Node RightChild;

    public int CompareTo(Node other)
    {
        return Frequency.CompareTo(other.Frequency);
    }

    public List<int> Search(string symbol, List<int> prevPath)
    {
        if (RightChild == null && LeftChild == null)
        {
            if (symbol == this.Symbol)
            {
                return prevPath;
            }
            return null;
        }

        List<int> path = null;
        if (LeftChild != null)
        {
            var leftPath = new List<int>(prevPath); 
            leftPath.Add(0); 
            path = LeftChild.Search(symbol, leftPath);
        }

        if (path != null)
        {
            return path;
        }

        if (RightChild != null)
        {
            var rightPath = new List<int>(prevPath);
            rightPath.Add(1);
            path = RightChild.Search(symbol, rightPath);
        }
        return path;
    }
}

