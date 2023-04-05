using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private TextMeshProUGUI lifeText;
    private int life = 3;
    public Transform startPoint;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("water") || collision.gameObject.CompareTag("fireball"))
        {
            deathSound.Play();
            life--;
            lifeText.text = "Health: " + life + "/3";
            Die();
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
    }

    private void RestartLevel()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        if(life > 0)
            rb.transform.position = new Vector3(startPoint.position.x, startPoint.position.y, 0.0f);
        else
        {
            gameOverSound.Play();
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }
            
    }
}
