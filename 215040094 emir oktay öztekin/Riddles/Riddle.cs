namespace DGD203
{
    internal class Riddle
    {
        #region REFERENCES

        public Player Player { get; private set; }
        public List<RiddleAnswer> Answers { get; private set; }

        private Location _location;
        private bool _hasAnswer = false;
        private bool _runeChoiceMade = false;
        public bool RiddleCompleted { get; private set; }

        #endregion

        #region VARIABLES

        private const string riddleQuestion = "I speak without a mouth and hear without ears. I have no body, but I come alive with the wind. What am I?";
        private bool _isOngoing;

        private string _playerInput;

        #endregion

        #region CONSTRUCTOR

        public Riddle(Game game, Location location)
        {
            Player = game.Player;
            _isOngoing = false;

            Answers =
            [
                new RiddleAnswer("Shadow", false),
                new RiddleAnswer("Whisper", false),
                new RiddleAnswer("Reflection", false),
                new RiddleAnswer("Silence", false),
            ];

            _location = location;
        }

        #endregion


        #region METHODS

        #region Initialization & Loop

        public void StartCombat()
        {
            _isOngoing = true;

            while (_isOngoing)
            {
                GetInput();
                ProcessInput();

                if (!_isOngoing) break;

                CheckPlayerPulse();
            }
        }

        private void GetInput()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"There is an old man standing in front of you.");
            Console.ForegroundColor = ConsoleColor.Gray;
            if (Player.Inventory.HasItem(Item.Rune) && !_runeChoiceMade)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"The old man smiles as he sees the rune you picked up sparkle in your hand. He asks for the Rune");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("[1] Give the old man the rune");
                Console.WriteLine("[2] Hold onto the rune");

                while (!_runeChoiceMade)
                {
                    var choice = Console.ReadLine();
                    _runeChoiceMade = ProcessRunChoice(choice);
                }
            }
            
            Console.WriteLine("He asks you a riddle.");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(riddleQuestion);
            Console.ForegroundColor = ConsoleColor.Gray;

            int answerCount = 1;
            for (int i = 0; i < Answers.Count; i++)
            {
                if (Answers[i].CorrectAnswer && !_hasAnswer)
                    continue; //Don't show the correct answer if we don't have the item

                Console.WriteLine($"[{answerCount}]: {Answers[i].Answer}");
                answerCount++;
            }

            Console.WriteLine($"[{Answers.Count + 1}]: Try to flee (50% chance)");

            _playerInput = Console.ReadLine();
        }

        private void ProcessInput()
        {
            if (_playerInput == "" || _playerInput == null)
            {
                Console.WriteLine("You must provide an answer or the old man will kill you!");
                return;
            }

            ProcessRiddleChoice(_playerInput);
        }

        private bool ProcessRunChoice(string choice)
        {
            if (Int32.TryParse(choice, out int value)) // When the command is an integer
            {
                switch (value)
                {
                    case 1:
                        Console.WriteLine("The old man takes the rune from you and pockets it.");
                        Answers.Add(new RiddleAnswer("Echo", true));
                        _hasAnswer = true;
                        return true;
                    case 2:
                        Console.WriteLine("You ignore the old man and keep the rune for yourself.");
                        return true;
                    default:
                        Console.WriteLine("That is not a valid choice.");
                        return false;
                }
            }
            else // When the command is not an integer
            {
                Console.WriteLine("You don't make any sense. Quit babbling, or the old man will kill you!");
                return false;
            }
        }

         private void ProcessRiddleChoice(string choice)
        {
            if (Int32.TryParse(choice, out int value)) // When the command is an integer
            {
                if (value > Answers.Count + 1)
                {
                    Console.WriteLine("That is not a valid choice");
                }
                else
                {
                    if (value == Answers.Count + 1)
                    {
                        TryToFlee();
                    }
                    else
                    {
                        AnswerRiddle(value);
                    }
                }
            }
            else // When the command is not an integer
            {
                Console.WriteLine("You don't make any sense. Quit babbling, or the old man will kill you!");
            }
        }

        private void CheckPlayerPulse()
        {
            if (Player.Health <= 0)
            {
                EndCombat();
            }
        }

        private void EndCombat()
        {
            _isOngoing = false;
        }

        #endregion

        #region Combat

        private void TryToFlee()
        {
            Random rand = new Random();
            double randomNumber = rand.NextDouble();

            if (randomNumber >= 0.5f)
            {
                Console.WriteLine("You flee! You are a coward maybe, but a live one!");
                EndCombat();
            }
            else
            {
                Console.WriteLine("You cannot flee you must answer the riddle");
                Player.TakeDamage(1);
            }
        }

        private void AnswerRiddle(int index)
        {
            var riddleAnswer = Answers[index - 1];
            if (riddleAnswer.CorrectAnswer)
            {
                Console.WriteLine("That is the correct answer, you may live");
                RiddleCompleted = true;
                EndCombat();
            }
            else
            {
                Console.WriteLine("That is NOT the correct answer");
                Player.TakeDamage(1);
            }
        }

        #endregion

        #endregion

    }
}
