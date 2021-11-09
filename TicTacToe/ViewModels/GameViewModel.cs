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
        public Player? Winner { get; private set; }
        public bool IsTie { get; set; }
        public bool HasWinner { get; set; }
        public bool GameFinished => HasWinner || IsTie;
        public List<Tile>? Tiles => Game.Board.Tiles;

        public GameViewModel(Player player1, Player player2, Game? game = null)
        {
            // TODO: initialize new game only if game session is null
            Game = Game.StartNewGame(player1, player2);
        }

        public void PlayGame(int tileId)
        {
            if (HasWinner || IsTie)
            {
                return;
            }
            
            Game.PlayRound(Game.IsPlayerOneTurn ? Player1 : Player2, Game.RoundNumber, tileId);
            Game.RoundNumber++;

            switch (Game.GameStatus)
            {
                case Game.Status.Playing:
                    Game.IsPlayerOneTurn = !Game.IsPlayerOneTurn; // only change when another round will be played
                    break;
                case Game.Status.Winner:
                    Winner = Game.IsPlayerOneTurn ? Player1 : Player2;
                    HasWinner = true;
                    break;
                case Game.Status.Tie:
                    IsTie = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Game.ActivePlayer.Type == Player.PlayerType.Robot)
            {
                PlayGame(MakeRobotMove());
            }
        }

        private readonly RobotMove _robotMove = new RobotMove();
        private int MakeRobotMove()
        {
            var player = Game.ActivePlayer;
            var opponent = player == Player1 ? Player2 : Player1;
            var tileId = _robotMove.SelectTile(Game.Board, player, opponent);

            return tileId;
        }
    }
}