using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RedStone))]
public class CustomStoneEditor : Editor {
	RedStone stoneScript;
	int t = 0;
	//bool[,] cords2d;

	void OnEnable()
	{
		stoneScript = (RedStone)target;
	}

	public override void OnInspectorGUI()
	{
		GUILayout.Label("Player Grid");
		base.OnInspectorGUI();

		if (GUILayout.Button("Increase Grid"))
		{
			IncreaseGrid();
		}
		if (GUILayout.Button("Decrease Grid"))
		{
			DecreaseGrid();
		}
		GUILayout.Space(200);
#region CreateGrid
		bool[,] cords2d = new bool[stoneScript.range,stoneScript.range];
		for (int x = 0; x < cords2d.GetLength(0); x++)
		{
			for (int y = 0; y < cords2d.GetLength(1); y++)
			{
				if(x == 2 && y == 2){ t++;continue;}
				stoneScript.cords[t] = EditorGUI.Toggle(new Rect(100+20*x,275+20*y,20,20),stoneScript.cords[t]);
				t++;
			}
		}
		t=0;
#endregion

	}

	void IncreaseGrid()
	{
		if (stoneScript.range < 9)
			stoneScript.range += 2;
	}
	void DecreaseGrid()
	{
		if (stoneScript.range > 5)
			stoneScript.range -= 2;
	}
}
