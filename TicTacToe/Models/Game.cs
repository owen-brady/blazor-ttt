#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using PropertyChanged;
using TicTacToe.Services;
using TicTacToe.ViewModels;

namespace TicTacToe.Models
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public Player Player1 { get; }
        public Player Player2 { get; }
        public Board Board { get; }
        public bool IsPlayerOneTurn { get; set; } = true;
        public int RoundNumber { get; set; } = 1;
        public Status GameStatus { get; set; } = Status.Playing;
        public Player ActivePlayer => IsPlayerOneTurn ? Player1 : Player2;
        public Player? Winner { get; private set; }
        public bool IsTie { get; private set; }
        public bool HasWinner { get; private set; }
        
        [DependsOn(nameof(HasWinner), nameof(IsTie))]
        public bool GameFinished => HasWinner || IsTie;
        
        private readonly IPlayerInput _playerInput;
        
        public Game(Player player1, Player player2, IPlayerInput playerInput, Board? board = null)
        {
            Player1 = player1;
            Player2 = player2;
            Board = board ?? new Board(1, new List<Tile>());
            _playerInput = playerInput;
        }
        
        public enum Status
        {
            Playing,
            Winner,
            Tie,
        }

        public static Game StartNewGame(Player player1, Player player2)
        {
            var game = new Game(player1, player2, new PlayerInput(new ConsoleWrapper()));
            game.Board.GenerateTiles();
            return game;
        }

        public void PlayRound(Player player, int moveNumber, int tileId)
        {
            // User selects tile
            // var tile = _playerInput.GetValidTile(Board.Tiles);
            var tile = Board.Tiles[tileId];
            
            // Tile is played
            var move = new Move(moveNumber, player.Id, tile.Id);
            tile.Move = move;
            tile.Player = player;
            
            // Check for win
            var isWinner = Board.CheckForWin();
            if (isWinner)
            {
                GameStatus = Status.Winner;
                return; // so simple for so much pain
            }
            
            // Check for tie
            var isTie = Board.CheckForTie();
            if (isTie)
            {
                GameStatus = Status.Tie;
            }
        }
        
        public void PlayGame(int tileId)
        {
            if (HasWinner || IsTie)
            {
                return;
            }
            
            PlayRound(IsPlayerOneTurn ? Player1 : Player2, RoundNumber, tileId);
            RoundNumber++;

            switch (GameStatus)
            {
                case Status.Playing:
                    IsPlayerOneTurn = !IsPlayerOneTurn; // only change when another round will be played
                    break;
                case Status.Winner:
                    Winner = IsPlayerOneTurn ? Player1 : Player2;
                    HasWinner = true;
                    break;
                case Status.Tie:
                    IsTie = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (ActivePlayer.Type == Player.PlayerType.Robot)
            {
                PlayGame(MakeRobotMove());
            }
        }

        public static GameViewModel PlayHuman()
        {
            var player1 = new Player(1, Player.PlayerType.Human, "Player 1", "X");
            var player2 = new Player(2, Player.PlayerType.Human, "Player 2", "O");

            GameViewModel gameViewModel = new GameViewModel(player1, player2);

            return gameViewModel;
        }

        public static GameViewModel PlayRobot()
        {
            var player1 = new Player(1, Player.PlayerType.Human, "Player 1", "X");
            var player2 = new Player(2, Player.PlayerType.Robot, "Player 2", "O");

            GameViewModel gameViewModel = new GameViewModel(player1, player2);

            return gameViewModel;
        }
        
        private readonly RobotMove _robotMove = new RobotMove();
        private int MakeRobotMove()
        {
            var player = ActivePlayer;
            var opponent = player == Player1 ? Player2 : Player1;
            var tileId = _robotMove.SelectTile(Board, player, opponent);

            return tileId;
        }
    }
}