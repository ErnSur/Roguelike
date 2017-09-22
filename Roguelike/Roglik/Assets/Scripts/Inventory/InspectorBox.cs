using UnityEngine;
using UnityEngine.UI;


public class InspectorBox : MonoBehaviour {

    public Text itemName;
    public Text itemDescription;
    public Image itemImage;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            gameObject.SetActive(false);
        }
    }

}
