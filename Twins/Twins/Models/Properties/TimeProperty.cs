using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Twins.Models.Properties
{
    public class TimeProperty : INotifyPropertyChanged
    {
        public Color color;
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
        public string time;
        public string Time {
            get => time;
            set {
                time = value.Substring(3);
                if (Int32.Parse(value.Substring(6)) <= 2)
                    Color = Color.Red;
                else
                    Color = Color.White;
                OnPropertyChanged(nameof(Time));
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TimeProperty(string time = "00:00:00")
        {
            Time = time;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
