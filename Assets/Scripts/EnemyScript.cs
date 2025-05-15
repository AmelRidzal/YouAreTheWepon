using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;
    [SerializeField] FloatingHealthBar healthBar;
    public float health, maxHealth;
    private Animator animator;
    public bool isStanding = false;

    public SpriteRenderer spriteRenderer;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Ensure AudioSource is attached
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealth(maxHealth, maxHealth);

        if (isStanding)
        {
            animator.SetBool("Standing", true);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            if (collision.gameObject.name == "bullet(Clone)" || collision.gameObject.name == "boneBullet(Clone)")
            {
                TakeDmg(20);
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(AttackAnimation());
        }
    }

    IEnumerator AttackAnimation()
    {
        animator.SetBool("Attack", true);
        PlayAttackSound();
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Attack", false);
    }

    IEnumerator EnemyFlash(bool what)
    {
        if (what)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.green;
        }
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = Color.white;
    }

    public void TakeDmg(float damage)
    {
        health -= damage;
        healthBar.UpdateHealth(health, maxHealth);
        StartCoroutine(EnemyFlash(true));

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        PlayDeathSound();
        Destroy(gameObject, 0.5f); // Allow sound to finish before destroying
    }

    private void PlayAttackSound()
    {
        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }

    private void PlayDeathSound()
    {
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }
}