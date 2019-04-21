using System.Collections;
using System.Collections.Generic;
using LDF.UserInterface.MVC;
using UnityEngine;

namespace LDF.Roglik.UI
{
    public class GameOverController : Controller<GameOverModel, GameOverView>
    {
        private void Awake()
        {
            Initialize();
        }
    }
}