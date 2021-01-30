using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisoner001 : MonoBehaviour {

    public TextBoxManager textBox;
    public TextAsset[] myText;
    public int myAnswer;

    public Item holdItem;

    public Item poison;
    public Item cure;


    public void UpdateState()
    {
        if (holdItem == poison)
        {
            myAnswer = 2;
            UpdateText();
        }
        if (holdItem == cure)
        {
            myAnswer = 1;
            UpdateText();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        UpdateText();
    }
	void OnTriggerExit2D(Collider2D other)
	{
		textBox.DisableTextBox();
	}

    void UpdateText()
    {
        textBox.EnableTextBox();
        textBox.textFile = myText[myAnswer];
        textBox.UpdateTextData();
    }
}
