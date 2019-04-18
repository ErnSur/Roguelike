using LDF.UserInterface.MVC;

namespace LDF.Roglik.UI
{
    public class StatusController : Controller<StatusModel,StatusView>
    {
        private void Awake()
        {
            Initialize();
            ShowView();
        }

        private void OnEnable()
        {
            Model.OnPlayerDamaged += UpdatePlayerHP;
        }

        private void OnDisable()
        {
            Model.OnPlayerDamaged -= UpdatePlayerHP;
        }

        private void UpdatePlayerHP()
        {
            View.UpdateStatusBar(StatusView.Bar.Health, Model.PlayerHpPercentage);
        }
    }
}
