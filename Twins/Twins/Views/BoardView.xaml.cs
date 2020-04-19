using System;
using Twins.Views.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardView : ContentPage
    {
        private double UsableBoardAreaSize { get { return Math.Min(boardArea.Width, boardArea.Height); } }

        public BoardView()
        {
            InitializeComponent();

            boardArea.LayoutChanged += EnforceBoardAspectRatio;   
        }

        private void EnforceBoardAspectRatio(object sender = null, EventArgs e = null)
        {
            if (UsableBoardAreaSize > 0) {
                var usableWidth = boardArea.Width;
                var usableHeight = boardArea.Height;

                var columns = board.ColumnDefinitions.Count;
                var rows = board.RowDefinitions.Count;

                var cellSide = Math.Min(usableHeight / rows, usableWidth / columns);

                board.WidthRequest = cellSide * columns;
                board.HeightRequest = cellSide * rows;
                InvalidateMeasure();
            }
        }

        private void OnPause(object sender, EventArgs e)
        {

        }

        private void OnMute(object sender, EventArgs e)
        {

        }
    }
}