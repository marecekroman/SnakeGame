//This project is done by Roman Marecek for Tomas Bata University in Zlin, Czech Republic
//This code is refactored and modified fom the original code from https://codereview.stackexchange.com/questions/127515/first-c-program-snake-game

namespace SnakeGame.Model
{
    public class GameModel
    {
        // Direction constants for readability and easy management of snake's direction.
        public const string Right = "RIGHT";
        public const string Left = "LEFT";
        public const string Up = "UP";
        public const string Down = "DOWN";

        public int ScreenWidth;
        public int ScreenHeight;

        // Random number generator for placing the berryPosition randomly.
        private readonly Random _random = new Random();

        // Initial score and game state flags.
        public int Score { get; private set; }
        public bool IsGameOver { get; private set; }

        // Snake head and body management.
        public Point SnakeHead { get; private set; }
        public List<Point> SnakeBody { get; private set; }
        public Point BerryPosition { get; private set; }
        public string CurrentDirection { get; set; } = "RIGHT";

        public GameModel(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;
            SnakeBody = new List<Point>();
            Score = 5;
            ResetGame();
        }

        private void ResetGame()
        {
            IsGameOver = false;
            SnakeHead = new Point(ScreenWidth / 2, ScreenHeight / 2, ConsoleColor.Red);
            SnakeBody.Clear();
            // Initialize the snake body based on the initial score.
            for (int i = 1; i < Score; i++)
            {
                SnakeBody.Add(new Point(SnakeHead.X - i, SnakeHead.Y, ConsoleColor.Green));
            }
            BerryPosition = CreateBerry();
        }

        private Point CreateBerry()
        {
            // Place a new berry at a random position within the game boundaries.
            return new Point(_random.Next(1, ScreenWidth - 1), _random.Next(1, ScreenHeight - 1), ConsoleColor.Cyan);
        }

        public void MoveSnake()
        {
            // Calculate new head position based on current direction.
            var newHead = new Point(SnakeHead.X, SnakeHead.Y, ConsoleColor.Red);
            switch (CurrentDirection)
            {
                case Up: newHead.Y--; break;
                case Down: newHead.Y++; break;
                case Left: newHead.X--; break;
                case Right: newHead.X++; break;
            }

            // Move the snake head and adjust the body accordingly.
            SnakeBody.Insert(0, SnakeHead);
            SnakeHead = newHead;

            // If the new head position is on the berry, eat the berry and grow.
            if (SnakeHead.Equals(BerryPosition))
            {
                Score++;
                BerryPosition = CreateBerry();
            }
            else
            {
                // Remove the tail segment if no berry was eaten
                SnakeBody.RemoveAt(SnakeBody.Count - 1);
            }
        }

        public void CheckCollision()
        {
            if (SnakeHead.X == 0 || SnakeHead.X == ScreenWidth - 1 || SnakeHead.Y == 0 || SnakeHead.Y == ScreenHeight - 1)
            {
                IsGameOver = true;
            }
            if (SnakeBody.Skip(1).Any(p => p.Equals(SnakeHead)))
            {
                IsGameOver = true;
            }
        }
    }
}