namespace LDF.EventSystem
{
    public abstract class Event { };

    public abstract class Event<T> : Event where T : Event<T>
    {
        public static void Add(EventManager.EventDelegate<T> action)
        {
            EventManager.AddListener(action);
        }

        public static void Remove(EventManager.EventDelegate<T> action)
        {
            EventManager.RemoveListener(action);
        }

        public static void Trigger(T payload)
        {
            EventManager.TriggerEvent(payload);
        }
    }
}