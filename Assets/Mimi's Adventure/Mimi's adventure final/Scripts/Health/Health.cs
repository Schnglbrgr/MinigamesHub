using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField]private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField]private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriterend;
    private readonly IEnumerable<Behaviour> components;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriterend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        //making sure health does not go above max.Health or under 0
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if(currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            //player dead
            if(!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
  private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            //we wish for the player character to turn back to the original color in between vulnerability phase 
            spriterend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriterend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        //invulnerability duration
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    public void Respawn ()
    {
        dead = false;//the player is not dead anymore and is revived
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idel");
        StartCoroutine(Invulnerability());//at this point I realised I wrote this wrong everywhere and I have to keep using it wrong "idle" would be correct

        //activating all attached components
        foreach (Behaviour component in components)
            component.enabled = true;
    }
}

