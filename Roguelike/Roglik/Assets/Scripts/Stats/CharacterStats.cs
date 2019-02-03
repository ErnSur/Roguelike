using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterStats : MonoBehaviour
{
    public CharacterTemplate myTemplate;

    [Header("Basic Statistics")]
    public int maxHealth = 60;

    public int currentHealth;
    public float visionRange = 6;
    public int attackDamage = 2;

    public int AttackDamage =>
        weapon == null ? attackDamage : Random.Range(weapon.minDamage, weapon.maxDamage) + attackDamage;

    [Header("Weapon & Hit Effects")]
    public Weapon weapon;

    public List<Stone> stones;

    private Vector3 position;
    public Vector3 previousPosition;

    public Vector3 Position
    {
        get => position;
        set
        {
            previousPosition = position;
            position = value;
            previousNode = node;
            previousNode.Walkable = true;
            node = PFgrid.grid[(int) position.x, (int) position.y];
            node.Walkable = false;
        }
    }

    public PFNode node;
    PFNode previousNode;

    [Header("Status Effects")]
    public int poisonDuration = 0;

    public int poisonDamage = 5;
    public int fireDuration = 0;
    public int fireDamage = 15;
    public int paralyzeDuration;

    [HideInInspector]
    public AudioSource audioSrc;

    [HideInInspector]
    public SpriteRenderer sprite;

    private AudioClip[] hurtSound;


    public void Start()
    {
        // set stats from template
        maxHealth = myTemplate.maxHealth;
        currentHealth = myTemplate.currentHealth;
        if (currentHealth == 0)
        {
            currentHealth = maxHealth;
        }

        visionRange = myTemplate.visionRange;
        attackDamage = myTemplate.attackDamage;

        weapon = myTemplate.weapon;
        stones = myTemplate.stones;

        hurtSound = myTemplate.hurtSound;

        //initialize gameobject
        position = transform.position;
        previousPosition = transform.position;
        node = PFgrid.grid[(int) position.x, (int) position.y];
        node.Walkable = false;
        sprite = GetComponent<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        TurnSystem.nextTurn += OnNextTurn;
    }

    void OnDisable()
    {
        TurnSystem.nextTurn -= OnNextTurn;
    }

    public virtual void Die() //Player object cannot be destroyed, light and audio systems are attached
    {
        Log.Write(transform.name + " died.");
        node.Walkable = true;

        Instantiate(myTemplate.corpses, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        StartCoroutine("ColorFlicker", Color.green);
    }


    public void DealDamage(CharacterStats enemy) //effect eff
    {
        int blow = AttackDamage;

        enemy.TakeDamage(blow);
        Log.Write(this.name + " attacked For: " + blow);

        if (weapon != null && weapon.onHitEffect != null)
        {
            weapon.onHitEffect.OnHitEffect(enemy);
        }
    }

    public void TakeDamage(int damage) // (int damage, upgrade effect)
    {
        //effect.act();

        audioSrc.clip =
            hurtSound
                [Random.Range(0, hurtSound.Length)]; //maybe do it on corutine or dont destroy object, just change its sprite and disable states
        audioSrc.Play();

        currentHealth -= damage;
        StartCoroutine("ColorFlicker", Color.red);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void OnNextTurn()
    {
        if (poisonDuration > 0)
        {
            TakeDamage(poisonDamage);
            //Debug.Log(this.name + " took:" + poisonDamage + " poison damage.");
            poisonDuration--;
        }

        if (fireDuration > 0)
        {
            TakeDamage(fireDamage);
            //Debug.Log(this.name + " took:" + poisonDamage + " poison damage.");
            fireDuration--;
        }
    }

    IEnumerator ColorFlicker(Color toColor)
    {
        bool changed = false;
        float t = 0;

        while (!changed)
        {
            t += 0.2f;
            //sprite.color = Color.Lerp(Color.white, Color.red, (ElapsedTime / TotalTime));
            sprite.color =
                Color.Lerp(Color.white, toColor, Mathf.PingPong(t, 1)); //Mathf.PingPong(Time.time * 1f, 1.0f)

            if (sprite.color == Color.white)
            {
                changed = true;
            }

            yield return null;
        }
    }
}