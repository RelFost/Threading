using System;
using System.Threading;
using Relfost.Threading;

namespace Relfost.Support.Facades
{
    public class RThread
    {
        private readonly TimeoutThread _timeoutThread;

        public RThread(ThreadStart start)
        {
            _timeoutThread = new TimeoutThread(start);
        }

        public static RThread Create(ThreadStart start)
        {
            return new RThread(start);
        }

        /// <summary>
        /// Устанавливает тайм-аут и коллбек для его обработки.
        /// </summary>
        public void Timedout(int seconds, Action? timeoutCallback = null)
        {
            _timeoutThread.Timedout(seconds, timeoutCallback);
        }

        /// <summary>
        /// Устанавливает коллбек для успешного завершения.
        /// </summary>
        public void OnSuccess(Action successCallback)
        {
            _timeoutThread.OnSuccess(successCallback);
        }

        /// <summary>
        /// Устанавливает коллбек для обработки ошибок.
        /// </summary>
        public void OnError(Action<Exception> errorCallback)
        {
            _timeoutThread.OnError(errorCallback);
        }

        /// <summary>
        /// Запускает поток.
        /// </summary>
        public void Start()
        {
            _timeoutThread.Start();
        }

        /// <summary>
        /// Делает поток фоновым.
        /// </summary>
        public bool IsBackground
        {
            get => _timeoutThread.Thread.IsBackground;
            set => _timeoutThread.Thread.IsBackground = value;
        }
    }
}
