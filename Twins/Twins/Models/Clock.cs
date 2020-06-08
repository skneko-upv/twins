using System;
using System.Diagnostics;
using System.Threading;
using Twins.Models.Properties;
using Xamarin.Forms;

namespace Twins.Models
{
    //Clase que implementa la funcionalidad de cronómetro y temporizador 
    public class Clock
    {
        public TimeProperty TimeLeft { get; }
        public bool IsCountingDown { get; private set; } = false;
        public event Action TimedOut;
        public TimeSpan TimeLimit { get; }

        // Variable usada por el temporizador
        private readonly Stopwatch clock;
        private readonly System.Timers.Timer eventTimeout;

        //Inicializa el cronómetro
        public Clock() { clock = new Stopwatch(); }

        //Inicializa el temporizador
        public Clock(TimeSpan maxTime) : this()
        {
            IsCountingDown = true;
            timeLimit = maxTime;
            TimeLeft = new TimeProperty();

            eventTimeout = new System.Timers.Timer(500.0);
            eventTimeout.Elapsed += (_0, _1) =>
            {
                TimeLeft.Time = GetTimeSpan().ToString(@"hh\:mm\:ss");
                if (clock.ElapsedMilliseconds >= timeLimit.TotalMilliseconds)
                {
                    TimedOut();
                }
            };
            eventTimeout.AutoReset = true;
            eventTimeout.Enabled = true;
        }


        public void Start() { clock.Start(); }

        public void Stop() { clock.Stop(); }

        public void Restart() { clock.Restart(); }

        public void Reset() { clock.Reset(); }

        public bool IsRunning() { return clock.IsRunning; }

        //Convierte el tiempo actual del Stopwatch en TimeSpan y lo devuelve
        public TimeSpan GetTimeSpan()
        {
            TimeSpan elapsedTime = new TimeSpan(0, 0, 0, 0, (int)clock.ElapsedMilliseconds);
            return IsCountingDown ? timeLimit - elapsedTime : elapsedTime;
        }



        /*
        //Necesita un formato MM:SS o MM:SS:mmm
        public static TimeSpan StringToTimeSpan(String time) {
            string[] separateNumbers = time.Split(':');
            if (separateNumbers.Length < 2 || separateNumbers.Length > 3) { throw new FormatException(); }

            return new TimeSpan(0, 0, System.Convert.ToInt32(separateNumbers[0]),
                System.Convert.ToInt32(separateNumbers[1]), separateNumbers.Length == 2 ? 0 : System.Convert.ToInt32(separateNumbers[2])); 
        }

        //Transforma un TimeSpan proporcionado a String de formato MM:SS:mmm
        //Anotación: Acepta un formato HH:MM:SS:mmm, los dias no se cuentan
        public String TimeSpanToString(TimeSpan tiempo) {
            String minutesElapsed = ((System.Convert.ToInt32(tiempo.Hours.ToString())) * 60
                                    + (System.Convert.ToInt32(tiempo.Minutes.ToString())).ToString().Length) < 2 ?
                                                            "0" + tiempo.Minutes.ToString() : tiempo.Minutes.ToString();
            String secondsElapsed = tiempo.Seconds.ToString().Length < 2 ? "0" + tiempo.Seconds.ToString() : tiempo.Seconds.ToString();
            String millisecondsElapsed = tiempo.Milliseconds.ToString();

            return minutesElapsed + ":" + secondsElapsed + ":" + millisecondsElapsed;
        }

        //Devuelve el tiempo transcurrido o restante dependiendo si es cronómetro o temporizador en MM:SS:mmm
        public String ToString() {
            //Para diferenciar si es cronómetro o temporizador se usa la variable 'timeLimit' 
            // inicializada al MaxValue de TimeSpan para selañar que es un cronómetro
            return timeSpanToString(timeLimit.Equals(TimeSpan.MaxValue) ? getTimeSpan() : timeLimit - getTimeSpan());
        }
        */
    }
}
