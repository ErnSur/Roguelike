using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorch : MonoBehaviour {

	public static bool torch = false;
	public float fadeTime = 0;
	public float MaxfadeTime = 2;
	public float torchOffSpeed = 17;
    public float MaxFlickerTime = 0.05f;
    public float minRange;
    public float maxRange;
    private float flickerTime = 0;

	private AudioSource audioSourceTorch;
	private AudioSource audioSourceFlint;
	public AudioClip[] torchSounds; // 0-turn on, 1-on, 2-turn off
	[Range(0,1)]public float torchVolume;

	void Update () {

		if (Input.GetButtonDown("Torch"))
		{
			torch = !torch;
		}

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

			audioSourceTorch.volume = Mathf.Lerp(audioSourceTorch.volume, torchVolume, Time.deltaTime * torchOffSpeed); //sound fade in

		}else
		{
			fadeTime = 0;
			transform.localScale = Vector3.Lerp (transform.localScale, Vector3.zero, Time.deltaTime * torchOffSpeed); //light fade off
			audioSourceTorch.volume = Mathf.Lerp(audioSourceTorch.volume, 0, Time.deltaTime * torchOffSpeed); //sound fade off
		}
	}

	void Awake()
	{
		audioSourceTorch = GetComponents<AudioSource>()[0];
        audioSourceTorch.clip = torchSounds[1];
		audioSourceFlint = GetComponents<AudioSource>()[1];
		audioSourceFlint.clip = torchSounds[0];
	}

	public void ToggleTorch()
	{
		torch = !torch;
		if(torch)
		{
			audioSourceFlint.Play();
		}else
		{
			FearSystem.IncreaseFear(5);
		}
	}

	void OnEnable()
	{
		torch = true;
    	audioSourceTorch.Play();
	}
}