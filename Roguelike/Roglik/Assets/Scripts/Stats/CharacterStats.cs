using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public int maxHealth = 60;
    public int currentHealth { get; private set; }
    public int attackDamage;
    public float visionRange = 6;
    public Vector3 position;



    public void TakeDamage(int damage)
    {
        //damage -= armor.GetValue();

        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //die in some way
        Debug.Log(transform.name + " died.");
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        position = transform.position;
    }
}
