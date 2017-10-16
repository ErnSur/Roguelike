using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStatusInfo : MonoBehaviour {

	PlayerStats stats;
	public Text textDuration;

void Start()
{
	stats = PlayerStats.instance;
}
	// Update is called once per frame
	void Update () {
		if(stats.poisonDuration > 0)
		{
			textDuration.text = stats.poisonDuration.ToString();
		}else
		{
			textDuration.text = null;
		}
	}
}
