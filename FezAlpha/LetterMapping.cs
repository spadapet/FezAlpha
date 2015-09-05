using System.ComponentModel;
using System.Windows.Media;

namespace FezAlpha
{
    /// <summary>
    /// A mapping from a Fez letter to any text (usually a letter)
    /// </summary>
    class LetterMapping : INotifyPropertyChanged
    {
        private char _fezLetter;
        private string _text;
        private double _percent;

        public LetterMapping(char fezLetter, string text)
        {
            _fezLetter = fezLetter;
            _text = text ?? string.Empty;
        }

        public char FezLetter
        {
            get { return _fezLetter; }

            set
            {
                if (value != _fezLetter)
                {
                    _fezLetter = value;
                    OnPropertyChanged("FezLetter");
                    OnPropertyChanged("FezImage");
                }
            }
        }

        public string Text
        {
            get { return _text; }

            set
            {
                if (value != null && value != _text)
                {
                    _text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        public double Percent
        {
            get { return _percent; }
            set
            {
                if (_percent != value)
                {
                    _percent = value;
                    OnPropertyChanged("Percent");
                    OnPropertyChanged("PercentText");
                }
            }
        }

        public string PercentText
        {
            get { return (_percent * 100.0).ToString("F0"); }
        }

        public ImageSource FezImage
        {
            get { return App.Current.GetFezLetterImage(FezLetter); }
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
