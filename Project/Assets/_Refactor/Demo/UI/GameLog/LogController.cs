﻿using LDF.UserInterface.MVC;

namespace LDF.Roglik.UI
{
    public class LogController : Controller<LogView>
    {
        private void Awake()
        {
            Initialize();
            ShowView();
        }
    }
}