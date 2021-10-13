using FluentAssertions;
using TicTacToe.Models;
using TicTacToe.ViewModels;
using Xunit;

namespace TicTacToe.Tests.ViewModels.Test
{
    public class GameViewModelTest
    {
        private GameViewModel _gameViewModel;
        private readonly Player _player1 = new Player(1, Player.PlayerType.Human, "Player 1", "X");
        private readonly Player _player2 = new Player(2, Player.PlayerType.Human, "Player 2", "O");

        [Fact]
        public void NewGame_ViewModel_ExpectAssignment()
        {
            _gameViewModel = new GameViewModel(_player1, _player2);

            _gameViewModel.Player1.Should().BeEquivalentTo(_player1);
            _gameViewModel.Player2.Should().BeEquivalentTo(_player2);
            _gameViewModel.Game.Board.Tiles.Should().NotBeEmpty();
        }

        [Fact]
        public void ModelProperties_PropertyChanged_ExpectPropertyChangedEvent()
        {
            _gameViewModel = new GameViewModel(_player1, _player2);
            
            Assert.PropertyChanged(_gameViewModel.Game, nameof(_gameViewModel.Game.IsPlayerOneTurn), () => _gameViewModel.Game.IsPlayerOneTurn = false);
            Assert.PropertyChanged(_gameViewModel.Game, nameof(_gameViewModel.Game.RoundNumber), () => _gameViewModel.Game.RoundNumber = 2);
            Assert.PropertyChanged(_gameViewModel.Game, nameof(_gameViewModel.Game.GameStatus), () => _gameViewModel.Game.GameStatus = Game.Status.Winner);
        }

        [Fact]
        public void PlayGame_RunsPlayerInput_ReturnsExpectedWinner()
        {
            // Mock user input in Game
        }
    }
}