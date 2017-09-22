using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour {

    private float flickerTime = 0;

    public float MaxFlickerTime = 0.05f;
    public float minRange;
    public float maxRange;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (flickerTime >= MaxFlickerTime)
        {
            float scale = Random.Range(minRange, maxRange);
            transform.localScale = new Vector3 (scale, scale,0);
            flickerTime = 0;
        }

        flickerTime += Time.deltaTime;

	}
}
