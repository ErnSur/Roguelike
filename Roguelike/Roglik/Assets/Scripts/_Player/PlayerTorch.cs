using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorch : MonoBehaviour {

	public static bool torch = true;
	public float torchOffSpeed = 17;
    public float MaxFlickerTime = 0.05f;
    public float minRange;
    public float maxRange;
    private float flickerTime = 0;

	void Update () {

		if (Input.GetButtonDown("Torch"))
		{
			torch = !torch;
		}

		if(torch)
		{
			if (flickerTime >= MaxFlickerTime)
			{
				float scale = Random.Range(minRange, maxRange);
				transform.localScale = new Vector3 (scale, scale,0);
				flickerTime = 0;
			}

			flickerTime += Time.deltaTime;
		}else{
			transform.localScale = Vector3.Lerp (transform.localScale, Vector3.zero, Time.deltaTime * torchOffSpeed);
		}
	}

	public void ToggleTorch()
	{
		torch = !torch;
	}
	void OnEnable()
	{
		torch = true;
	}
}
