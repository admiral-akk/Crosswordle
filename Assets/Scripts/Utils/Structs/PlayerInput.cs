namespace Assets.Scripts.Structs
{
    public readonly struct PlayerInput
    {
        public enum InputType
        {
            None,
            AddLetter,
            Delete,
            SubmitWord, 
            NewGame
        }

        public readonly InputType Type;
        private readonly char _letter;
        public char Letter => _letter;

        private PlayerInput(InputType type, char letter = '0')
        {
            Type = type;
            _letter = letter;
        }

        public static PlayerInput AddLetter(char c)
        {
            return new PlayerInput(InputType.AddLetter, c);
        }
        public static PlayerInput Delete()
        {
            return new PlayerInput(InputType.Delete);
        }
        public static PlayerInput Enter()
        {
            return new PlayerInput(InputType.SubmitWord);
        }

        public static PlayerInput NewGame()
        {
            return new PlayerInput(InputType.NewGame);
        }

    }
}