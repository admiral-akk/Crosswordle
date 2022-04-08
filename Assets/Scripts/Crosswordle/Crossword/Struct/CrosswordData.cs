
using System.Linq;
using System.Text;
using UnityEngine;

public class CrosswordData 
{
    public readonly WordData[] Words;
    public readonly int xDim, yDim;

    public int MinimumIntersectionCount
    {
        get
        {
            var intersection = new int[Words.Length];
            for (var i = 0; i < Words.Length; i++)
            {
                for (var j = i+1; j < Words.Length; j++)
                {
                    if (Words[i].IsHorizontal == Words[j].IsHorizontal)
                        continue;

                    if (Words[i].Intersect(Words[j]))
                    {
                        intersection[i]++;
                        intersection[j]++;
                    }
                }

            }
            return intersection.Min();
        }
    }
    public int CountAtMin
    {
        get
        {
            var intersection = new int[Words.Length];
            for (var i = 0; i < Words.Length; i++)
            {
                for (var j = i + 1; j < Words.Length; j++)
                {
                    if (Words[i].IsHorizontal == Words[j].IsHorizontal)
                        continue;

                    if (Words[i].Intersect(Words[j]))
                    {
                        intersection[i]++;
                        intersection[j]++;
                    }
                }

            }
            var min = intersection.Min();
            return intersection.Where(x => x == min).Count();
        }
    }
    public int MinimumHorizontalVerticalCount
    {
        get
        {
            var horizontal = 0;
            var vertical = 0;
            foreach (var word in Words)
            {
                if (word.IsHorizontal)
                    horizontal++;
                else
                    vertical++;
            }
            return Mathf.Min(horizontal,vertical);
        }
    }

    private static bool GenerateCrossword(WordData[] output, Word[] words, int currentIndex)
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

    public static CrosswordData GenerateCrossword(Word[] words)
    {
        if (words.Length == 0)
            return null;
        // Words will be placed relevative to the first one, so the position and orientation can be set arbitrarily.
        var crosswords = new WordData[words.Length];
        crosswords[0] = new WordData(words[0]);
        var success = GenerateCrossword(crosswords, words, 1);
        if (!success)
            return null;
        return new CrosswordData(crosswords);
    }

    private static (int,int) Normalize(WordData[] crosswords)
    {
        var (minX, minY) = (int.MaxValue, int.MaxValue);
        foreach (var word in crosswords)
        {
            minX = Mathf.Min(word.StartPosition.x, minX);
            minY = Mathf.Min(word.StartPosition.y, minY);
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