using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LDF.UserInterface.MVC;
using LDF.Utility;

namespace LDF.Roglik.UI
{
    public class StatusView : View<StatusModel>
    {
        [SerializeField]
        private Image _healthBar, _fearBar;

        private float _hpBarInitWidth, _fearBarInitWidth;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _hpBarInitWidth = _healthBar.rectTransform.rect.width;
            _fearBarInitWidth = _fearBar.rectTransform.rect.width;
        }

        public void UpdateStatusBar(Bar bar, float fillPercentage)
        {
            var barImage = bar == Bar.Health ? _healthBar : _fearBar;

            var barWidth = bar == Bar.Health ? _hpBarInitWidth : _fearBarInitWidth;

            var newWidth = fillPercentage * barWidth;

            barImage.rectTransform.sizeDelta = new Vector2(newWidth, barImage.rectTransform.sizeDelta.y);
        }

        public enum Bar { Health, Fear }
    }
}
