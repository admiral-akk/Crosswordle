
using System.Text;
using UnityEngine;

public readonly struct CrosswordData 
{
    public readonly WordData[] Words;
    public readonly int xDim, yDim;

    private static bool GenerateCrossword(WordData[] output, string[] words, int currentIndex)
    {
        // Base case: we're out of words to add
        if (currentIndex == words.Length)
            return true;

        // Inductive case: try every position and see if it's a valid placement. If so, increment the index and go one level deeper.
        for (var i = 0; i < currentIndex; i++)
        {
            // Find all of the matching letters.
            var startPositions = output[i].GetStartPositions(words[currentIndex]);
            for (var j = 0; j < startPositions.Count; j++)
            {
                output[currentIndex] = new WordData(words[currentIndex], startPositions[j], !output[i].IsHorizontal);
                var legal = true;
                for (var k = 0; k < currentIndex; k++)
                {
                    legal &= output[currentIndex].IsLegal(output[k]);
                }
                if (legal)
                {
                    var success = GenerateCrossword(output, words, currentIndex + 1);
                    if (success)
                    {
                        return true;
                    }
                }
            }

        }

        // If we run out of things to try, notify the caller.
        return false;
    }

    public static CrosswordData GenerateCrossword(string[] words)
    {
        if (words.Length == 0)
            return new CrosswordData();
        // Words will be placed relevative to the first one, so the position and orientation can be set arbitrarily.
        var crosswords = new WordData[words.Length];
        crosswords[0] = new WordData(words[0]);
        var success = GenerateCrossword(crosswords, words, 1);
        if (!success)
            throw new System.Exception("Couldn't find valid crossword!");
        return new CrosswordData(crosswords);
    }

    private static (int,int) Normalize(WordData[] crosswords)
    {
        var (minX, minY) = (int.MaxValue, int.MaxValue);
        foreach (var word in crosswords)
        {
            minX = Mathf.Min(word.StartPosition.x, minX);
            minY = Mathf.Min(word.StartPosition.y, minX);
        }
        var offset = new Vector2Int(-minX, -minY);
        var (maxX, maxY) = (int.MinValue, int.MinValue);
        for (var i = 0; i < crosswords.Length; i++)
        {
            crosswords[i] = new WordData(crosswords[i].Word, crosswords[i].StartPosition + offset, crosswords[i].IsHorizontal);
            maxX = Mathf.Max(crosswords[i].StartPosition.x+1, maxX);
            maxY = Mathf.Max(crosswords[i].StartPosition.y+1, maxY);
            if (crosswords[i].IsHorizontal)
                maxX = Mathf.Max(crosswords[i].StartPosition.x + crosswords[i].Word.Length, maxX);
            else
                maxY = Mathf.Max(crosswords[i].StartPosition.y + crosswords[i].Word.Length, maxY);

        }
        return (maxX, maxY);
    }
    private CrosswordData(WordData[] crosswords)
    {
        (xDim, yDim) = Normalize(crosswords);
        Words = crosswords;
    }

    public override string ToString()
    {
        var chars = new char[xDim, yDim];
        for (var i = 0; i < xDim; i++)
        {
            for (var j = 0; j < yDim; j++)
            {
                chars[i, j] = '+';
            }
        }
        foreach (var word in Words)
        {
            var offset = word.IsHorizontal ? Vector2Int.right : Vector2Int.up;
            for (var i = 0; i < word.Word.Length; i++)
            {
                var pos = word.StartPosition + i * offset;
                chars[pos.x, pos.y] = word.Word[i];
            }
        }
        var sb = new StringBuilder();
        for (var i = 0; i < xDim; i++)
        {
            for (var j = 0; j < yDim; j++)
            {
                sb.Append(chars[i, j]);
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}