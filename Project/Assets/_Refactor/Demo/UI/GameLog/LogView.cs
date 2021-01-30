using LDF.Systems;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using LDF.UserInterface.MVC;

namespace LDF.Roglik.UI
{
    public class LogView : View
    {
        [SerializeField]
        private TMP_Text _logsText;

        private Log _log = new Log(16);

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Write("\"Floor was wet and swear I could hear someones footsteps.\"");
        }

        private void OnEnable()
        {
            TurnSystemBehaviour.LogMessageSent += Write;
        }

        private void OnDisable()
        {
            TurnSystemBehaviour.LogMessageSent -= Write;
        }

        private void Write(string message)
        {
            _log.AddMessage(message);

            _logsText.text = _log.ToString();
        }

        private class Log //LogMessage to GameLog, message log could be class with logType to filter log messages and collapse diplicates
        {
            private uint _capacity;

            private Queue<string> _messages = new Queue<string>();

            public Log(uint capacity) => _capacity = capacity;

            public void AddMessage(string message)
            {
                if (_messages.Count >= _capacity)
                {
                    _messages.Dequeue();
                }

                _messages.Enqueue(message);
            }

            public override string ToString()
            {
                return string.Join("\n", _messages);
            }
        }
    }
}