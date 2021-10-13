using FluentAssertions;
using TicTacToe.Models;
using Xunit;

namespace TicTacToe.Tests.Models.Test
{
    public class PlayerTest
    {
        private readonly Player _player;

        public PlayerTest()
        {
            const int id = 1;
            const Player.PlayerType type = Player.PlayerType.Human;
            const string name = "Player 1";
            const string token = "X";

            _player = new Player(id, type, name, token);
        }

        [Fact]
        public void Player_WhenCreated_NotNull()
        {
            Assert.NotNull(_player);
        }

        [Fact]
        public void Player_WhenCreated_PropsMatch()
        {
            // Assert
            _player.Id.Should().Be(1);
            _player.Type.Should().Be(Player.PlayerType.Human);
            _player.Name.Should().Be("Player 1");
            _player.Token.Should().Be("X");
        }
    }
}