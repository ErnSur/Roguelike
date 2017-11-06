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
		for (int y = 0; y < cords2d.GetLength(1); y++)
		{
			for (int x = 0; x < cords2d.GetLength(0); x++)
			{
				if(x == stoneScript.playerOnMiniGrid.x && y == stoneScript.playerOnMiniGrid.y){ t++;continue;}
				stoneScript.cords[t] = EditorGUI.Toggle(new Rect(100+20*x,375-20*y,20,20),stoneScript.cords[t]);
				//stoneScript.cords[t] = GUI.Toggle(new Rect(100+35*x,375-35*y,40,40),stoneScript.cords[t],t.ToString());
				t++;
			}
		}
		t=0;
#endregion

	}

	void IncreaseGrid()
	{
		if (stoneScript.range < 9)
			stoneScript.range += 1;
	}
	void DecreaseGrid()
	{
		if (stoneScript.range > 5)
			stoneScript.range -= 1;
	}
}
