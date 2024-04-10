namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(32, 16);  // Ensure the window size is set correctly at the start
            Console.SetBufferSize(32, 16);
            var game = new SnakeGame();
            game.Start();
        }
    }

    public class SnakeGame
    {
            int screenWidth;
            int screenHeight;
            Random random;
            int score;
            bool gameOver;
            Point berry = new Point();
            string currentDirection;
            List<int> xposlijf;
            List<int> yposlijf;
            int berryX;
            int berryY;
            DateTime tijd;
            DateTime tijd2;
            string buttonPressed;

            public SnakeGame()
            {
                screenWidth = Console.WindowWidth;
                screenHeight = Console.WindowHeight;
                random = new Random();
                score = 5;
                gameOver = false;
                berry = new Point { xpos = screenWidth / 2, ypos = screenHeight / 2, color = ConsoleColor.Red };
                currentDirection = "RIGHT";
                xposlijf = new List<int>();
                yposlijf = new List<int>();
                berryX = random.Next(0, screenWidth);
                berryY = random.Next(0, screenHeight);
                tijd = DateTime.Now;
                tijd2 = DateTime.Now;
                buttonPressed = "no";
            }

            public void Start()
            {
                while (!gameOver)
                {
                    Console.Clear();
                    if (berry.xpos == screenWidth - 1 || berry.xpos == 0 || berry.ypos == screenHeight - 1 || berry.ypos == 0)
                    {
                        gameOver = true;
                    }

                    DrawBorders();

                    Console.ForegroundColor = ConsoleColor.Green;
                    if (berryX == berry.xpos && berryY == berry.ypos)
                    {
                        score++;
                        berryX = random.Next(1, screenWidth - 2);
                        berryY = random.Next(1, screenHeight - 2);
                    }

                    UpdateSnakePosition();

                    if (gameOver)
                    {
                        break;
                    }

                    RenderBerry();

                    HandleInput();

                    UpdateSnake();

                }
                EndGame();
            }

            private void DrawBorders()
            {
                    for (int i = 0; i < screenWidth; i++)
                    {
                        Console.SetCursorPosition(i, 0);
                        Console.Write("■");
                    }
                    for (int i = 0; i < screenWidth; i++)
                    {
                        Console.SetCursorPosition(i, screenHeight - 1);
                        Console.Write("■");
                    }
                    for (int i = 0; i < screenHeight; i++)
                    {
                        Console.SetCursorPosition(0, i);
                        Console.Write("■");
                    }
                    for (int i = 0; i < screenHeight; i++)
                    {
                        Console.SetCursorPosition(screenWidth - 1, i);
                        Console.Write("■");
                    }

            }

            private void RenderBerry()
            {
                Console.SetCursorPosition(berry.xpos, berry.ypos);
                Console.ForegroundColor = berry.color;
                Console.Write("■");
                Console.SetCursorPosition(berryX, berryY);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                tijd = DateTime.Now;
                buttonPressed = "no";
            }

            private void HandleInput()
            {
                while (true)
                {
                    tijd2 = DateTime.Now;
                    if (tijd2.Subtract(tijd).TotalMilliseconds > 500) { break; }
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo toets = Console.ReadKey(true);
                        if (toets.Key.Equals(ConsoleKey.UpArrow) && currentDirection != "DOWN" && buttonPressed == "no")
                        {
                            currentDirection = "UP";
                            buttonPressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.DownArrow) && currentDirection != "UP" && buttonPressed == "no")
                        {
                            currentDirection = "DOWN";
                            buttonPressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.LeftArrow) && currentDirection != "RIGHT" && buttonPressed == "no")
                        {
                            currentDirection = "LEFT";
                            buttonPressed = "yes";
                        }
                        if (toets.Key.Equals(ConsoleKey.RightArrow) && currentDirection != "LEFT" && buttonPressed == "no")
                        {
                            currentDirection = "RIGHT";
                            buttonPressed = "yes";
                        }
                    }
                }
            }

            private void UpdateSnake()
            {
                xposlijf.Add(berry.xpos);
                yposlijf.Add(berry.ypos);
                switch (currentDirection)
                {
                    case "UP":
                        berry.ypos--;
                        break;
                    case "DOWN":
                        berry.ypos++;
                        break;
                    case "LEFT":
                        berry.xpos--;
                        break;
                    case "RIGHT":
                        berry.xpos++;
                        break;
                }
                if (xposlijf.Count() > score)
                {
                    xposlijf.RemoveAt(0);
                    yposlijf.RemoveAt(0);
                }
            }

            private void UpdateSnakePosition()
            {
                for (int i = 0; i < xposlijf.Count(); i++)
                {
                    Console.SetCursorPosition(xposlijf[i], yposlijf[i]);
                    Console.Write("■");
                    if (xposlijf[i] == berry.xpos && yposlijf[i] == berry.ypos)
                    {
                        gameOver = true;
                    }
                }
            }

            private void EndGame()
            {
                Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
                Console.WriteLine("Game over, Score: " + score);
                Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
                Console.WriteLine("Press any key to exit...");
            }

        private class Point
        {
            public int xpos { get; set; }
            public int ypos { get; set; }
            public ConsoleColor color { get; set; }
        }
    }
}