public readonly struct Word 
{
    private readonly string _word;

    public Word(string word)
    {
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
    public static explicit operator string(Word w) => w._word.ToUpper();
    public static explicit operator Word(string s) => new Word(s.ToUpper());
    public static Word operator +(Word word, char c) => new Word(word._word + c);
    public static Word operator +(char c, Word word) => new Word(c + word._word);

    public char this[int i] => _word[i];
}