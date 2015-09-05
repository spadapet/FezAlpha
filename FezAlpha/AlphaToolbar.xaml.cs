using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FezAlpha
{
    internal partial class AlphaToolbar : UserControl
    {
        private IList<LetterMappingItem> _mappings;

        public AlphaToolbar()
        {
            _mappings = new LetterMappingItem[27];

            for (char ch = 'a'; ch <= 'z'; ch++)
            {
                _mappings[ch - 'a'] = new LetterMappingItem(App.Current.GetMapping(ch));
            }

            _mappings[26] = new LetterMappingItem(App.Current.GetMapping(' '));

            InitializeComponent();
        }

        public IList<LetterMappingItem> Mappings
        {
            get { return _mappings; }
        }

        private void OnItemClick(object sender, RoutedEventArgs eventArgs)
        {
            Button button = sender as Button;

            if (button != null && button.DataContext is LetterMappingItem)
            {
                _list.SelectedValue = button.DataContext;

                LetterMappingItem letterMappingItem = (LetterMappingItem)button.DataContext;
                MainWindow mainWindow = (MainWindow)App.Current.MainWindow;

                if (letterMappingItem.Mapping != null && mainWindow.CurrentGrid != null)
                {
                    // Assign the Fez letter to the current list selection

                    LetterMappingItem item = mainWindow.CurrentGrid.SelectedItem;

                    if (item != null)
                    {
                        item.Mapping = letterMappingItem.Mapping;
                    }

                    // Move the list selection down by one

                    FrameworkElement elem = (FrameworkElement)mainWindow.CurrentGrid._list.ItemContainerGenerator.ContainerFromItem(item);
                    if (elem != null)
                    {
                        elem = (FrameworkElement)elem.PredictFocus(FocusNavigationDirection.Down);

                        if (elem != null)
                        {
                            mainWindow.CurrentGrid.SelectedItem = elem.DataContext as LetterMappingItem;
                        }

                        App.Current.UpdatePercentages();
                    }
                }
            }
        }
    }
}
