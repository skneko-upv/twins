using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Twins.Logic
{
    public class TurnProperty : INotifyPropertyChanged
    {
        public int turn;
        public int Turn 
        {
            get { return turn; }
            set 
            {
                turn = value;
                OnPropertyChanged(nameof(Turn));
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TurnProperty(int turn = 1) {
            Turn = turn;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
