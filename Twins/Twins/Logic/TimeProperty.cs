using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Twins.Logic
{
    public class TimeProperty : INotifyPropertyChanged
    {
        public string time;
        public string Time
        {
            get { return time; }
            set
            {
                time = value.Substring(3);
                OnPropertyChanged(nameof(Time));
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TimeProperty(string time = "00:00")
        {
            Time = time;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
