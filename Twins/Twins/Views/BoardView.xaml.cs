using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardView : ContentPage
    {
        static double GetWidth(RelativeLayout parent, View view) =>
            view?.Measure(parent.Width, parent.Height).Request.Width ?? -1;
        static double GetHeight(RelativeLayout parent, View view) =>
            view?.Measure(parent.Width, parent.Height).Request.Height ?? -1;

        private double UsableBoardAreaSize { get { return Math.Min(boardArea.Width, boardArea.Height); } }

        public BoardView()
        {
            InitializeComponent();

            boardArea.LayoutChanged += EnforceBoardAspectRatio;

            boardArea.Children.Add(cardPreviewFrame,
                /* X */
                Constraint.RelativeToParent(parent => parent.Width / 2 - GetWidth(parent, cardPreviewFrame) / 2),
                /* Y */
                Constraint.RelativeToParent(parent => 0),
                /* Width */
                Constraint.RelativeToParent(_ => board.Children.First().Width),
                /* Height */
                Constraint.RelativeToParent(_ => board.Children.First().Height));
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
    }
}