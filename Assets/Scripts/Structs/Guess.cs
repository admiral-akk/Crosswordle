using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Structs
{
    public readonly struct Guess : IEnumerable<char>
    {
        private readonly string _guess;
        public Guess(string word)
        {
            _guess = word.ToLower();
        }

        public override int GetHashCode()
        {
            return _guess.GetHashCode();
        }

        public IEnumerator<char> GetEnumerator()
        {
            return ((IEnumerable<char>)_guess).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_guess).GetEnumerator();
        }

        public override string ToString()
        {
            return (string)this;
        }

        public int Length => _guess.Length;
        public char this[int i] => _guess[i];
        public static explicit operator Guess(string word) => new Guess(word);
        public static explicit operator string(Guess g) => g._guess;
    }
}