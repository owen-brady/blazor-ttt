namespace TicTacToe.Models
{
    public class Player
    {
        public int Id { get; }
        public PlayerType Type { get; }
        public string Name { get; }
        public string Token { get; }
     
        public enum PlayerType
        {
            Human,
            Robot
        }
        
        public Player(int id, PlayerType type, string name, string token)
        {
            Id = id;
            Type = type;
            Name = name;
            Token = token;
        }
    }
}