using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Items/Stone/empty")]

public class Stone : ScriptableObject {

	public enum StoneType
	{
		RED,
		GREEN,
		BLUE,
		YELLOW
	};
}
