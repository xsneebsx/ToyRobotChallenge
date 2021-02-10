namespace ToyRobotChallenge.Library
{
    public interface IBoard
    {
        bool IsValidPosition(uint x, uint y);
    }

    internal class Board : IBoard
    {
        private readonly uint _size = 5;

        public bool IsValidPosition(uint x, uint y) => x < _size && y < _size;
    }
}
