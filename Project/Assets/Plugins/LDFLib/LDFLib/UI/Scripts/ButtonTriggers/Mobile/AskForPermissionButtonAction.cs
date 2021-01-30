using System;
using System.Collections.Generic;
using UnityEngine;
using LDF.UserInterface.MessageBox;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

namespace LDF
{
    public class AskForPermissionButtonAction : ButtonActionSequenceElement
    {
        [SerializeField]
        private MessageBoxContent popupContent;

        private Queue<ButtonActionSequenceElement> _triggerSequence;

        public override void OnClick(Queue<ButtonActionSequenceElement> triggerSequence)
        {
            _triggerSequence = triggerSequence;
            AskForCameraPermission();
        }

        public void AskForCameraPermission()
        {
#if PLATFORM_IOS && !UNITY_EDITOR
            iOSCameraPermission.VerifyPermission(gameObject.name, nameof(OnCameraPermisionVerification));
#elif PLATFORM_ANDROID && !UNITY_EDITOR
            if (AndroidRuntimePermissions.CheckPermission(Permission.Camera) != AndroidRuntimePermissions.Permission.Granted)
            {
                AndroidRuntimePermissions.RequestPermission(Permission.Camera);
            }
            OnCameraPermisionVerification(Permission.HasUserAuthorizedPermission(Permission.Camera).ToString().ToLower());
#elif UNITY_EDITOR
            base.OnClick(_triggerSequence);
#endif
        }

        /// This is a special callback method that is required by <see cref="iOSCameraPermission.VerifyPermission"/> 
        /// It MUST implement one string parameter that can be either "true" or "false". 
        public void OnCameraPermisionVerification(string permissionWasGranted)
        {
            if (permissionWasGranted == "true")
            {
                base.OnClick(_triggerSequence);
            }
            else
            {
                MessageBox.Show(popupContent);
            }
        }
    }
}
