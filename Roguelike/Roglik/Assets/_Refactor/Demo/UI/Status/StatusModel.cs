using System;
using UnityEngine;
using LDF.UserInterface.MVC;

namespace LDF.Roglik.UI
{
    public class StatusModel : Model
    {
        public float PlayerHpPercentage;
        public Action OnPlayerDamaged;

#if UNITY_EDITOR
        [ContextMenu("Trigger Event")]
        private void TriggerEvent() => OnPlayerDamaged.Invoke();
#endif
    }
}
