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

Heap minHeap = new Heap();
foreach (var data in dictOfChars)
{
    minHeap.Insert(new Node(data.Key, data.Value));
}

while (minHeap.Size > 1)
{
    var min1 = minHeap.ExtractMin();
    var min2 = minHeap.ExtractMin();

    var commonNode = new Node()
    {
        Symbol = min1.Symbol.Replace("\n", "\\n") + "_" + min2.Symbol.Replace("\n", "\\n"),
        Frequency = min1.Frequency + min2.Frequency,
        LeftChild = min1,
        RightChild = min2
    };
    minHeap.Insert(commonNode);
}

var root = minHeap.ExtractMin();


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
    public Node() { }

    public Node(char symbol, int frequency)
    {
        Symbol = symbol.ToString();
        Frequency = frequency;
    }
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
public class Heap
{
    private readonly List<Node> heap;

    public int Size => heap.Count;

    public Heap()
    {
        heap = new List<Node>();
    }

    private int Parent(int index) => (index - 1) / 2;
    private int LeftChild(int index) => 2 * index + 1;
    private int RightChild(int index) => 2 * index + 2;

    private void Swap(int i, int j)
    {
        (heap[i], heap[j]) = (heap[j], heap[i]);
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && heap[index].Frequency < heap[Parent(index)].Frequency)
        {
            Swap(index, Parent(index));
            index = Parent(index);
        }
    }

    private void HeapifyDown(int index)
    {
        int minIndex = index;
        int leftChild = LeftChild(index);
        int rightChild = RightChild(index);

        if (leftChild < Size && heap[leftChild].Frequency < heap[minIndex].Frequency)
        {
            minIndex = leftChild;
        }

        if (rightChild < Size && heap[rightChild].Frequency < heap[minIndex].Frequency)
        {
            minIndex = rightChild;
        }

        if (index != minIndex)
        {
            Swap(index, minIndex);
            HeapifyDown(minIndex);
        }
    }

    public void Insert(Node value)
    {
        heap.Add(value);
        HeapifyUp(Size - 1);
    }

    public Node ExtractMin()
    {
        if (Size == 0)
        {
            throw new Exception("Heap is empty. Cannot extract minimum element.");
        }

        var min = heap[0];
        heap[0] = heap[Size - 1];
        HeapifyDown(0);

        heap.RemoveAt(Size - 1);
        return min;
    }
}

