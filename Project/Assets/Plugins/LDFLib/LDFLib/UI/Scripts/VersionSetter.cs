using UnityEngine;
using TMPro;

namespace LDF.UserInterface
{
    public class VersionSetter : MonoBehaviour
    {
        public TextMeshProUGUI[] TextObjects;

        [TextArea]
        public string Format;

        //————————————————————————
        // Unity methods
        //————————————————————————
        protected void Awake()
        {
            foreach(var t in TextObjects)
            {
                t.text = string.Format(Format, Application.version);
            }
        }
    }
}