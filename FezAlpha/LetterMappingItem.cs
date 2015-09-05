using System.ComponentModel;

namespace FezAlpha
{
    /// <summary>
    /// This is the data model for lists that contain letter mappings
    /// </summary>
    internal class LetterMappingItem : INotifyPropertyChanged
    {
        private LetterMapping _mapping;

        public LetterMappingItem(LetterMapping mapping)
        {
            _mapping = mapping;
        }

        public LetterMapping Mapping
        {
            get { return _mapping; }

            set
            {
                if (_mapping != value)
                {
                    _mapping = value;
                    OnPropertyChanged("Mapping");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
