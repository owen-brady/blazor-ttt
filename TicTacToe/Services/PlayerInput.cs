using System.Collections.Generic;
using TicTacToe.Models;

namespace TicTacToe.Services
{
    public class PlayerInput : IPlayerInput
    {
        private readonly IConsoleWrapper _consoleWrapper;

        public PlayerInput(IConsoleWrapper consoleWrapper)
        {
            _consoleWrapper = consoleWrapper;
        }
        
        private static Tile ConvertInputToTile(string tileAsString, List<Tile> boardTiles)
        {
            var column =  tileAsString[0] - 97;
            var row = tileAsString[1] - 49;

            var tileToReturn = boardTiles.Find(tile => tile.Column == column && tile.Row == row);
            return tileToReturn;
        }
        
        private static string CleanInput(string input)
        {
            return input.ToLower().Trim().Replace(" ", "");
        }

        public Tile GetValidTile(List<Tile> boardTiles)
        {
            var isValid = false;
            Tile tile = null;
            var prompt = "Please select tile: ";

            while (!isValid)
            {
                // Get Selection from User
                _consoleWrapper.Write(prompt);
                var userInput = _consoleWrapper.GetInput();
                var cleanInput = CleanInput(userInput);
                
                // Check tile is valid (exists on board)
                var valid = Tile.IsValidTileInput(cleanInput);
                
                if (!valid)
                {
                    prompt = "Invalid tile. Please select tile: ";
                    continue;
                }
                
                tile = ConvertInputToTile(cleanInput, boardTiles);
                
                // Check tile has not been played yet
                if (!tile.IsPlayableTile())
                {
                    prompt = "Tile has already been played. Please select tile: ";
                    continue;
                }
                
                isValid = true;
            }

            return tile;
        }
    }
}