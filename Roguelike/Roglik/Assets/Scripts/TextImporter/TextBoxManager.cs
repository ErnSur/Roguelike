using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public bool isActive;

    // Use this for initialization
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

        if (Input.GetKeyDown(KeyCode.DownArrow)) { currentLine = Mathf.Clamp(++currentLine,0,endAtLine); }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { currentLine = Mathf.Clamp(--currentLine, 0, endAtLine); }
        if (Input.GetKeyDown(KeyCode.Return)) { DisableTextBox(); }
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
