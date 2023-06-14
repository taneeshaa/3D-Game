using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Codice.CM.Common.CmCallContext;

public class PlayerDeath : MonoBehaviour
{
    private PlayerHealth PlayerHealth;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource hurtSoundEffect;


    private void Start()
    {
        PlayerHealth = GetComponent<PlayerHealth>();
    }

    private void TakeDamage(int damage)
    {

            PlayerHealth.currentHealth -= damage;
            PlayerHealth.healthbar.SetHealth(PlayerHealth.currentHealth);
        if (PlayerHealth.currentHealth <= 0)
        {
            deathSoundEffect.Play();
            Die();
            Invoke("RestartLevel", 3f);
        }
        else
        {
            hurtSoundEffect.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(30);
            //Die();

        }

        else if (collision.gameObject.CompareTag("DeathTrap"))
        {

            Die();
        }
    }

    private void Die()
    {
        //deathSoundEffect.Play();
        anim.SetTrigger("death");
        //RestartLevel();

    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}