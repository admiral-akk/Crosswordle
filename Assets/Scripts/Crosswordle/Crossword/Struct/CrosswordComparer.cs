using System.Collections.Generic;
using UnityEngine;

public class CrosswordComparer : IComparer<CrosswordData>
{
    public int Compare(CrosswordData crossword1, CrosswordData crossword2)
    {
        var (xDim, yDim) = (crossword1.xDim, crossword1.yDim);
        var (xDimNew, yDimNew) = (crossword2.xDim, crossword2.yDim);
        if (Mathf.Max(xDimNew, yDimNew) < Mathf.Max(xDim, yDim))
        {
            return 1;
        }
        else if (Mathf.Max(xDimNew, yDimNew) == Mathf.Max(xDim, yDim))
        {
            if (xDimNew < xDim && yDimNew <= yDim)
            {
                return 1;
            }
            if (xDimNew <= xDim && yDimNew < yDim)
            {
                return 1;
            }
            return 0;
        } else
        {
            return -1;
        }
    }
}
