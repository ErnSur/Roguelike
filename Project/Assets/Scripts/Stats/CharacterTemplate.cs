using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterTemplate : ScriptableObject {

#region /// Basic Statistics ///
	[Header("Basic Statistics")]
    public int maxHealth = 60;
    public int currentHealth;
    public float visionRange = 6;
    public int attackDamage = 2;
#endregion

#region /// Weapon & Hit Effects ///
	[Header("Weapon & Hit Effects")]
	public Weapon weapon;
	public List<Stone> stones;
#endregion

#region /// Status Effects ///
	[Header("Status Effects")]
	public int poisonDuration = 0;
	public int poisonDamage = 5;
	public int fireDuration = 0;
	public int fireDamage = 15;
	public int paralyzeDuration;
#endregion

#region /// Audio & Visuals ///
	[Header("Audio & Visuals")]
    public AudioClip[] hurtSound;
    private SpriteRenderer sprite;
	public Transform corpses;
#endregion

}
