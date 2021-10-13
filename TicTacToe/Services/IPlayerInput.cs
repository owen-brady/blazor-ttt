using System.Collections.Generic;
using TicTacToe.Models;

namespace TicTacToe.Services
{
    public interface IPlayerInput
    {
        Tile GetValidTile(List<Tile> boardTiles);
    }
}