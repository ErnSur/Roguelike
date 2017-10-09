using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour {

    public int maxHealth = 60;
    public int currentHealth { get; private set; }
    public int attackDamage;
    public float visionRange = 6;
    public Vector3 position;

    private SpriteRenderer sprite;

    public void TakeDamage(int damage)
    {
        //damage -= armor.GetValue();

        currentHealth -= damage;
        StartCoroutine("ColorFlicker");
        //change of colors


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //die in some way
        Debug.Log(transform.name + " died.");
    }

    IEnumerator ColorFlicker()
    {
        bool changed = false;
        float t = 0;

        while (!changed)
        {
            t += 0.1f;
            //sprite.color = Color.Lerp(Color.white, Color.red, (ElapsedTime / TotalTime));
            sprite.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(t,1)); //Mathf.PingPong(Time.time * 1f, 1.0f)

            if (sprite.color == Color.white)
            {
                changed = true;
                Debug.Log("changed");
            }
            yield return null;
        }
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        position = transform.position;
        sprite = GetComponent<SpriteRenderer>();
    }
}
