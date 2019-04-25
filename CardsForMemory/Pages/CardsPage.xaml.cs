using CardsForMemoryLibrary.Models;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages
{
    public sealed partial class CardsPage : Page
    {
        public List<CardHolder> CardHolders;
        public CardsPage()
        {
            CardHolder.CardHolders.Add(new CardHolder("shudian", "Karl", "asd"));
            CardHolder.CardHolders.Add(new CardHolder("modian", "Karlasd", "asvbcrtcbcvyd"));
            CardHolder.CardHolders.Add(new CardHolder("lishi", "Kasdfrl", "asycvbuaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaacid"));
            CardHolder.CardHolders.Add(new CardHolder("shuxue", "Karghfl", "aljkhscvbd"));
            CardHolders = CardHolder.CardHolders;
            this.InitializeComponent();
            CardsPageEditBtn.IsEnabled = false;
            CardsPagePlayBtn.IsEnabled = false;
        }

        private void CardsPageListViewSelectionChangeHandler(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem as CardHolder;
            selectedName.Text = "Name: " + item.Name;
            selectedAuthor.Text = "Author: " + item.Author;
            selectedCreateTime.Text = "CreateTime: " + item.CreateTime.ToString();
            selectedUpdateTime.Text = "UpdateTime: " + item.UpdateTime.ToString();
            selectedDescription.Text = "Description: " + item.Description;
            CardsPageEditBtn.IsEnabled = true;
            CardsPagePlayBtn.IsEnabled = true;
        }

        private void CardsPageAddBtnClickHandler(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (null == CardsPageAddBtn.Flyout)
            {
                CardsPageAddBtn.Flyout = new Flyout { Content = new TextBlock { Text = "Not implement Button!" } };
            }
        }

        private void CardsPageEditBtnClickHandler(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void CardsPagePlayBtnClickHandler(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RememberPage), 1);
        }
    }
}
