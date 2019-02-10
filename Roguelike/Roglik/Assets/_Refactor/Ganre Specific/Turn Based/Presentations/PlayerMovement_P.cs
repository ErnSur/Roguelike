using System;
using System.Collections;
using System.Collections.Generic;
using LDF.Systems;
using UnityEngine;

namespace LDF.Presentations.TurnBased
{
    [RequireComponent(typeof(PlayerMovement_S))]
    public class PlayerMovement_P : MonoBehaviour
    {
        private void Awake()
        {
            
        }
    }

    public class PresentationAction
    {
        private Action _onEnd;
        
        

        public virtual void OnEnd(Action onEnd)
        {
            _onEnd = onEnd;
        }
        
        
    }
}
