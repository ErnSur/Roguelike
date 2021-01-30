using System;
using UnityEngine;
using LDF.Utility;

namespace LDF.UserInterface.MessageBox
{
    public class MessageBoxPresenter : MonoBehaviour
    {
        [Header("MessageBox Pool settings:")]
        [SerializeField]
        private GameObject _messageBoxPrototype;

        [SerializeField]
        private uint _poolStartSize;

        internal static MessageBoxPresenter Instance { get; private set; }

        private PoolingStore _messageBoxPool;
        private void Awake()
        {
            AssignInstance();
            _messageBoxPool = new PoolingStore(this.transform, _messageBoxPrototype, _poolStartSize);
        }

        internal void ShowMessageBox(MessageBoxContent content, Action showCallback, Action hideCallback)
        {
            var messageController = _messageBoxPool.GetFromPool().GetComponent<MessageBoxController>();

            messageController.ShowMessageBox(content, showCallback);

            messageController.OnHide += (obj) =>
            {
                _messageBoxPool.ReturnToPool(obj);
                hideCallback?.Invoke();
            };
        }

        private void AssignInstance()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }

            Instance = this;
        }
    }
}