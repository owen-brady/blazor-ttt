#nullable enable
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Models
{
    public class Board
    {
        public int Id { get; set; }
        public List<Tile> Tiles { get; set; }

        public Board(int id, List<Tile>? tiles)
        {
            Id = id;
            Tiles = tiles ?? new List<Tile>();
        }

        public void GenerateTiles()
        {
            Tiles.Add(new Tile(0, 0, 0));
            Tiles.Add(new Tile(1, 1, 0));
            Tiles.Add(new Tile(2, 2, 0));
            Tiles.Add(new Tile(3, 0, 1));
            Tiles.Add(new Tile(4, 1, 1));
            Tiles.Add(new Tile(5, 2, 2));
            Tiles.Add(new Tile(6, 0, 2));
            Tiles.Add(new Tile(7, 1, 2));
            Tiles.Add(new Tile(8, 2, 2));
        }

        public bool TileExists(int tileId)
        {
            var exists = Tiles.Find(tile => tile.Id == tileId);

            return exists != null;
        }

        // Check if all tiles have been played
        // Does NOT check win conditions
        public bool CheckForTie()
        {
            var tileCount = Tiles.Count;

            var playedTiles = 0;
            
            for (var i = 0; i < tileCount; i++)
            {
                if (!Tiles[i].IsPlayableTile())
                {
                    playedTiles++;
                }
            }

            return tileCount == playedTiles;
        }

        public bool CheckForWin()
        {
            // Convert Tiles to list of tokens to compare for a win
            var tiles = new List<string>();
            Tiles.ForEach(tile => tiles.Add(tile.Player?.Token ?? ""));
            
            // Win Conditions
            var winConditions = new List<List<string>>
            {
                new() {tiles[0], tiles[1], tiles[2]},
                new() {tiles[3], tiles[4], tiles[5]},
                new() {tiles[6], tiles[7], tiles[8]},
                new() {tiles[0], tiles[3], tiles[6]},
                new() {tiles[1], tiles[4], tiles[7]},
                new() {tiles[2], tiles[5], tiles[8]},
                new() {tiles[0], tiles[4], tiles[8]},
                new() {tiles[2], tiles[4], tiles[6]}
            };

            // Check Win Conditions for Player Tokens
            foreach (var condition in winConditions)
            {
                // Compare against first token in list
                var token = condition[0];
                
                // Check if they are all the same input AND not empty
                var winningCombo = condition.All(input => input == token && input != "");

                if (winningCombo)
                {
                    return true;
                }
            }

            return false;
        }

        public void DrawBoard()
        {
            throw new System.NotImplementedException();
        }
    }
}