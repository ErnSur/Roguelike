using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceFeatures : MonoBehaviour {

	public bool torch;
	public float fadeTime = 2;
	public float MaxfadeTime = 0.8f;
    public float MaxFlickerTime = 0.08f;
    public float minRange = 7.9f;
    public float maxRange = 8.5f;
    private float flickerTime = 0;
	public float torchOffSpeed = 6;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		if(torch)
		{
			if(fadeTime < MaxfadeTime)
			{
				transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(minRange,minRange,0), Time.deltaTime * torchOffSpeed); //light fade in
				fadeTime += Time.deltaTime;
			}
			else if (flickerTime >= MaxFlickerTime)
			{
				float scale = Random.Range(minRange, maxRange);
				transform.localScale = new Vector3 (scale, scale,0);
				flickerTime = 0;
			}
			flickerTime += Time.deltaTime;

		}else
		{
			fadeTime = 0;
			transform.localScale = Vector3.Lerp (transform.localScale, Vector3.zero, Time.deltaTime * torchOffSpeed); //light fade off
		}
	}
}
