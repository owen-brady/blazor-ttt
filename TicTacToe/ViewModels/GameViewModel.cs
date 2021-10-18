#nullable enable
using System;
using TicTacToe.Models;

namespace TicTacToe.ViewModels
{
    public class GameViewModel
    {
        public readonly Game Game;
        public readonly Player Player1;
        public readonly Player Player2;
        public Player? Winner { get; set; }
        public bool IsTie { get; set; }
        public bool HasWinner { get; set; }
        public bool GameFinished => HasWinner || IsTie;

        public GameViewModel(Player player1, Player player2, Game? game = null)
        {
            // TODO: add ability to input custom player data
            Player1 = player1;
            Player2 = player2;
            // TODO: initialize new game only if game session is null
            Game = Game.StartNewGame(Player1, Player2);
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
        }
    }
}