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
        public readonly Player Player1;
        public readonly Player Player2;
        public Player ActivePlayer { get; private set; }
        public Player? Winner { get; private set; }
        public bool IsTie { get; set; }
        public bool HasWinner { get; set; }
        public bool GameFinished => HasWinner || IsTie;
        public List<Tile>? Tiles { get; private set; }

        public GameViewModel(Player player1, Player player2, Game? game = null)
        {
            // TODO: add ability to input custom player data
            Player1 = player1;
            Player2 = player2;

            // TODO: initialize new game only if game session is null
            Game = Game.StartNewGame(Player1, Player2);
            ActivePlayer = player1;
        }

        public void PlayGame(int tileId)
        {
            if (HasWinner || IsTie)
            {
                Tiles = Game.Board.Tiles;
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

            ActivePlayer = Game.ActivePlayer;
            Tiles = Game.Board.Tiles;
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