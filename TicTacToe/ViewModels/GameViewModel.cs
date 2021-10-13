#nullable enable
using System;
using TicTacToe.Models;

namespace TicTacToe.ViewModels
{
    public class GameViewModel
    {
        public Game Game;
        public readonly Player Player1;
        public readonly Player Player2;
        

        public GameViewModel(Player player1, Player player2, Game? game = null)
        {
            // TODO: add ability to input custom player data
            Player1 = player1;
            Player2 = player2;
            // TODO: initialize new game only if game session is null
            Game = Game.StartNewGame(Player1, Player2);
        }

        public void PlayGame()
        {
            while (Game.GameStatus == Game.Status.Playing)
            {
                // TODO: re-implement
                // Game.Board.DrawBoard();
                Game.GameStatus = Game.PlayRound(Game.IsPlayerOneTurn ? Player1 : Player2, Game.RoundNumber);
                Game.RoundNumber++;
                if (Game.GameStatus == Game.Status.Playing) Game.IsPlayerOneTurn = !Game.IsPlayerOneTurn; // only change when another round will be played
            }

            if (Game.GameStatus == Game.Status.Winner)
            {
                var winner = Game.IsPlayerOneTurn ? Player1 : Player2;
                // TODO: convert to future solution
                Console.WriteLine($"Winner: {winner.Name}");
            }
            else
            {
                // TODO: convert to future solution
                Console.WriteLine("Tie Game");
            }
        }
    }
}