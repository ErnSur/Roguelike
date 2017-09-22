using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    


    // Update is called once per frame
    void Update ()
	{
		KeyMovement ();
	}

	void KeyMovement ()
	{
		Vector3 newPos;

		switch (Input.inputString) {

		case "w":
			newPos = new Vector3 (this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
			this.transform.position = newPos;
			break;
		case "s":
			newPos = new Vector3 (this.transform.position.x, this.transform.position.y - 1, this.transform.position.z);
			this.transform.position = newPos;
			break;
		case "a":
			newPos = new Vector3 (this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);
			this.transform.position = newPos;
			break;
		case "d":
			newPos = new Vector3 (this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);
			this.transform.position = newPos;
			break;
		}
	}
}

