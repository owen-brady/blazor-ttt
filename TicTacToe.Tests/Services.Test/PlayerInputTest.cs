using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using TicTacToe.Models;
using TicTacToe.Services;
using Xunit;

namespace TicTacToe.Tests.Services.Test
{
    public class PlayerInputTest
    {
        // GetValidTile 
        // - requests user input
        // - validates user input
        // - returns valid tile based on user input
        private readonly PlayerInput _playerInput;
        private readonly Game _game;

        public PlayerInputTest()
        {
            var userInputMock = Substitute.For<IConsoleWrapper>();
            _playerInput = new PlayerInput(userInputMock);
            
            var player1 = new Player(1, Player.PlayerType.Human, "Player 1", "X");
            var player2 = new Player(2, Player.PlayerType.Human, "Player 2", "O");
            _game = Game.StartNewGame(player1, player2);
        }

        [Theory]
        [InlineData("z1", "b1")]
        [InlineData("Z1", "B1")]
        [InlineData("z1", "B1")]
        public void GetValidTile_WhenUserSelectsTileNotOnBoard_TileNotReturned(string input1, string input2)
        {
            var userInputMock = Substitute.For<IConsoleWrapper>();
            var playerInput = new PlayerInput(userInputMock);
            userInputMock.GetInput().Returns(input1, input2);
        
            var tiles = new List<Tile>
            {
                new (1, 1, 0)
            };
        
            var result = playerInput.GetValidTile(tiles);
            
            result.Should().BeEquivalentTo(new Tile(1, 1, 0));
        }
        
        [Theory]
        [InlineData("a1", "b1")]
        [InlineData("A1", "B1")]
        [InlineData("a1", "B1")]
        public void GetValidTile_WhenUserDuplicatesMove_TileNotReturned(string input1, string input2)
        {
            var userInputMock = Substitute.For<IConsoleWrapper>();
            var playerInput = new PlayerInput(userInputMock);
            userInputMock.GetInput().Returns(input1, input2);

            var tiles = new List<Tile>
            {
                new (0, 0, 0, new Player(1, Player.PlayerType.Human, "Player 1", "X")),
                new (1, 1, 0)
            };

            var result = playerInput.GetValidTile(tiles);
            
            result.Should().BeEquivalentTo(new Tile(1, 1, 0));
        }

        [Theory]
        [InlineData("a1")]
        [InlineData("A1")]
        public void GetValidTile_WhenUserMakesValidMove_TileIsReturned(string input)
        {
            var userInputMock = Substitute.For<IConsoleWrapper>();
            var playerInput = new PlayerInput(userInputMock);
            userInputMock.GetInput().Returns(input);

            var tiles = new List<Tile>
            {
                new (0, 0, 0),
                new (1, 1, 0),
            };

            var result = playerInput.GetValidTile(tiles);

            result.Should().BeEquivalentTo(new Tile(0, 0, 0));
        }
    }
}