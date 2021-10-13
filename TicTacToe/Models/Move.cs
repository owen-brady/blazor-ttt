namespace TicTacToe.Models
{
    public class Move
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int TileId { get; set; }

        public Move(int id, int playerId, int tileId)
        {
            Id = id;
            PlayerId = playerId;
            TileId = tileId;
        }
    }
}