using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Twins.Logic
{
    public class TimeProperty : INotifyPropertyChanged
    {
        public int minutes;
        public int Minutes
        {
            get { return minutes; }
            set
            {
                minutes = value;
                OnPropertyChanged(nameof(minutes));
            }
        }
        public int seconds;
        public int Seconds
        {
            get { return seconds; }
            set
            {
                seconds = value;
                OnPropertyChanged(nameof(seconds));
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TimeProperty(int minutes = 0, int seconds = 0)
        {
            Minutes = minutes;
            Seconds = seconds;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
