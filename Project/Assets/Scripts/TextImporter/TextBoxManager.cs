using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;
	public GameObject upArrow;
	public GameObject downArrow;

    public Text theText;

    [HideInInspector]public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public bool isActive;



    public void Start()
    {
        UpdateTextData();
    }

    public void UpdateTextData()
    {
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
            currentLine = 0;
            endAtLine = textLines.Length - 1;
        }

        switch (isActive)
        {
            case true: EnableTextBox(); break;
            case false: DisableTextBox(); break;
        }
    }

    void Update()
    {
        if (!isActive) { return; }
        theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.DownArrow)) { NextTextLine(); }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { PreviousTextLine(); }
        if (Input.GetButtonDown("Cancel")) { DisableTextBox(); }

		if (currentLine != 0 )
		{
			upArrow.SetActive(true);
		}else { upArrow.SetActive(false); }

		if( endAtLine > currentLine )
		{
			downArrow.SetActive(true);
		}else { downArrow.SetActive(false); }
    }

	public void NextTextLine()
	{
		currentLine = Mathf.Clamp(++currentLine,0,endAtLine);
	}
	public void PreviousTextLine()
	{
		currentLine = Mathf.Clamp(--currentLine, 0, endAtLine);
	}

    public void EnableTextBox()
    {
        isActive = true;
        textBox.SetActive(true);
    }

    public void DisableTextBox()
    {
        isActive = false;
        textBox.SetActive(false);
    }
}
