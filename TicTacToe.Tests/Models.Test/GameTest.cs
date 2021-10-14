using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using TicTacToe.Models;
using TicTacToe.Services;
using Xunit;

namespace TicTacToe.Tests.Models.Test
{
    public class GameTest
    {
        private readonly Game _game;
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly Board _board;
        private bool playerOneTurn = true;
        private bool hasWinner = false;

        public GameTest()
        {
            _player1 = new Player(1, Player.PlayerType.Human, "Player 1", "X");
            _player2 = new Player(2, Player.PlayerType.Human, "Player 2", "O");
            _board = new Board(1, new List<Tile>());

            _game = new Game(_player1, _player2, new PlayerInput(new ConsoleWrapper()), _board);
            _game.Board.GenerateTiles();
        }
        
        [Fact]
        public void DefaultTile_ValidParams_ExpectAssignment()
        {
            _game.Player1.Should().BeEquivalentTo(_player1);
            _game.Player2.Should().BeEquivalentTo(_player2);
            _game.Board.Should().BeEquivalentTo(_board);
        }

        [Fact]
        public void StartNewGame_ShouldReturnCorrectGameSession()
        {
            var actual = Game.StartNewGame(_player1, _player2);

            actual.Should().BeEquivalentTo(_game);
        }

        [Fact]
        public void PlayRound_NoWinner_ReturnsPlayingStatus()
        {
            var playerInputMock = Substitute.For<IPlayerInput>();
            var game = new Game(_player1, _player2, playerInputMock);
            game.Board.GenerateTiles();

            playerInputMock.GetValidTile(game.Board.Tiles).Returns(game.Board.Tiles[0]);

            var tileId = playerInputMock.GetValidTile(game.Board.Tiles).Id;
            
            const Game.Status expected = Game.Status.Playing;
            
            game.PlayRound(_player1, 1, tileId);
            
            var actual = game.GameStatus;
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PlayRound_SelectedTileHasPlayerAndMove()
        {
            var playerInputMock = Substitute.For<IPlayerInput>();
            var game = new Game(_player1, _player2, playerInputMock);
            game.Board.GenerateTiles();

            playerInputMock.GetValidTile(game.Board.Tiles).Returns(game.Board.Tiles[0]);

            var tileId = playerInputMock.GetValidTile(game.Board.Tiles).Id;

            game.PlayRound(_player1, 1, tileId);

            game.Board.Tiles[0].Player.Should().BeEquivalentTo(_player1);
            game.Board.Tiles[0].Move.Should().NotBeNull();
        }
        
        [Fact]
        public void PlayRound_UnselectedTilesDoNotHavePlayerOrMove()
        {
            var playerInputMock = Substitute.For<IPlayerInput>();
            var game = new Game(_player1, _player2, playerInputMock);
            game.Board.GenerateTiles();

            playerInputMock.GetValidTile(game.Board.Tiles).Returns(game.Board.Tiles[0]);

            var tileId = playerInputMock.GetValidTile(game.Board.Tiles).Id;

            game.PlayRound(_player1, 1, tileId);

            game.Board.Tiles.RemoveAt(0);
            
            var actual = game.Board.Tiles.Find(tile => tile.IsPlayableTile() == false);

            actual.Should().BeNull();
        }
        
        [Theory]
        [InlineData(0, 1)]
        [InlineData(5, 8)]
        [InlineData(4, 6)]
        public void PlayRound_PlayerWins_ReturnsWinnerStatus(int tile1, int tile2)
        {
            var playerInputMock = Substitute.For<IPlayerInput>();
            var game = new Game(_player1, _player2, playerInputMock);
            game.Board.GenerateTiles();
            
            // Simulate tiles necessary to return win
            game.Board.Tiles[tile1].Player = _player1;
            game.Board.Tiles[tile1].Move = new Move(1, _player1.Id, tile1);
            
            game.Board.Tiles[tile2].Player = _player1;
            game.Board.Tiles[tile2].Move = new Move(2, _player1.Id, tile2);

            playerInputMock.GetValidTile(game.Board.Tiles).Returns(game.Board.Tiles[2]);

            const Game.Status expected = Game.Status.Winner;

            var tileId = playerInputMock.GetValidTile(game.Board.Tiles).Id;
            
            game.PlayRound(_player1, 3, tileId);

            var actual = game.GameStatus;
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PlayRound_TieGame_ReturnsTieStatus()
        {
            var playerInputMock = Substitute.For<IPlayerInput>();
            var game = new Game(_player1, _player2, playerInputMock);
            game.Board.GenerateTiles();
            
            // Simulate board with played tiles that will result in tie
            game.Board.Tiles[0].Player = _player1;
            game.Board.Tiles[0].Move = new Move(1, _player1.Id, 0);
            
            game.Board.Tiles[1].Player = _player1;
            game.Board.Tiles[1].Move = new Move(2, _player1.Id, 1);
            
            game.Board.Tiles[2].Player = _player2;
            game.Board.Tiles[2].Move = new Move(3, _player2.Id, 2);
            
            game.Board.Tiles[3].Player = _player2;
            game.Board.Tiles[3].Move = new Move(4, _player2.Id, 3);
            
            game.Board.Tiles[4].Player = _player2;
            game.Board.Tiles[4].Move = new Move(5, _player2.Id, 4);
            
            game.Board.Tiles[5].Player = _player1;
            game.Board.Tiles[5].Move = new Move(6, _player1.Id, 5);
            
            game.Board.Tiles[6].Player = _player1;
            game.Board.Tiles[6].Move = new Move(7, _player1.Id, 6);
            
            game.Board.Tiles[7].Player = _player2;
            game.Board.Tiles[7].Move = new Move(8, _player2.Id, 7);
            
            playerInputMock.GetValidTile(game.Board.Tiles).Returns(game.Board.Tiles[8]);

            var tileId = playerInputMock.GetValidTile(game.Board.Tiles).Id;
            
            const Game.Status expected = Game.Status.Tie;
            
            game.PlayRound(_player1, 9, tileId);

            var actual = game.GameStatus;
            
            Assert.Equal(expected, actual);
        }
    }
}