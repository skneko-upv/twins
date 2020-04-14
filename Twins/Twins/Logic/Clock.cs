using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace Twins.Logic
{
    //Clase que implementa la funcionalidad de cronómetro y temporizador 
    public class Clock
    {
        private Stopwatch clock = null;
        // Variable usada por el temporizador
        private TimeSpan timeLimit = TimeSpan.MaxValue;
        private int turn = 1;

        //Inicializa el Stopwatch
        private void initializeStopwatch() { clock = new Stopwatch(); }

        //Inicializa el cronómetro
        public Clock() { initializeStopwatch(); }

        //Inicializa el temporizador
        public Clock(String maxTime) {
            initializeStopwatch();
            timeLimit = stringToTimeSpan(maxTime);
        }
        
        //Devuelve referencia al cronómetro
        public Stopwatch getStopwatch() { return clock; }

        public void startStopwatch() { clock.Start(); }

        public void stopStopwatch() { clock.Stop(); }

        public void restartStopwatch() { clock.Restart(); }

        public void nextTurn() {
            turn++;
            restartStopwatch();
        }

        public int getTurn() { return turn; }

        //Convierte el tiempo actual del Stopwatch en TimeSpan y lo devuelve
        public TimeSpan getTimeSpan(Stopwatch reloj) { return new TimeSpan(0, 0, 0, 0, (int)reloj.ElapsedMilliseconds); }

        //Necesita un formato MM:SS o MM:SS:mmm
        public static TimeSpan stringToTimeSpan(String time) {
            string[] separateNumbers = time.Split(':');
            if (separateNumbers.Length < 2 || separateNumbers.Length > 3) { throw new FormatException(); }

            return new TimeSpan(0, 0, System.Convert.ToInt32(separateNumbers[0]),
                System.Convert.ToInt32(separateNumbers[1]), separateNumbers.Length == 2 ? 0 : System.Convert.ToInt32(separateNumbers[2])); 
        }

        //Transforma un TimeSpan proporcionado a String de formato MM:SS:mmm
        //Anotación: Acepta un formato HH:MM:SS:mmm, los dias no se cuentan
        public String timeSpanToString(TimeSpan tiempo) {
            String minutesElapsed = ((System.Convert.ToInt32(tiempo.Hours.ToString())) * 60
                                    + (System.Convert.ToInt32(tiempo.Minutes.ToString())).ToString().Length) < 2 ?
                                                            "0" + tiempo.Minutes.ToString() : tiempo.Minutes.ToString();
            String secondsElapsed = tiempo.Seconds.ToString().Length < 2 ? "0" + tiempo.Seconds.ToString() : tiempo.Seconds.ToString();
            String millisecondsElapsed = tiempo.Milliseconds.ToString();

            return minutesElapsed + ":" + secondsElapsed + ":" + millisecondsElapsed;
        }

        //Devuelve el tiempo transcurrido o restante dependiendo si es cronómetro o temporizador en MM:SS:mmm
        public String toString() {
            //Para diferenciar si es cronómetro o temporizador se usa la variable 'timeLimit' 
            // inicializada al MaxValue de TimeSpan para selañar que es un cronómetro
            return timeSpanToString(timeLimit.Equals(TimeSpan.MaxValue) ? getTimeSpan(clock) : timeLimit - getTimeSpan(clock));
        }
    }
}
