using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize a new game instance with the current console window dimensions.
            var game = new SnakeGame(Console.WindowWidth, Console.WindowHeight);
            game.Start();
        }
    }

    public class SnakeGame
    {
        // Direction constants for readability and easy management of snake's direction.
        private const string Right = "RIGHT";
        private const string Left = "LEFT";
        private const string Up = "UP";
        private const string Down = "DOWN";

        private readonly int _screenWidth;
        private readonly int _screenHeight;
        // Random number generator for placing the berryPosition randomly.
        private readonly Random _random = new Random();
        // Initial score and game state flags.
        private int _score = 5;
        private bool _isGameOver;
        private Point _snakeHead;
        // Snake head and body management.
        private readonly List<Point> _snakeBody = new();
        private Point _berryPosition;
        private string _currentDirection = Right;

        public SnakeGame(int width, int height)
        {
            _screenWidth = width;
            _screenHeight = height;
            ResetGame();
        }

        public void Start()
        {
            while (!_isGameOver)
            {
                ClearScreen();
                DrawBorders();
                ProcessInput();
                UpdateGame();
                RenderGame();
                // Game loop delay for playable speed.
                Thread.Sleep(200);
            }
            DisplayGameOver();
        }

        private void ResetGame()
        {
            _isGameOver = false;
            _snakeHead = new Point(_screenWidth / 2, _screenHeight / 2, ConsoleColor.Red);
            _snakeBody.Clear();
            // Initialize the snake body based on the initial score.
            for (int i = 1; i < _score; i++)
            {
                _snakeBody.Add(new Point(_snakeHead.X - i, _snakeHead.Y, ConsoleColor.Green));
            }
            _berryPosition = CreateBerry();
        }

        private Point CreateBerry()
        {
            // Place a new berry at a random position within the game boundaries.
            return new Point(_random.Next(1, _screenWidth - 1), _random.Next(1, _screenHeight - 1), ConsoleColor.Red);
        }

        private void ClearScreen()
        {
            Console.Clear();
        }

        private void DrawBorders()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int x = 0; x < _screenWidth; x++)
            {
                Console.SetCursorPosition(x, 0);
                Console.Write("■");
                Console.SetCursorPosition(x, _screenHeight - 1);
                Console.Write("■");
            }
            for (int y = 0; y < _screenHeight; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write("■");
                Console.SetCursorPosition(_screenWidth - 1, y);
                Console.Write("■");
            }
        }

        private void ProcessInput()
        {
            // Check if a key is pressed and if so, update the direction based on arrow keys.
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow when _currentDirection != Down:
                        _currentDirection = Up;
                        break;
                    case ConsoleKey.DownArrow when _currentDirection != Up:
                        _currentDirection = Down;
                        break;
                    case ConsoleKey.LeftArrow when _currentDirection != Right:
                        _currentDirection = Left;
                        break;
                    case ConsoleKey.RightArrow when _currentDirection != Left:
                        _currentDirection = Right;
                        break;
                }
            }
        }

        private void UpdateGame()
        {
            MoveSnake();
            CheckCollision();
        }

        private void MoveSnake()
        {
            // Calculate new head position based on current direction.
            var newHead = new Point(_snakeHead.X, _snakeHead.Y, ConsoleColor.Red);
            switch (_currentDirection)
            {
                case Up: newHead.Y--; break;
                case Down: newHead.Y++; break;
                case Left: newHead.X--; break;
                case Right: newHead.X++; break;
            }

            // Move the snake head and adjust the body accordingly.
            _snakeBody.Insert(0, _snakeHead);
            _snakeHead = newHead;

            // If the new head position is on the berry, eat the berry and grow.
            if (_snakeHead.Equals(_berryPosition))
            {
                _score++;
                _berryPosition = CreateBerry();
            }
            else
            {
                // Remove the tail segment if no berry was eaten
                _snakeBody.RemoveAt(_snakeBody.Count - 1);
            }
        }

        private void CheckCollision()
        {
            if (_snakeHead.X == 0 || _snakeHead.X == _screenWidth - 1 || _snakeHead.Y == 0 || _snakeHead.Y == _screenHeight - 1)
            {
                _isGameOver = true;
            }
            if (_snakeBody.Skip(1).Any(p => p.Equals(_snakeHead)))
            {
                _isGameOver = true;
            }
        }

        private void RenderGame()
        {
            // Draw the snake body and head on the console.
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var part in _snakeBody)
            {
                Console.SetCursorPosition(part.X, part.Y);
                Console.Write("■");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(_snakeHead.X, _snakeHead.Y);
            Console.Write("■");
            // Draw the berry.
            Console.SetCursorPosition(_berryPosition.X, _berryPosition.Y);
            Console.ForegroundColor = _berryPosition.Color;
            Console.Write("ó");
        }

        private void DisplayGameOver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Game over, Score: {_score}");
        }
    }

    public class Point(int x, int y, ConsoleColor color)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public ConsoleColor Color { get; } = color;

        public bool Equals(Point other)
        {
            // Check if two points are the same based on their coordinates.
            return X == other.X && Y == other.Y;
        }
    }
}