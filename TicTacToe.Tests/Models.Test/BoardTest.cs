using System.Collections.Generic;
using FluentAssertions;
using TicTacToe.Models;
using Xunit;

namespace TicTacToe.Tests.Models.Test
{
    public class BoardTest
    {
        // TODO: discuss with Aaron - how much setup should belong here vs individual tests
        private readonly Board _board;

        public BoardTest()
        {
            const int id = 1;
            const List<Tile> tiles = null;

            _board = new Board(id, tiles);
        }

        [Fact]
        public void DefaultTile_ValidParams_ExpectAssignment()
        {
            _board.Id.Should().Be(1);
            _board.Tiles.Should().BeEmpty();
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(2, true)]
        [InlineData(4, true)]
        [InlineData(6, true)]
        [InlineData(8, true)]
        [InlineData(10, false)]
        [InlineData(12, false)]
        public void NewGame_GenerateTiles_ShouldCreateTiles(int tileId, bool expected)
        {
            _board.GenerateTiles();
            var actual = _board.Tiles.Find(tile => tile.Id == tileId);

            var tileExists = actual != null;

            tileExists.Should().Be(expected);
        }
        
        [Theory]
        [InlineData(0, true)]
        [InlineData(2, true)]
        [InlineData(4, true)]
        [InlineData(6, true)]
        [InlineData(8, true)]
        [InlineData(10, false)]
        [InlineData(12, false)]
        public void NewGame_TileExists_ReturnsExpectedResult(int tileId, bool expected)
        {
            _board.GenerateTiles();
            var tileExists = _board.TileExists(tileId);

            tileExists.Should().Be(expected);
        }

        [Fact]
        public void NewGame_CheckForTie_ShouldReturnFalse()
        {
            _board.GenerateTiles();

            var isTie = _board.CheckForTie();

            Assert.False(isTie);
        }

        [Fact]
        public void OngoingGame_CheckForTie_ShouldReturnFalse()
        {
            var player1 = new Player(1, Player.PlayerType.Human, "Player 1", "X");
            var player2 = new Player(2, Player.PlayerType.Human, "Player 2", "O");
            var board = new Board(1, new List<Tile>());
            board.GenerateTiles();
            board.Tiles[0].Player = player1;
            board.Tiles[0].Move = new Move(0, 1, 0);

            var isTie = board.CheckForTie();
            
            isTie.Should().BeFalse();
        }

        [Fact]
        public void TieGame_CheckForTie_ShouldReturnTrue()
        {
            var player1 = new Player(1, Player.PlayerType.Human, "Player 1", "X");
            var board = new Board(1, new List<Tile>());
            board.GenerateTiles();
            board.Tiles[0].Player = player1;
            board.Tiles[1].Player = player1;
            board.Tiles[2].Player = player1;
            board.Tiles[3].Player = player1;
            board.Tiles[4].Player = player1;
            board.Tiles[5].Player = player1;
            board.Tiles[6].Player = player1;
            board.Tiles[7].Player = player1;
            board.Tiles[8].Player = player1;
            board.Tiles[0].Move = new Move(0, 1, 0);
            board.Tiles[1].Move = new Move(1, 1, 1);
            board.Tiles[2].Move = new Move(2, 1, 2);
            board.Tiles[3].Move = new Move(3, 1, 3);
            board.Tiles[4].Move = new Move(4, 1, 4);
            board.Tiles[5].Move = new Move(5, 1, 5);
            board.Tiles[6].Move = new Move(6, 1, 6);
            board.Tiles[7].Move = new Move(7, 1, 7);
            board.Tiles[8].Move = new Move(8, 1, 8);

            var isTie = board.CheckForTie();
            
            isTie.Should().BeTrue();
        }
    }
}