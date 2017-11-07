using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;

 public class Log : MonoBehaviour {

	 public GUISkin UISkin;
     public static int maxLines = 6;
     public static Queue<string> queue = new Queue<string>();
     public static string Mytext = "";

    public static void Write(string activity)
	{
        if (queue.Count >= maxLines)
        	queue.Dequeue();

           queue.Enqueue(activity);

        Mytext = "";

        foreach (string st in queue)
        	Mytext = Mytext + st + "\n";
    }

    void OnGUI() //text is cut off becausse size is not scaling
	{

      GUI.Label(new Rect(10,                             // x, left offset
                   (Screen.height - 150),            // y, bottom offset
                   300f,                                // width
                   150f), Mytext,UISkin.label);    // height, text, Skin features /  150f), Mytext,GUI.skin.textArea);

    }

	void Start()
	{
		queue.Clear();
		Mytext = "";
		Write("\"Floor is wet and I hear someones footsteps.\"");
	}
 }