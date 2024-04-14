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