﻿using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Threading.Tasks;
using Twins.Components;
using Twins.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditDeckView : ContentPage
    {
        private readonly DeckEditor deckEditor;
        private int indexSelector;

        public EditDeckView()
        {
            InitializeComponent();
            deckEditor = new DeckEditor();
            deckEditor.CardsModified += RefreshCards;
            deckEditor.CategoriesModified += RefreshCategories;
            SelectorCategory.ItemsSource = deckEditor.GetCategories();
            indexSelector = -1;
        }

        private void RefreshCategories(object sender, EventArgs e)
        {
            SelectorCategory.ItemsSource = deckEditor.GetCategories();
            indexSelector = -1;
        }

        private void RefreshCards(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void OnSave(object sender, EventArgs e)
        {
            if (DeckName.Text != null && !DeckName.Text.Trim().Equals(""))
            {
                deckEditor.AddName(DeckName.Text);
            }
            else
            {
                ErrorView.SetTextError("Asigne un nombre para la baraja.");
                ErrorView.IsVisible = true;
                return;
            }
            if (deckEditor.Deck.Categories.Count <= 1)
            {
                ErrorView.SetTextError("Añada como mínimo 2 categorias a la baraja.");
                ErrorView.IsVisible = true;
                return;
            }
            if (deckEditor.Deck.Cards.Count > 6)
            {
                deckEditor.SaveDeck();
                Navigation.PopAsync();
            }
            else
            {
                ErrorView.SetTextError("Añada como mínino 7 cartas a la baraja.");
                ErrorView.IsVisible = true;
            }
        }
        private void OnCancel(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private async Task<FileData> OpenFileDialog()
        {
            try
            {
                string[] types = { ".png", ".jpg" };
                FileData fileData = await CrossFilePicker.Current.PickFile(types);
                if (fileData != null) // user canceled file picking
                {
                    return fileData;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private async void OnAddCard(object sender, EventArgs e)
        {
            Category category;
            if (indexSelector != -1 && SelectorCategory.SelectedItem != null)
            {
                category = (Category)SelectorCategory.SelectedItem;
            }
            else
            {
                indexSelector = -1;
                ErrorView.SetTextError("Seleccione primero una categoría o cree una antes de añadir una imagen.");
                ErrorView.IsVisible = true;
                return;
            }
            FileData file = await OpenFileDialog();
            if (file == null)
            {
                return;
            }

            deckEditor.AddCard(ImageSource.FromStream(file.GetStream), category);
        }

        private void RefreshList()
        {
            ListCard.Children.Clear();
            foreach (Card card in deckEditor.Deck.Cards)
            {
                CardComponent cardComponent = new CardComponent(card);
                ListCard.Children.Add(cardComponent.SetToEdit());
                ImageButton imageButton = new ImageButton
                {
                    Source = "Assets/Icons/delete.png",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                };
                imageButton.Clicked += (sender, e) => deckEditor.RemoveCard(card.Id);
                ListCard.Children.Add(imageButton);
            }

        }


        private void OnAddCategory(object sender, EventArgs e)
        {
            if (NewCategory.Text != null && !NewCategory.Text.Trim().Equals(""))
            {
                deckEditor.AddCategory(NewCategory.Text.Trim());
            }

            NewCategory.Text = "";
        }

        private void SelectorCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectorCategory.SelectedItem != null)
            {
                indexSelector = SelectorCategory.SelectedIndex;
            }
        }

        private async void OnAddBackCard(object sender, EventArgs e)
        {
            FileData file = await OpenFileDialog();
            if (file == null)
            {
                return;
            }

            backImage.Source = ImageSource.FromStream(file.GetStream);
            deckEditor.AddBackImage(backImage.Source);
        }

        private void OnRemoveCategory(object sender, EventArgs e)
        {
            if (indexSelector != -1 && SelectorCategory.SelectedItem != null)
            {
                deckEditor.RemoveCategory(((Category)SelectorCategory.SelectedItem));
            }
        }
    }
}