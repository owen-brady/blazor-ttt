using System.Collections.Generic;
using FluentAssertions;
using TicTacToe.Models;
using TicTacToe.Services;
using TicTacToe.ViewModels;
using Xunit;

namespace TicTacToe.Tests.Services.Test
{
    public class RobotMoveTest
    {
        private readonly RobotMove _robotMove = new RobotMove();
        private static readonly Player Player1 = new (0, Player.PlayerType.Human, "Human", "X");
        private static readonly Player Player2 = new (1, Player.PlayerType.Robot, "Robot", "O");
        private readonly GameViewModel _gameViewModel = new (Player1, Player2);
        
        [Fact]
        public void OpeningMove_ExpectCornerTileSelected()
        {
            var selectedTile = _robotMove.SelectTile(_gameViewModel.Game.Board, Player1, Player2);

            var expected = new[] { 0, 2, 6, 8 };

            expected.Should().Contain(selectedTile);
        }

        [Fact]
        public void EvaluateBoard_ReturnsExpectedScore_NoWinCondition()
        {
            const int expectedScore = 0;
            
            _gameViewModel.Game.Board.Tiles[0].Player = Player1;
            _gameViewModel.Game.Board.Tiles[6].Player = Player2;
            _gameViewModel.Game.Board.Tiles[1].Player = Player1;
            _gameViewModel.Game.Board.Tiles[7].Player = Player2;
            
            var player = Player1;
            var opponent = Player2;

            var actualScore = RobotMove.EvaluateBoard(_gameViewModel.Game.Board, player, opponent);
            
            Assert.Equal(expectedScore, actualScore);
        }

        [Fact]
        public void Minimax_ReturnsExpectedScore_WinningMove()
        {
            // should return 10 points for Player1
            const int expectedScore = 10;
            
            _gameViewModel.Game.Board.Tiles[0].Player = Player1;
            _gameViewModel.Game.Board.Tiles[6].Player = Player2;
            _gameViewModel.Game.Board.Tiles[1].Player = Player1;
            _gameViewModel.Game.Board.Tiles[7].Player = Player2;
            _gameViewModel.Game.Board.Tiles[2].Player = Player1;
            
            var player = Player1;
            var opponent = Player2;

            var actualScore = _robotMove.Minimax(_gameViewModel.Game.Board, player, opponent, 0, true);
            
            Assert.Equal(expectedScore, actualScore);
        }

        [Fact]
        public void FindBestMove_ReturnsWinningMove()
        {
            _gameViewModel.Game.Board.Tiles[0].Player = Player1;
            _gameViewModel.Game.Board.Tiles[6].Player = Player2;
            _gameViewModel.Game.Board.Tiles[1].Player = Player1;
            _gameViewModel.Game.Board.Tiles[7].Player = Player2;

            var player = Player1;
            var opponent = Player2;

            const int expectedMove = 2;
            var actualMove = _robotMove.SelectTile(_gameViewModel.Game.Board, player, opponent);
            
            Assert.Equal(expectedMove, actualMove);
        }

        [Fact]
        public void FindBestMove_ReturnsSavingMove()
        {
            _gameViewModel.Game.Board.Tiles[0].Player = Player1;
            _gameViewModel.Game.Board.Tiles[6].Player = Player2;
            _gameViewModel.Game.Board.Tiles[1].Player = Player1;

            var player = Player2;
            var opponent = Player1;

            const int expectedMove = 2;
            var actualMove = _robotMove.SelectTile(_gameViewModel.Game.Board, player, opponent);
            
            Assert.Equal(expectedMove, actualMove);
        }

        [Fact]
        public void FindBestMove_AvoidsTrap()
        {
            _gameViewModel.Game.Board.Tiles[0].Player = Player1;
            _gameViewModel.Game.Board.Tiles[4].Player = Player2;
            _gameViewModel.Game.Board.Tiles[6].Player = Player1;
            
            var player = Player2;
            var opponent = Player1;

            var expectedMove = new[] {1, 3, 5, 7};
            
            var actualMove = _robotMove.SelectTile(_gameViewModel.Game.Board, player, opponent);

            expectedMove.Should().Contain(actualMove);
        }
    }
}