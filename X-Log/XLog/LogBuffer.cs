using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace X_Log.XLog
{
    public class LogBuffer
    {
        private readonly Timer _autoFlushTimer;
        private readonly Queue<string> _queue = new Queue<string>();
        private readonly object _queueLock = new object();

        public readonly int AutoLogDelay;
        public readonly string LogFileName;
        public readonly int MaxQueueLength;

        private int _flushing;

        public LogBuffer(int maxQueueLength, int autoLogDelay)
        {
            var now = DateTime.Now;

            MaxQueueLength = maxQueueLength;
            AutoLogDelay = autoLogDelay;
            LogFileName = "xlog-" + now.Day + "-" + now.Month + "-" + now.Year + ".txt";
            _autoFlushTimer = new Timer(obj => Flush(), null, AutoLogDelay, AutoLogDelay);

            Add("Created " + nameof(LogBuffer) + " (" + nameof(MaxQueueLength) + ": " + MaxQueueLength + ", " +
                nameof(AutoLogDelay) + ": " + AutoLogDelay + ")");
            Flush();
        }

        public async void Flush()
        {
            if (Interlocked.CompareExchange(ref _flushing, 1, 0) == 0)
            {
                var lines = new List<string>();
                lock (_queueLock)
                {
                    lines.AddRange(_queue);
                    _queue.Clear();
                }

                Console.WriteLine("Logging " + lines.Count + " messages");
                try
                {
                    await File.AppendAllLinesAsync(LogFileName, lines);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    _flushing = 0;
                }
            }
        }

        public void Add(string s)
        {
            int queueLength;
            lock (_queueLock)
            {
                _queue.Enqueue(CreateLogString(s));
                queueLength = _queue.Count;
            }

            if (queueLength >= MaxQueueLength) Flush();
        }

        private string CreateLogString(string s)
        {
            var now = DateTime.Now;
            var hour = now.Hour.ToString().PadLeft(2, '0');
            var minute = now.Minute.ToString().PadLeft(2, '0');
            var second = now.Second.ToString().PadLeft(2, '0');
            return "XLog[" + hour + ":" + minute + ":" + second + "]: " + s;
        }
    }
}