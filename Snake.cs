using SnakeGame.Model;
using SnakeGame.View;
using SnakeGame.Controller;

namespace SnakeGame
{
    internal class Program
    {
        private static void Main()
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            // Initialize a new GameModel instance with the current console window dimensions.
            GameModel model = new GameModel(width, height);
            GameView view = new GameView();
            GameController controller = new(model, view);
            controller.Start();
        }
    }
}