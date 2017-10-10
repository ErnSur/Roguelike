using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats {

    #region Singleton & Awake
    public static PlayerStats instance;
    new void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Too much PlayerStats");
            return;
        }

        instance = this;
        base.Awake();
    }
    #endregion

    public float percentageHp //1 = 100%
    {
        get { return (float)currentHealth / (float)maxHealth; }
    }

    public Image hpBar;
    float width;

    void UpdateHpBar() // add to TakeDamage delegate
    {
        float newWidth = percentageHp * width;
        hpBar.rectTransform.sizeDelta = new Vector2(newWidth, hpBar.rectTransform.sizeDelta.y);
    }

    private void Update()
    {
        UpdateHpBar();
    }

    private void Start()
    {
        width = hpBar.rectTransform.sizeDelta.x;
    }
}
