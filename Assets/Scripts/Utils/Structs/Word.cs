using System.Text.RegularExpressions;

public readonly struct Word 
{
    private readonly string _word;

    public Word(string word)
    {
        word = Regex.Replace(word, "[^a-zA-Z]", string.Empty);
        _word = word.ToUpper();
    }

    public override int GetHashCode()
    {
        return _word.GetHashCode();
    }

    public override string ToString()
    {
        return _word;
    }

    public override bool Equals(object obj)
    {
        return _word.Equals(((Word)obj)._word);
    }

    public Word RemoveEnd()
    {
        return new Word(_word.Substring(0, _word.Length - 1));
    }

    public int Length => _word.Length;
    public static explicit operator string(Word w) => w.ToString();
    public static implicit operator Word(string s) => new Word(s);
    public static Word operator +(Word word, char c) => word + new Word(c.ToString());
    public static Word operator +(char c, Word word) => word + new Word(c.ToString());
    public static Word operator +(Word word, Word other) => word.ToString() + other.ToString();


    public bool Contains(char c)
    {
        return _word.Contains(c.ToString());
    }
    public char this[int i] => _word[i];
}