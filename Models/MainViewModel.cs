using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace booklook.Models {
    public class MainViewModel : INotifyPropertyChanged {
        private bool _isScanning;
        public bool IsScanning {
            get => _isScanning; set {
                _isScanning = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Notify on property change helper method
        /// </summary>
        /// <param name="name"></param>
        private void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
