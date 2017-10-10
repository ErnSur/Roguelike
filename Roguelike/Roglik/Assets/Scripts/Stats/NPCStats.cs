using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCStats : CharacterStats {

    public List<PFnode> myPath = new List<PFnode>();
    public Vector3 position;

    private void Start()
    {
        position = transform.position;
    }
}
