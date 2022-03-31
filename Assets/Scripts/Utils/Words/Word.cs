public readonly struct Word 
{
    private readonly string _word;

    public Word(string word)
    {
        _word = word;
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

    public static explicit operator string(Word w) => w._word.ToUpper();
    public static explicit operator Word(string s) => new Word(s.ToUpper());
}