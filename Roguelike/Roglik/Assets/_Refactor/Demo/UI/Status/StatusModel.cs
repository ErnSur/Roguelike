using System;
using LDF.Systems;
using UnityEngine;
using LDF.UserInterface.MVC;

namespace LDF.Roglik.UI
{
    public class StatusModel : Model
    {
        [SerializeField]
        public PlayerBodyModel _playerBody;

        public float PlayerHpPercentage => _playerBody.Health.Percentage;
    }
}
