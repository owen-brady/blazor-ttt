using FluentAssertions;
using TicTacToe.Models;
using Xunit;

namespace TicTacToe.Tests.Models.Test
{
    public class TileTest
    {
        private readonly Tile _tile;
        
        public TileTest()
        {
            const int id = 0;
            const int column = 0;
            const int row = 0;

            _tile = new Tile(id, column, row);
        }
        
        [Fact]
        public void DefaultTile_ValidParams_ExpectAssignment()
        {
            _tile.Id.Should().Be(0);
            _tile.Column.Should().Be(0);
            _tile.Row.Should().Be(0);
            _tile.Player.Should().Be(null);
            _tile.Move.Should().Be(null);
        }

        [Fact]
        public void Tile_SaveMove_ExpectPropsSet()
        {
            var player = new Player(1, Player.PlayerType.Human, "Player 1", "X");
            var move = new Move(1, 1, 0);

            _tile.Player = player;
            _tile.Move = move;

            _tile.Player.Token.Should().Be("X");
            _tile.Move.Id.Should().Be(1);
            _tile.Player.Id.Should().Be(_tile.Move.PlayerId);
        }

        [Fact]
        public void Tile_VerifyPlayableTile_ReturnTrue()
        {
            var isValid = _tile.IsPlayableTile();

            isValid.Should().Be(true);
        }

        [Fact]
        public void Tile_VerifyPlayedTile_ReturnFalse()
        {
            _tile.Player = new Player(1, Player.PlayerType.Human, "Player 1", "X");
            _tile.Move = new Move(1, 1, 0);
            
            var isValid = _tile.IsPlayableTile();

            isValid.Should().Be(false);
        }

        [Theory] // assumes user input is already cleaned and lowercase
        [InlineData("a1", true)]
        [InlineData("a4", false)]
        [InlineData("d1", false)]
        [InlineData("b2", true)]
        [InlineData("c3", true)]
        public void Tile_VerifyValidInput_ReturnCorrectBool(string input, bool expectedBool)
        {
            var isValid = Tile.IsValidTileInput(input);

            isValid.Should().Be(expectedBool);
        }
    }
}