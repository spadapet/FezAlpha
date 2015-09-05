using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FezAlpha
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public AlphaGrid CurrentGrid
        {
            get { return _tabs.SelectedContent as AlphaGrid; }
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs eventArgs)
        {
            for (int i = 0; i < App.TabCount; i++)
            {
                TabItem tab = new TabItem();
                tab.Header = string.Format("Tab {0}", i + 1);
                _tabs.Items.Add(tab);

                AlphaGrid grid = new AlphaGrid(App.Current.GetMappingItems(i));
                tab.Content = grid;
            }
        }
    }
}
