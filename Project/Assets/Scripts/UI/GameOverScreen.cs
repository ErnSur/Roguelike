using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

#region Singleton & Awake

    public static GameOverScreen instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Too much PlayerStats");
            return;
        }

        instance = this;
    }
    #endregion

	public GameObject canvasUI;
	public Text gameoverText;
	public Shadow gameoverShadow;
	public Text resetText;

	public void ShowScreen()
	{
		canvasUI.SetActive(false);
		gameoverText.enabled = true;
		StartCoroutine("ColorFlicker", Color.red);
	}

	IEnumerator ColorFlicker(Color toColor)
    {
        bool changed = false;
		float t = 0;

        while (!changed)
        {
			t += 0.05f;
            gameoverShadow.effectColor = Color.Lerp(Color.black, toColor, t );

            if (gameoverShadow.effectColor == toColor)
            {
				resetText.enabled = true;
				Color color = resetText.color;
				color.a += 0.1f;
				resetText.color = color;
				if ( color.a > 1 )
				{
                	changed = true;
				}
            }
            yield return null;
        }
    }
	void Start()
	{
		//ShowScreen();
	}

	void OnDisable()
	{
		gameoverText.enabled = false;
		resetText.enabled = false;
	}
}
