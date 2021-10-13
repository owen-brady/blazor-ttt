using FluentAssertions;
using TicTacToe.Models;
using Xunit;

namespace TicTacToe.Tests.Models.Test
{
    public class MoveTest
    {
        private readonly Move _move;

        public MoveTest()
        {
            const int id = 1;
            const int playerId = 1;
            const int tileId = 0;

            _move = new Move(id, playerId, tileId);
        }

        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            _move.Id.Should().Be(1);
            _move.PlayerId.Should().Be(1);
            _move.TileId.Should().Be(0);
        }
    }
}