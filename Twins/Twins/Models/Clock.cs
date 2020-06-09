using System;
using System.Diagnostics;
using Twins.Models.Properties;

namespace Twins.Models
{
    //Clase que implementa la funcionalidad de cronómetro y temporizador 
    public class Clock : IDisposable
    {
        public TimeProperty TimeLeft { get; }
        public bool IsCountingDown { get; private set; } = false;
        public event Action TimedOut;
        public event Action Resumed;
        public event Action Stopped;

        public TimeSpan TimeLimit { get; }

        // Variable usada por el temporizador
        private readonly Stopwatch clock;
        private readonly System.Timers.Timer eventTimeout;

        private bool isDisposed;

        //Inicializa el cronómetro
        public Clock() { clock = new Stopwatch(); }

        //Inicializa el temporizador
        public Clock(TimeSpan maxTime) : this()
        {
            IsCountingDown = true;
            TimeLimit = maxTime;
            TimeLeft = new TimeProperty();

            eventTimeout = new System.Timers.Timer(500.0);
            eventTimeout.Elapsed += (_0, _1) =>
            {
                TimeLeft.Time = GetTimeSpan().ToString(@"hh\:mm\:ss");
                if (clock.ElapsedMilliseconds >= TimeLimit.TotalMilliseconds)
                {
                    TimedOut();
                }
            };
            eventTimeout.AutoReset = true;
            eventTimeout.Enabled = true;
        }


        public void Start()
        {
            clock.Start();
            Resumed?.Invoke();
        }

        public void Stop()
        {
            clock.Stop();
            Stopped?.Invoke();
        }

        public void Restart()
        {
            clock.Restart();
            Resumed?.Invoke();
        }

        public void Reset()
        {
            clock.Reset();
            Stopped?.Invoke();
        }

        public bool IsRunning() { return clock.IsRunning; }

        //Convierte el tiempo actual del Stopwatch en TimeSpan y lo devuelve
        public TimeSpan GetTimeSpan()
        {
            TimeSpan elapsedTime = new TimeSpan(0, 0, 0, 0, (int)clock.ElapsedMilliseconds);
            return IsCountingDown ? TimeLimit - elapsedTime : elapsedTime;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }

            if (disposing)
            {
                eventTimeout.Dispose();
            }

            isDisposed = true;
        }
    }
}
