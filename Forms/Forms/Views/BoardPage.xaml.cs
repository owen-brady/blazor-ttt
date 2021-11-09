using System;
using TicTacToe.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardPage : ContentPage
    {
        private GameViewModel GameViewModel { get; set; }

        public BoardPage(GameViewModel gameViewModel)
        {
            GameViewModel = gameViewModel;
            BindingContext = GameViewModel;
            
            InitializeComponent();
            
            Board.RowDefinitions = SetRowDefinitions();
            Board.ColumnDefinitions = SetColumnDefinitions();
        }

        private static RowDefinitionCollection SetRowDefinitions()
        {
            var rowDefinitionCollection = new RowDefinitionCollection();
            var rowDefinition = new RowDefinition {Height = GridSize()};
            rowDefinitionCollection.Add(rowDefinition);
            rowDefinitionCollection.Add(rowDefinition);
            rowDefinitionCollection.Add(rowDefinition);

            return rowDefinitionCollection;
        }
        
        private static ColumnDefinitionCollection SetColumnDefinitions()
        {
            var columnDefinitionCollection = new ColumnDefinitionCollection();
            var columnDefinition = new ColumnDefinition {Width = GridSize()};
            columnDefinitionCollection.Add(columnDefinition);
            columnDefinitionCollection.Add(columnDefinition);
            columnDefinitionCollection.Add(columnDefinition);

            return columnDefinitionCollection;
        }

        private static double GridSize()
        {
            return (Application.Current.MainPage.Width - 50) / 3;
        }

        private void Tile_OnClicked(object sender, EventArgs e)
        {
            var idParameter = (sender as Button)?.CommandParameter;
            var tileId = int.Parse(idParameter as string ?? "");
            GameViewModel.Game.PlayGame(tileId);
        }

        private void NewGame_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}