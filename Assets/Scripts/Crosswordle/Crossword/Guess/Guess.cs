public enum GuessLetterState
{
    None,
    Wrong,
    OnBoard,
    Complete
}
public class Guess
{
    public char[] Letters;
    public GuessLetterState[] Knowledge;
}