using System;

namespace LDF.Structures
{
    public interface IStatus
    {
        bool IsStatusOn { get; }
        float StatusDuration { get; }
        void AddDuration(float duration);
        void UpdateStatus(float stepSize);
    }

    //[Serializable]
    public class Status : IStatus
    {
        public float StatusDuration { get; private set; }

        public bool IsStatusOn => StatusDuration > 0;

        private readonly Action _onStatusUpdate;
        private readonly Action _onStatusEnd;
        
        private readonly Timer _timer;

        public Status(float duration, Action onUpdate, Action onEnd)
        {
            StatusDuration = duration;
            _onStatusUpdate = onUpdate;
            _onStatusEnd = onEnd;
            _timer = new Timer(duration);
        }

        public void UpdateStatus(float stepSize)
        {
            _timer.Countdown(stepSize,false,_onStatusEnd);
            _onStatusUpdate?.Invoke();
        }

        public void AddDuration(float duration)
        {
            StatusDuration += duration;
        }
    }
}