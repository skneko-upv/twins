using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Twins.Models.Properties
{
    public class TurnProperty : INotifyPropertyChanged
    {
        public int turn;
        public int Turn {
            get => turn;
            set {
                turn = value;
                OnPropertyChanged(nameof(Turn));
            }
        }

        public int match;
        public int Match {
            get => match;
            set {
                match = value;
                OnPropertyChanged(nameof(Match));
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TurnProperty(int turn = 1, int match = 0)
        {
            Turn = turn;
            Match = match;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
