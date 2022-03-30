namespace Assets.Scripts
{
    public readonly struct Guess
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
    }
}