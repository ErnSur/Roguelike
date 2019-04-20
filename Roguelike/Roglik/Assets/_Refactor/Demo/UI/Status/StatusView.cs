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
        private RectTransform _healthBar, _fearBar;

        private float _hpBarInitWidth, _fearBarInitWidth;

        private void Awake()
        {
            _hpBarInitWidth = _healthBar.rect.width;
            _fearBarInitWidth = _fearBar.rect.width;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            UpdateStatusBar(Bar.Health, Model.PlayerHpPercentage);
        }

        public void UpdateStatusBar(Bar bar, float fillPercentage)
        {
            var barImage = bar == Bar.Health ? _healthBar : _fearBar;

            var barWidth = bar == Bar.Health ? _hpBarInitWidth : _fearBarInitWidth;

            var newWidth = fillPercentage * barWidth;

            barImage.sizeDelta = new Vector2(newWidth, barImage.sizeDelta.y);
        }

        public enum Bar { Health, Fear }
    }
}
