using System.ComponentModel;
using System.Linq;

#nullable enable
namespace TicTacToe.Models
{
    public class Tile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public int Id { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public Player? Player { get; set; }
        public Move? Move { get; set; }
        
        public Tile(int id, int column, int row, Player? player = null, Move? move = null)
        {
            Id = id;
            Column = column;
            Row = row;
            Player = player;
            Move = move;
        }
        
        public static bool IsValidTileInput(string input)
        {
            var valid = false;
            
            char[] validColumns = {'a', 'b', 'c'};
            char[] validRows = {'1', '2', '3'};
            
            if (input.Length == 2 && validColumns.Contains(input[0]) && validRows.Contains(input[1]))
            {
                valid = true;
            }
            
            return valid;
        }

        public bool IsPlayableTile()
        {
            return Player == null && Move == null;
        }
    }
}