using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats {

    #region Singleton & Awake

    public static PlayerStats instance;
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

    public float percentageHp{ get { return (float)currentHealth / (float)maxHealth; } }

    public Image hpBar;
    float width;

	public override void Die()
	{
		GetComponent<PlayerMovement>().enabled = false;
		GetComponent<PlayerCombat>().enabled = false;
		PlayerTorch.torch = false;

		GameOverScreen.instance.ShowScreen();
	}

    void UpdateHpBar() // add to TakeDamage delegate
    {
        float newWidth = percentageHp * width;
        hpBar.rectTransform.sizeDelta = new Vector2(newWidth, hpBar.rectTransform.sizeDelta.y);
    }

    void Update()
    {
        UpdateHpBar();
    }

    new void Start()
    {
		base.Start();
        width = hpBar.rectTransform.sizeDelta.x;
    }
}
