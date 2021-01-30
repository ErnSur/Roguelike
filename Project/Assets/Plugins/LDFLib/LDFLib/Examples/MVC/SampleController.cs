#if UNITY_EDITOR
using System.Collections;
using UnityEngine;
using LDF.Utility;

namespace LDF.UserInterface.MVC.Examples
{
    public class SampleController : Controller<SampleModel, SampleView>
    {
        public BuildScene scene;

        private void Awake()
        {
            Initialize();
            ShowView();
        }

        IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);

            SceneBlender.ChangeScene(scene);
        }
    }
}
#endif