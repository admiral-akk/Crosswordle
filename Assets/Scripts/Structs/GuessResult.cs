using System.Linq;

namespace Assets.Scripts.Structs
{
    public readonly struct GuessResult
    {
        public enum ResultType
        {
            NotInAnswer,
            WrongPosition,
            Correct
        }

        public readonly Guess Guess;
        public readonly ResultType[] Results;

        public GuessResult(Guess guess, Guess answer)
        {
            Guess = guess;
            var results = new ResultType[guess.Length];
            for (var i = 0; i < guess.Length; i++)
            {
                if (guess[i] == answer[i])
                {
                    results[i] = ResultType.Correct;
                }
            }
            for (var i = 0; i < guess.Length; i++)
            {
                if (Enumerable.Range(0, guess.Length).Any(index => results[index] != ResultType.Correct && guess[i] == answer[index]))
                {
                    results[i] = ResultType.WrongPosition;
                }
            }
            Results = results;
        }
    }
}