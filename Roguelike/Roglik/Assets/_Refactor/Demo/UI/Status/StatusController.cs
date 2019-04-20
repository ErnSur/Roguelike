using LDF.Systems;
using LDF.UserInterface.MVC;
using UnityEngine;

namespace LDF.Roglik.UI
{
    public class StatusController : Controller<StatusModel,StatusView>
    {
        [SerializeField]
        private LivingBodySystem _playerBodySystem;

        private void Awake()
        {
            Initialize();
            ShowView();
        }

        private void OnEnable()
        {
            _playerBodySystem.OnDamageTaken += UpdatePlayerHP;
            _playerBodySystem.OnHeal += UpdatePlayerHP;
        }

        private void OnDisable()
        {
            _playerBodySystem.OnDamageTaken -= UpdatePlayerHP;
            _playerBodySystem.OnHeal -= UpdatePlayerHP;
        }

        private void UpdatePlayerHP(float _)
        {
            View.UpdateStatusBar(StatusView.Bar.Health, Model.PlayerHpPercentage);
        }
    }
}
