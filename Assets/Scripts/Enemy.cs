using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] AudioSource EnemyDeath;
    [SerializeField] AudioSource EnemyHurt;
    private float currentHealth;
    [SerializeField] private Animator anim;
    [SerializeField] FloatingHealthbar healthBar;
    [SerializeField] float pointsIncreasePerSecond = 10f;
    [SerializeField] GameObject Blood;

    [SerializeField] private Rigidbody rb;
    float mag;
    Vector3 lastPosition;
    float speed;
    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthbar>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthbar(currentHealth, maxHealth);
        Debug.Log(currentHealth.ToString());
        //anim.SetTrigger("hurt");
        if(currentHealth <= 0)
        {
            Instantiate(Blood, transform.position, Quaternion.identity);
            EnemyDeath.Play();
            anim.SetTrigger("Death");
            Invoke("Die", 3);
        }
        else
        {
            EnemyHurt.Play();

        }

    }

    private void Update()
    {
        if(currentHealth < 100f)
        {
            currentHealth = currentHealth + (pointsIncreasePerSecond * Time.deltaTime);
            healthBar.UpdateHealthbar(currentHealth, maxHealth);
        }

        speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;
        mag = speed / 3.5f;
        anim.SetFloat("Blend", mag);
    }
    private void Die()
    {
        //EnemyDeath.Play();
        /*Instantiate(Blood, transform.position, Quaternion.identity);
        anim.SetTrigger("Death");*/
        Destroy(gameObject);
        GetComponent <CapsuleCollider>().enabled = false;
        //healthBar.enabled = false;
        transform.Find("Canvas").gameObject.SetActive(false);
        Destroy(transform.Find("Warrior").gameObject);
        this.enabled = false;

    }

}
