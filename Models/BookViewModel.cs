using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace booklook.Models {
    public class BookViewModel : INotifyPropertyChanged {
        private string _title;
        public string Title {
            get => _title; set {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _authors;
        public string Authors {
            get => _authors; set {
                _authors = value;
                OnPropertyChanged();
            }
        }

        private string _imageSource;
        public string ImageSource {
            get => _imageSource; set {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        private string _isbn13;
        public string Isbn13 {
            get => _isbn13; set {
                _isbn13 = value;
                OnPropertyChanged();
            }
        }

        private string _isbn10;
        public string Isbn10 {
            get => _isbn10; set {
                _isbn10 = value;
                OnPropertyChanged();
            }
        }

        private string _releaseDate;
        public string ReleaseDate {
            get => _releaseDate; set {
                _releaseDate = value;
                OnPropertyChanged();
            }
        }

        private string _booklink;
        public string BookLink {
            get => _booklink; set {
                _booklink = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;

        public bool IsLoading {
            get => _isLoading; set {
                _isLoading = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ReverseIsLoading));
            }
        }
        public bool ReverseIsLoading { get => !IsLoading; }

        /// <summary>
        ///     Notify on property change helper method
        /// </summary>
        /// <param name="name"></param>
        private void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Call OpenLink method with a command
        /// </summary>
        public ICommand OpenUrlCommand => new Command<string>(OpenLink);

        /// <summary>
        ///     Open a link in a browser
        /// </summary>
        /// <param name="url"></param>
        private async void OpenLink(string url) {
            await Browser.OpenAsync(url);
        }

    }
}
