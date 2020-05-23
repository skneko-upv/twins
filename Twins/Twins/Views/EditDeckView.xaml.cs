using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Xml.Linq;
using Twins.Models.Singletons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditDeckView : ContentPage
    {
        private DeckEditor deckEditor;

        public EditDeckView()
        {
            InitializeComponent();
            deckEditor = new DeckEditor();
            CardList.BindingContext = deckEditor;
            deckEditor.CardsModified += RefreshCards;
        }

        private void RefreshCards(object sender, EventArgs e)
        {
            CardList.RefreshList();
        }

        private void OnSave(object sender, EventArgs e)
        {
            deckEditor.SaveDeck();
        }
        private void OnCancel(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }


    }
}