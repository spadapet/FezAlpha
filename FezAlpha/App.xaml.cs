using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Text;

namespace FezAlpha
{
    internal partial class App : Application
    {
        public const int Columns = 64;
        public const int Rows = 16;
        public const int AllRowsHeight = Rows * 32;
        public const int TabCount = 4;

        private ImageSource[] _fezLetters;
        private LetterMapping[] _mappings;
        private LetterMappingItem[][] _items;

        public App()
        {
            _fezLetters = new ImageSource[27];
            _mappings = new LetterMapping[27];
            _items = new LetterMappingItem[TabCount][];

            BitmapImage allLetters = new BitmapImage(new Uri("pack://application:,,/fezletters.png"));

            for (int i = 0; i < 27; i++)
            {
                char ch = (i == 26) ? ' ' : (char)(i + 'a');

                _fezLetters[i] = new CroppedBitmap(allLetters, new Int32Rect(i * 32, 0, 32, 32));
                _mappings[i] = new LetterMapping(ch, string.Empty);
            }

            for (int i = 0; i < _items.Length; i++)
            {
                _items[i] = new LetterMappingItem[Columns * Rows];

                for (int h = 0; h < _items[i].Length; h++)
                {
                    _items[i][h] = new LetterMappingItem(GetMapping(' '));
                }
            }

            Load();
        }

        protected override void OnExit(ExitEventArgs eventArgs)
        {
            Save();

            base.OnExit(eventArgs);
        }

        public static new App Current
        {
            get { return (App)Application.Current; }
        }

        public LetterMapping GetMapping(char ch)
        {
            if (ch == ' ')
            {
                ch = (char)('z' + 1);
            }

            if (ch - 'a' >= 0 && ch - 'a' < _mappings.Length)
            {
                return _mappings[ch - 'a'];
            }

            return null;
        }

        public IList<LetterMappingItem> GetMappingItems(int tab)
        {
            return _items[tab];
        }

        public ImageSource GetFezLetterImage(char ch)
        {
            if (ch == ' ')
            {
                ch = (char)('z' + 1);
            }

            if (ch - 'a' >= 0 && ch - 'a' < _fezLetters.Length)
            {
                return _fezLetters[ch - 'a'];
            }

            return null;
        }

        public IList<LetterMapping> Mappings
        {
            get { return _mappings; }
        }

        public void UpdatePercentages()
        {
            int total = 0;
            int[] count = new int[26];

            for (int i = 0; i < _items.Length; i++)
            {
                for (int h = 0; h < _items[i].Length; h++)
                {
                    char ch = _items[i][h].Mapping.FezLetter;

                    if (ch >= 'a' && ch <= 'z')
                    {
                        count[ch - 'a']++;
                        total++;
                    }
                }
            }

            for (int i = 0; i < count.Length; i++)
            {
                _mappings[i].Percent = (count[i] != 0) ? (double)count[i] / (double)total : 0.0;
            }
        }

        private static string SavePath
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Fez Letters.txt"); }
        }

        private void Load()
        {
            if (File.Exists(SavePath))
            {
                string text = string.Empty;

                try
                {
                    text = File.ReadAllText(SavePath);
                }
                catch
                {
                    return;
                }

                StringReader sr = new StringReader(text);

                for (int i = 0; i < _mappings.Length; i++)
                {
                    text = sr.ReadLine();

                    _mappings[i].FezLetter = (char)('a' + i);
                    _mappings[i].Text = text.Substring(2);
                }

                for (int i = 0; i < _items.Length; i++)
                {
                    text = sr.ReadLine();

                    for (int h = 0; h < _items[i].Length; h++)
                    {
                        char ch = text[h];
                        _items[i][h].Mapping = GetMapping(ch);
                    }
                }

                UpdatePercentages();
            }
        }

        private void Save()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _mappings.Length; i++)
            {
                sb.Append(_mappings[i].FezLetter.ToString());
                sb.Append(':');
                sb.Append(_mappings[i].Text);
                sb.AppendLine();
            }

            for (int i = 0; i < _items.Length; i++)
            {
                for (int h = 0; h < _items[i].Length; h++)
                {
                    sb.Append(_items[i][h].Mapping.FezLetter);
                }

                sb.AppendLine();
            }

            try
            {
                File.WriteAllText(SavePath, sb.ToString());
            }
            catch
            {
                return;
            }
        }
    }
}
