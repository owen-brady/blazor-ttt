using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TicTacToe.Models;
using TicTacToe.ViewModels;

namespace TicTacToe.Services
{
    public class RobotMove
    {
        public int SelectTile(Board gameBoard, Player player, Player opponent)
        {
            if (gameBoard.Tiles.Count(tile => tile.Player != null) == 0)
            {
                return MakeOpeningMove();
            }
            
            return FindBestMove(gameBoard, player, opponent);
        }

        private int MakeOpeningMove()
        {
            return RandomCorner();
        }

        private int RandomCorner()
        {
            var randomGenerator = new Random();
            var tile = randomGenerator.Next(1, 4);

            var tileId = tile switch
            {
                1 => 0,
                2 => 2,
                3 => 6,
                4 => 8,
                _ => 0
            };

            return tileId;
        }
        
        // Method to score board state (+10 for win, -10 for loss)
        public static int EvaluateBoard(Board board, Player player, Player opponent)
        {
            var playerTiles = board.Tiles.Where(tile => tile.Player == player).Select(tile => tile.Id).ToList();
            var opponentTiles = board.Tiles.Where(tile => tile.Player == opponent).Select(tile => tile.Id).ToList();

            var win = CheckForWin(playerTiles);

            if (win)
            {
                return 10;
            }
            
            var lose = CheckForWin(opponentTiles);
            if (lose)
            {
                return -10;
            }

            return 0;
        }

        private static bool CheckForWin(ICollection<int> tiles)
        {
            int[,] winConditions = {{0, 1, 2}, {3, 4, 5}, {6, 7, 8}, {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, {0, 4, 8}, {2, 4, 6}};
            
            for (var i = 0; i < winConditions.GetUpperBound(0); i++)
            {
                var winningMove = tiles.Contains(winConditions[i, 0]) && tiles.Contains(winConditions[i, 1]) &&
                                  tiles.Contains(winConditions[i, 2]);

                if (winningMove)
                {
                    return true;
                }
            }

            return false;
        }
        
        // Recursive minimax function that scores each move
        public int Minimax(Board board, Player player, Player opponent, int depth, bool isMax)
        {
            var score = EvaluateBoard(board, player, opponent);

            // Check for win/loss to return
            if (score == 10 || score == -10)
            {
                return score;
            }
            
            // Check for draw - no moves left and no win/loss
            if (board.Tiles.Count(tile => tile.Player == null) == 0)
            {
                return 0;
            }
            
            // Generate list of available tiles
            var availableTiles = board.Tiles.Where(tile => tile.Player == null).Select(tile => tile.Id).ToList();
            
            // TODO: refactor isMax
            if (isMax)
            {
                var best = -1000;

                for (var i = 0; i < availableTiles.Count(); i++)
                {
                    // Simulate making move
                    board.Tiles[availableTiles[i]].Player = player;
                    
                    // Recursively call Minimax
                    best = Math.Max(best, Minimax(board, player, opponent, depth + 1, !isMax));

                    board.Tiles[availableTiles[i]].Player = null;
                }

                return best;
            }
            else
            {
                var best = 1000;

                for (var i = 0; i < availableTiles.Count(); i++)
                {
                    // Simulate opponent making move
                    board.Tiles[availableTiles[i]].Player = opponent;
                    
                    // Recursively call Minimax
                    best = Math.Min(best, Minimax(board, player, opponent, depth + 1, !isMax));

                    board.Tiles[availableTiles[i]].Player = null;
                }

                return best;
            }
        }
        
        // Method for finding best move that loops through each possible move and calls minimax
        private int FindBestMove(Board board, Player player, Player opponent)
        {
            var availableTiles = board.Tiles.Where(tile => tile.Player == null).Select(tile => tile.Id).ToList();

            var bestMove = -1000;
            var bestMoveScore = -1000;

            for (var i = 0; i < availableTiles.Count(); i++)
            {
                var tile = board.Tiles[availableTiles[i]];
                tile.Player = player;

                var score = Minimax(board, player, opponent, 0, false);

                tile.Player = null;

                if (score > bestMoveScore)
                {
                    bestMove = availableTiles[i];
                    bestMoveScore = score;
                }
            }

            return bestMove;
        }
    }
}