using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace ToyRobotChallenge.Library
{
    public interface IToyRobot
    {
        void Place(uint x, uint y, Direction direction);
        void RotateLeft();
        void RotateRight();
        void Move();
        void Report();
        void Validate(uint x, uint y, Direction direction);
        void Echo(string text);
        void Error(string text);
    }

    internal class ToyRobot : IToyRobot
    {
        private readonly IBoard _board;
        private readonly ILogger<ToyRobot> _logger;

        private Direction? _direction;
        private uint _x;
        private uint _y;

        /// <summary>
        /// Used to determine what direction to face when rotating left or right
        /// </summary>
        private static readonly List<Direction> _directionRotation =
            new List<Direction>() { Direction.NORTH, Direction.EAST, Direction.SOUTH, Direction.WEST };

        public ToyRobot(IBoard board, ILogger<ToyRobot> logger)
        {
            _board = board;
            _logger = logger;
        }

        private void DoAction(Action action)
        {
            if (_direction != null)
            {
                action();
            }
            else
            {
                Error("Not placed on the board yet");
            }
        }

        public void Echo(string text) => _logger.LogInformation(text);

        public void Error(string text) => _logger.LogError(text);

        public void Move() => DoAction(() =>
        {
            var tmpX = _x;
            var tmpY = _y;

            switch (_direction)
            {
                case Direction.NORTH:
                    tmpY++;
                    break;
                case Direction.SOUTH:
                    tmpY--;
                    break;
                case Direction.EAST:
                    tmpX++;
                    break;
                case Direction.WEST:
                    tmpX--;
                    break;
            }

            if (!_board.IsValidPosition(tmpX, tmpY))
            {
                Error("Invalid board position");
                return;
            }

            // Valid position so update it
            _x = tmpX;
            _y = tmpY;
        });

        public void Place(uint x, uint y, Direction direction)
        {
            if (!_board.IsValidPosition(x, y))
            {
                Error("Invalid board position");
                return;
            }

            _x = x;
            _y = y;
            _direction = direction;
        }

        public void Report() => DoAction(() => _logger.LogInformation(string.Join(",", new object[] { _x, _y, _direction })));

        public void RotateLeft() => DoAction(() =>
        {
            var index = _directionRotation.IndexOf(_direction.Value) - 1;
            if (index < 0)
            {
                _direction = _directionRotation.Last();
            }
            else
            {
                _direction = _directionRotation[index];
            }
        });

        public void RotateRight() => DoAction(() =>
        {
            var index = _directionRotation.IndexOf(_direction.Value) + 1;
            if (index > _directionRotation.Count - 1)
            {
                _direction = _directionRotation.First();
            }
            else
            {
                _direction = _directionRotation[index];
            }
        });

        public void Validate(uint x, uint y, Direction direction)
            => DoAction(() =>
                        {
                            if (_x != x
                                || _y != y
                                || _direction != direction)
                            {
                                throw new ApplicationException("Invalid location");
                            }
                        });
    }
}
