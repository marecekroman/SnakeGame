//This project is done by Roman Marecek for Tomas Bata University in Zlin, Czech Republic
//This code is refactored and modified fom the original code from https://codereview.stackexchange.com/questions/127515/first-c-program-snake-game

namespace SnakeGame.Model
{
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