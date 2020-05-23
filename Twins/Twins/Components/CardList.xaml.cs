using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twins.Models;
using Twins.Models.Singletons;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace Twins.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CardList : ScrollView
    {
        public CardList()
        {
            InitializeComponent();
        }

        private async Task<FileData> OpenFileDialog()
        {
            try
            {
                string[] types = {".png", ".jpg"};
                FileData fileData = await CrossFilePicker.Current.PickFile(types);
                if (fileData != null) // user canceled file picking
                    return fileData;
            }
            catch (Exception _)
            {
            }
            return null;
        }

        private async void OnAddCard(object sender, EventArgs e)
        {
            AddButton.IsEnabled = false;
            FileData file = await OpenFileDialog();

            //if (image.Width != 100.0 && image.Height != 100.0)
            //    throw new Exception("La imagen debe de ser 100x100 pixeles");
            if (file != null)
                ((DeckEditor)this.BindingContext).AddCard(ImageSource.FromStream(file.GetStream), null);

            AddButton.IsEnabled = true;
        }

        public void RefreshList() 
        {
            ListCard.Children.Clear();
            foreach(Card card in ((DeckEditor)this.BindingContext).Deck.Cards) 
            {
                CardComponent cardComponent = new CardComponent(card);
                ListCard.Children.Add(cardComponent.SetToEdit());
            }

        }
    }
}