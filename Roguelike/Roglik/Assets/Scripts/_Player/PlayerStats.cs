using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public int maxHp;
    public int currentHp;
    public float percentageHp //1 = 100%
    {
        get { return (float)currentHp / (float)maxHp; }
    }

    public float actionSpeed;
    public int dmg;

    public Image hpBar;
    float width;

    void UpdateHpStatus() // add to dmged delegate
    {
        float newWidth = percentageHp * width;
        hpBar.rectTransform.sizeDelta = new Vector2(newWidth, hpBar.rectTransform.sizeDelta.y);
    }

    private void Update()
    {
        UpdateHpStatus();
    }

    private void Start()
    {
        width = hpBar.rectTransform.sizeDelta.x;
    }
}
