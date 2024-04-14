using SnakeGame.Model;

namespace SnakeGame.View
{
    public class GameView
    {
        public void RenderGame(GameModel model)
        {
            ClearScreen();
            DrawBorders(model);
            DrawSnake(model.SnakeBody, model.SnakeHead);
            DrawBerry(model.BerryPosition);

            if (model.IsGameOver)
            {
                DisplayGameOver(model);
            }
        }
        public void ClearScreen()
        {
            Console.Clear();
        }

        public void DrawBorders(GameModel model)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int x = 0; x < model.ScreenWidth; x++)
            {
                Console.SetCursorPosition(x, 0);
                Console.Write("■");
                Console.SetCursorPosition(x, model.ScreenHeight - 1);
                Console.Write("■");
            }
            for (int y = 0; y < model.ScreenHeight; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write("■");
                Console.SetCursorPosition(model.ScreenWidth - 1, y);
                Console.Write("■");
            }
        }

         private void DrawSnake(List<Point> snakeBody, Point snakeHead)
         {
             Console.ForegroundColor = ConsoleColor.Green;
             foreach (var part in snakeBody)
             {
                 Console.SetCursorPosition(part.X, part.Y);
                 Console.Write("■");
             }

             Console.SetCursorPosition(snakeHead.X, snakeHead.Y);
             Console.ForegroundColor = snakeHead.Color;
             Console.Write("■");
         }

        private void DrawBerry(Point berryPosition)
        {
            Console.SetCursorPosition(berryPosition.X, berryPosition.Y);
            Console.ForegroundColor = berryPosition.Color;
            Console.Write("ó");
        }
        public void DisplayGameOver(GameModel model)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Game over, Score: {model.Score}");
        }
    }
}