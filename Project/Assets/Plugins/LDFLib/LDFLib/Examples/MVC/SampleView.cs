#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

namespace LDF.UserInterface.MVC.Examples
{
    public class SampleView : View<SampleModel>
    {
        [SerializeField]
        private RectTransform _listTransform;

        protected override void OnHide()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnShow()
        {
            foreach (var spotname in Model.ModelData)
            {
                var text = new GameObject().AddComponent<Text>();
                text.text = spotname;
                text.transform.SetParent(_listTransform);
            }
        }
    }
}
#endif