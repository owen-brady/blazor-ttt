#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TicTacToe.Models;
using TicTacToe.Services;

namespace TicTacToe.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public readonly Game Game;
        public Player Player1 => Game.Player1;
        public Player Player2 => Game.Player2;
        public Player ActivePlayer => Game.ActivePlayer;
        public Player? Winner => Game.Winner;
        public bool IsTie => Game.IsTie;
        public bool HasWinner => Game.HasWinner;
        public bool GameFinished => Game.GameFinished;
        public List<Tile>? Tiles => Game.Board.Tiles;

        public GameViewModel(Player player1, Player player2, Game? game = null)
        {
            // TODO: initialize new game only if game session is null
            Game = Game.StartNewGame(player1, player2);

            Game.PropertyChanged += (sender, args) => PropertyChanged?.Invoke(this, args);
        }
    }
}