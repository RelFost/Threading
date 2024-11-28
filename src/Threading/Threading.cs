using System;
using System.Threading;

namespace Relfost.Threading
{
    public class TimeoutThread
    {
        private readonly Thread _thread;        // Внутренний поток
        private int _timeoutSeconds;           // Тайм-аут в секундах
        private CancellationTokenSource _cts;  // Токен отмены
        private Action? _onTimeout;            // Коллбек при тайм-ауте
        private Action? _onSuccess;            // Коллбек при успешном завершении
        private Action<Exception>? _onError;   // Коллбек при ошибке

        public TimeoutThread(ThreadStart start)
        {
            _thread = new Thread(() =>
            {
                try
                {
                    start();
                    _onSuccess?.Invoke();
                }
                catch (Exception ex)
                {
                    _onError?.Invoke(ex);
                }
            });

            _cts = new CancellationTokenSource();
        }

        public Thread Thread => _thread;

        /// <summary>
        /// Устанавливает тайм-аут и коллбек для его обработки.
        /// </summary>
        public void Timedout(int seconds, Action? timeoutCallback = null)
        {
            _timeoutSeconds = seconds;
            _onTimeout = timeoutCallback;
        }

        /// <summary>
        /// Устанавливает коллбек для успешного завершения.
        /// </summary>
        public void OnSuccess(Action successCallback)
        {
            _onSuccess = successCallback;
        }

        /// <summary>
        /// Устанавливает коллбек для обработки ошибок.
        /// </summary>
        public void OnError(Action<Exception> errorCallback)
        {
            _onError = errorCallback;
        }

        /// <summary>
        /// Запускает поток с учетом тайм-аута.
        /// </summary>
        public void Start()
        {
            if (_timeoutSeconds > 0)
            {
                // Поток-контроллер
                Thread controller = new Thread(() =>
                {
                    if (!_thread.Join(_timeoutSeconds * 1000))
                    {
                        _onTimeout?.Invoke();
                        _cts.Cancel();
                        _thread.Abort();
                    }
                });

                controller.IsBackground = true;
                controller.Start();
            }

            _thread.Start();
        }
    }
}
