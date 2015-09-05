using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace FezAlpha
{
    internal partial class AlphaGrid : UserControl
    {
        public int Columns { get { return App.Columns; } }
        public int Rows { get { return App.Rows; } }
        public int AllRowsHeight { get { return App.AllRowsHeight; } }

        private IList<LetterMappingItem> _mappings;

        public AlphaGrid(IList<LetterMappingItem> mappings)
        {
            _mappings = mappings;

            InitializeComponent();
        }

        public IList<LetterMappingItem> Mappings
        {
            get { return _mappings; }
        }

        private void OnItemClick(object sender, RoutedEventArgs eventArgs)
        {
            FrameworkElement elem = sender as FrameworkElement;

            if (elem != null)
            {
                _list.SelectedValue = elem.DataContext;
            }
        }

        public LetterMappingItem SelectedItem
        {
            get { return _list.SelectedValue as LetterMappingItem; }
            set { _list.SelectedValue = value; }
        }
    }
}
