using System.Collections.Generic;
using System.Reflection.Emit;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TicTacToe.Models;
using TicTacToe.ViewModels;

namespace Android
{
    [Activity(Label = "TicTacToe", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private GameViewModel _gameViewModel;
        private TextView _title;
        private TextView _turnIndicator;
        private Button _tile0;
        private Button _tile1;
        private Button _tile2;
        private Button _tile3;
        private Button _tile4;
        private Button _tile5;
        private Button _tile6;
        private Button _tile7;
        private Button _tile8;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var player1 = new Player(1, Player.PlayerType.Human, "Player 1", "X");
            var player2 = new Player(2, Player.PlayerType.Human, "Player 2", "O");
            _gameViewModel = new GameViewModel(player1, player2);

            // Reference Labels
            _title = FindViewById<TextView>(Resource.Id.title);
            _turnIndicator = FindViewById<TextView>(Resource.Id.turnIndicator);
            
            // Reference Buttons
            _tile0 = FindViewById<Button>(Resource.Id.tile0);
            _tile1 = FindViewById<Button>(Resource.Id.tile1);
            _tile2 = FindViewById<Button>(Resource.Id.tile2);
            _tile3 = FindViewById<Button>(Resource.Id.tile3);
            _tile4 = FindViewById<Button>(Resource.Id.tile4);
            _tile5 = FindViewById<Button>(Resource.Id.tile5);
            _tile6 = FindViewById<Button>(Resource.Id.tile6);
            _tile7 = FindViewById<Button>(Resource.Id.tile7);
            _tile8 = FindViewById<Button>(Resource.Id.tile8);

            var tiles = new List<Button> {_tile0, _tile1, _tile2, _tile3, _tile4, _tile5, _tile6, _tile7, _tile8};

            // Add function to buttons
            for (var i = 0; i < tiles.Count; i++)
            {
                var i1 = i;
                tiles[i1].Click += (sender, args) =>
                {
                    _gameViewModel.PlayGame(i1);
                    RefreshView(tiles);
                };
            }
            
            RefreshView(tiles);
        }

        private void RefreshView(IReadOnlyList<Button> tiles)
        {
            // Set Labels
            if (!_gameViewModel.GameFinished)
            {
                if (_title != null) _title.Text = "Let's play a game!";
            } else if (_gameViewModel.HasWinner)
            {
                if (_title != null) _title.Text = $"Winner: {_gameViewModel.Winner?.Name}";
            }
            else if (_gameViewModel.IsTie)
            {
                if (_title != null) _title.Text = "Tie Game!";
            }

            _turnIndicator.Text = $"{_gameViewModel.Game.ActivePlayer.Name} - please select tile";
            _turnIndicator.Visibility = _gameViewModel.GameFinished ? ViewStates.Invisible : ViewStates.Visible;

            // Set Board Tokens
            for (var i = 0; i < tiles.Count; i++)
            {
                tiles[i].Text = _gameViewModel.Game.Board.Tiles[i].Player?.Token;
            }
        }
    }
}