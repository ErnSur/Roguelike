using UnityEngine;

namespace LDF.Utility
{
    public class Billboarding : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;

        public Transform Target
        {
            get { return _target; }
            set { _target = value; }
        }

        private void LateUpdate()
        {
            transform.rotation = Quaternion.LookRotation(_target.forward);
        }
    }
}