using NUnit.Framework;

public class WordDictionaryTest
{
    [Test]
    public void WordDictionaryTestValidWords()
    {
        var dictionary = WordDictionary.GenerateDictionary();

        Assert.IsTrue(dictionary.IsValidWord("ABOUT"));
        Assert.IsTrue(dictionary.IsValidWord("LIGHT"));
        Assert.IsTrue(dictionary.IsValidWord("FIRES"));
        Assert.IsTrue(dictionary.IsValidWord("BOMBS"));
        Assert.IsTrue(dictionary.IsValidWord("TASER"));
    }
    [Test]
    public void WordDictionaryTestInvalidWords()
    {
        var dictionary = WordDictionary.GenerateDictionary();

        Assert.IsFalse(dictionary.IsValidWord(""));
        Assert.IsFalse(dictionary.IsValidWord("AAAAA"));
        Assert.IsFalse(dictionary.IsValidWord("BANNA"));
    }
    
    [Test]
    public void WordDictionaryTestGenerateWords()
    {
        var dictionary = WordDictionary.GenerateDictionary();

        var word = dictionary.GetRandomWord();

        Assert.IsTrue(dictionary.IsValidWord(word));
    }
}
