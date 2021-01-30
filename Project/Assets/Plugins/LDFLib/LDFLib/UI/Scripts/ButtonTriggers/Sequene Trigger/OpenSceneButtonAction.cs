using System.Collections.Generic;
using UnityEngine;
using LDF.Utility;

namespace LDF.UserInterface
{
    public class OpenSceneButtonAction : ButtonActionSequenceElement
    {
        [SerializeField]
        private BuildScene _scene;

        public override void OnClick(Queue<ButtonActionSequenceElement> triggerSequence)
        {
            SceneBlender.ChangeScene(_scene);

            base.OnClick(triggerSequence);
        }
    }
}
