using UnityEngine;

namespace LDF.UserInterface.SafeArea
{
    public class LogoPresenter : MonoBehaviour
    {
        [SerializeField]
        private GameObject _smallLogo;
        [SerializeField]
        private GameObject _fullLogo;

        public void ShowCubeLogo()
        {
            _fullLogo.SetActive(false);
            _smallLogo.SetActive(true);
        }

        public void ShowFullLogo()
        {
            _smallLogo.SetActive(false);
            _fullLogo.SetActive(true);
        }

        public void HideLogo()
        {
            _smallLogo.SetActive(false);
            _fullLogo.SetActive(false);
        }
    }
}