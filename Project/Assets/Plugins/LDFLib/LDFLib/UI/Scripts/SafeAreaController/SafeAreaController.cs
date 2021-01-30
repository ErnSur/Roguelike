using System.Collections.Generic;
using UnityEngine;

namespace LDF.UserInterface.SafeArea
{
    public class SafeAreaController : MonoBehaviour
    {
		private static readonly string _prefabName = "TA Deco";
		private static readonly string _prefabPathFormat = "SafeAreaController/{0}/{1}";
  
        [SerializeField]
        private LogoPresenter _logoPresenter;
        [SerializeField]
        private List<RectTransform> _safeAreaList;

        private GameObject _frameDecoration;
        private Vector2 _anchorMin;
        private Vector2 _anchorMax;

        public bool IsSafeAreaOn { get; internal set; }

        private void UpdateSafeArea(Rect _safeArea, Vector2 _screen)
        {
            // Get Safe Area and calc anchors
            _anchorMin.x = _safeArea.x / _screen.x;
            _anchorMin.y = _safeArea.y / _screen.y;
            _anchorMax.x = (_safeArea.x + _safeArea.width) / _screen.x;
            _anchorMax.y = (_safeArea.y + _safeArea.height) / _screen.y;

            // Set anchors for targets
            foreach (RectTransform rt in _safeAreaList)
            {
                rt.anchorMin = _anchorMin;
                rt.anchorMax = _anchorMax;
            }
        }

        private void OnRectTransformDimensionsChange()
        {
            if (Screen.safeArea.width >= Screen.width
                && Screen.safeArea.height >= Screen.height)
            {
                _logoPresenter?.ShowFullLogo();

                return;
            }

            var safeArea = Screen.safeArea;
            if (Screen.orientation == ScreenOrientation.Portrait)
            {
                //hardcoded values for IPhone X
                safeArea.y = 0;
                safeArea.height += 153;

                _logoPresenter?.ShowCubeLogo();
            }
            else if (Screen.orientation == ScreenOrientation.Landscape)
            {
                //hardcoded values for IPhone X
                safeArea.y = 0;
                safeArea.x -= 30;
                safeArea.width += 132;

                _logoPresenter?.ShowFullLogo();
            }

            UpdateSafeArea(safeArea, new Vector2(Screen.width, Screen.height));
        }

        public void ShowSafeAreaFrame(SafeAreaEmulatorMode mode)
        {
            switch (mode)
            {
                case SafeAreaEmulatorMode.IPHONE_X_PORTRAIT:
                    {
                        _logoPresenter?.ShowCubeLogo();
                        UpdateSafeArea(new Rect(0, 0, 1125, 2355), new Vector2(1125.0f, 2436.0f));
                    }
                    break;
                case SafeAreaEmulatorMode.IPHONE_X_LANDSCAPE:
                    {
                        _logoPresenter?.ShowFullLogo();
                        UpdateSafeArea(new Rect(102, 0, 2334, 1125), new Vector2(2436.0f, 1125.0f));
                    }
                    break;
            }
        }

        public void HideSafeAreaFrame()
        {
            UpdateSafeArea(new Rect(0, 0, 1, 1), new Vector2(1, 1));
            _logoPresenter?.HideLogo();
        }

        // only for simulations
#if UNITY_EDITOR
        public void SetupDeviceFrame(SafeAreaEmulatorMode _type)
        {
            // Get prefab path
            var _prefabPath = string.Format(_prefabPathFormat, _type.ToString(), _prefabName);

            // Make and setup deco game object
            GameObject prefab = Resources.Load(_prefabPath) as GameObject;
            _frameDecoration = Instantiate(prefab) as GameObject;
            _frameDecoration.name = _prefabName;

            RectTransform frameDecoRectTransform = _frameDecoration.GetComponent<RectTransform>();
            frameDecoRectTransform.SetParent(this.transform);

            frameDecoRectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            frameDecoRectTransform.offsetMin = new Vector2(0.0f, 0.0f);
            frameDecoRectTransform.offsetMax = new Vector2(0.0f, 0.0f);

            // Setup Scale (because canvas base scale)
            Vector3 scaleFactor = this.GetComponent<RectTransform>().localScale;
            foreach (Transform tr in _frameDecoration.transform)
            {
                RectTransform temp = tr.GetComponent<RectTransform>();
                temp.localScale = new Vector3(temp.localScale.x / scaleFactor.x, temp.localScale.y / scaleFactor.y, temp.localScale.z / scaleFactor.z);
            }

        }

        public void DeleteDeviceFrame()
        {
            if (Application.isEditor)
            {
                DestroyImmediate(_frameDecoration);
            }
            else
            {
                Destroy(_frameDecoration);
            }
        }
#endif
    }
}