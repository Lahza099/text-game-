namespace DGD203
{
    internal class RiddleAnswer
    {
        public string Answer { get; protected set; }
        public bool CorrectAnswer { get; protected set; }

        public RiddleAnswer(string text,  bool correctAnswer)
        {
            Answer = text;
            CorrectAnswer = correctAnswer;
        }
    }
}
