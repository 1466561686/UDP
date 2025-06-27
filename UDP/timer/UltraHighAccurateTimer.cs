using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace UDP.timer
{
    public class UltraHighAccurateTimer
    {
        public delegate void ManualTimerEventHandler(object sender);

        public event ManualTimerEventHandler Tick;

        private long clockFrequency;
        private bool running = false;
        private Thread timerThread;

        public int intervalMs;
        private long intevalTicks;

        public int Interval
        {
            get { return intervalMs; }
            set
            {
                intervalMs = value;
                intevalTicks = (long)((double)value * (double)clockFrequency / (double)1000);
            }
        }

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        public UltraHighAccurateTimer()
        {
            if (QueryPerformanceFrequency(out clockFrequency) == false)
            {
                throw new Win32Exception("QueryPerformanceFrequency() function is not supported");
            }
        }

        // 进程主程序
        private void ThreadProc()
        {
            long currTime;
            long nextTriggerTime;
            GetTick(out currTime);
            nextTriggerTime = currTime + intevalTicks;
            while (running)
            {
                GetTick(out currTime);
                long sleepTicks = nextTriggerTime - currTime;
                if (sleepTicks > 0)
                {
                    // 将剩余时间转换为毫秒并留出1ms缓冲
                    int sleepMs = (int)(sleepTicks * 1000 / clockFrequency) - 1;
                    sleepMs = Math.Max(sleepMs, 0); // 确保睡眠时间不为负
                    if (sleepMs > 0)
                    {
                        Thread.Sleep(sleepMs);
                    }
                    else
                    {
                        // 极短等待时让出CPU
                        Thread.Sleep(0); // 让出CPU时间片给其他线程
                    }
                }
                // 精确等待剩余时间
                while (currTime < nextTriggerTime)
                {
                    GetTick(out currTime);
                }
                nextTriggerTime = currTime + intevalTicks;
                if (Tick != null)
                {
                    // 使用异步调用避免阻塞定时器线程
                    Tick.BeginInvoke(this, null, null);
                }
            }
        }

        public bool GetTick(out long currentTickCount)
        {
            if (QueryPerformanceCounter(out currentTickCount) == false)
                throw new Win32Exception("QueryPerformanceCounter() failed!");
            else
                return true;
        }

        public void Start()
        {
            running = true;
            timerThread = new Thread(new ThreadStart(ThreadProc));
            timerThread.Name = "HighAccuracyTimer";
            timerThread.Priority = ThreadPriority.BelowNormal;
            timerThread.Start();
        }

        public void Stop()
        {
            running = false;
            if (timerThread != null && timerThread.IsAlive)
            {
                // 等待线程退出
                timerThread.Join(500); // 最多等待500ms
                if (timerThread.IsAlive)
                {
                    timerThread.Abort(); // 超时后强制终止
                }
                timerThread = null;
            }
        }

        ~UltraHighAccurateTimer()
        {
            running = false;
            if (timerThread != null)
            {
                timerThread.Abort();
            }
        }
    }
}