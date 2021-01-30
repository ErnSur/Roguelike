using System;

namespace LDF.Structures
{   
    [Serializable]
    public class Timer
    {
        public event Action OnTimeUp;

        public float Duration { get; set; }
        public float TimeLeft { get; private set; }
        public bool TimeUpEventFired { get; private set; }

        public bool Finished => TimeLeft <= 0;
        public float CurrentDuration => Duration - TimeLeft;
        public float CompletionPercent => CurrentDuration / Duration;

        public Timer(float duration)
        {
            Duration = duration;
            Restart();
        }

        public void Restart()
        {
            TimeUpEventFired = false;
            TimeLeft = Duration;
        }

        public void Countdown(float stepSize, bool restartOnFinish = false, Action onTimeUp = null)
        {
            if(Finished && restartOnFinish)
                Restart();
        
            if(TimeLeft > 0)
                TimeLeft -= stepSize;

            if (Finished && !TimeUpEventFired)
            {
                TimeUpEventFired = true;
                CallOnTimeUpEvents(onTimeUp);
            }
        }

        private void CallOnTimeUpEvents(Action dynamicAction)
        {
            OnTimeUp?.Invoke();
            dynamicAction?.Invoke();
        }
    }
}