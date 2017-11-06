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

	[Header("Fear")]
	public int currentFear;
	public int maxFear = 100;

	public float percentageHp{ get { return (float)currentHealth / (float)maxHealth; } }
	public float percentageFear{ get { return (float)currentFear / (float)maxFear; } }

	[Header("HP and Fear UI bars")]
    public Image hpBar;
    float hpBarWidth;

	public Image fearBar;
	float fearBarWidth;


	public override void Die()
	{
		GetComponent<PlayerMovement>().enabled = false;
		GetComponent<PlayerCombat>().enabled = false;
		PlayerTorch.torch = false;

		if (poisonDuration > 0)
			TurnSystem.nextTurn -= TakePoisonDamage;

		GameOverScreen.instance.ShowScreen();
	}

    void UpdateHpBar() // add to TakeDamage delegate
    {
        float newWidth = percentageHp * hpBarWidth;
        hpBar.rectTransform.sizeDelta = new Vector2(newWidth, hpBar.rectTransform.sizeDelta.y);
    }

    void UpdateFearBar() // add to TakeDamage delegate
    {
        float newWidth = percentageFear * fearBarWidth;
        fearBar.rectTransform.sizeDelta = new Vector2(newWidth, fearBar.rectTransform.sizeDelta.y);
    }

    void Update()
    {
        UpdateHpBar();
        UpdateFearBar();
    }

    new void Start()
    {
		base.Start();
		TurnSystem.nextTurn += FearSystem.IncreaseFearInDark;
		currentFear = 0;
        hpBarWidth = hpBar.rectTransform.sizeDelta.x;
        fearBarWidth = fearBar.rectTransform.sizeDelta.x;
    }

	void OnDisable()
	{
		TurnSystem.nextTurn -= FearSystem.IncreaseFearInDark;
	}
}
