using SnakeGame.Model;
using SnakeGame.View;

namespace SnakeGame.Controller
{
    public class GameController(GameModel model, GameView view)
    {
        public void Start()
        {
            while (!model.IsGameOver)
            {
                view.ClearScreen();
                view.DrawBorders(model);
                ProcessInput();
                UpdateGame();
                view.RenderGame(model);

                // Game loop delay for playable speed.
                Thread.Sleep(200);
            }

            view.DisplayGameOver(model);
        }

        private void ProcessInput()
        {
            // Check if a key is pressed and if so, update the direction based on arrow keys.
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow when model.CurrentDirection != GameModel.Down:
                        model.CurrentDirection = GameModel.Up;
                        break;
                    case ConsoleKey.DownArrow when model.CurrentDirection != GameModel.Up:
                        model.CurrentDirection = GameModel.Down;
                        break;
                    case ConsoleKey.LeftArrow when model.CurrentDirection != GameModel.Right:
                        model.CurrentDirection = GameModel.Left;
                        break;
                    case ConsoleKey.RightArrow when model.CurrentDirection != GameModel.Left:
                        model.CurrentDirection = GameModel.Right;
                        break;
                }
            }

        }

        private void UpdateGame()
        {
            model.MoveSnake();
            model.CheckCollision();
        }
    }
}