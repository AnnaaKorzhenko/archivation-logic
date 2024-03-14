// go through the file and create a dict with characters as keys and frequency of appearing as values
var dictOfChars = new Dictionary<char, int>();
var allText = File.ReadAllLines("../../../kse.txt");
foreach (string str in allText)
{
    foreach (char ch in str)
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
}

foreach (var ch in dictOfChars.Keys)
{
    Console.WriteLine($"{ch} : {dictOfChars[ch]}");
}
// build a tree with those nodes of key-value frequencies and links to r-child and l-child

// go through the tree and create a table with code for each character in the tree

// translate the text into this code

// add a decoding algorithm with a table in the end like %A:1%B:100%C:1000%D:100000