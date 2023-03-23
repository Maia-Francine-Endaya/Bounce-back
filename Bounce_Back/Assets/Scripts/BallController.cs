using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
  Rigidbody rb;
  [SerializeField] float movementSpeed = 6f;

  public int maxHealth = 3;
  public int currentHealth;

  public HealthBar health;
  
    void Start()
    {
      rb = GetComponent<Rigidbody>();
      currentHealth = maxHealth;
      health.SetMaxHealth(maxHealth);
        
    }
    void Update()
    {
      float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

      if (currentHealth == 0) {
        SceneManager.LoadScene(2);
      }
    }

    void TakeDamage(int damage)
      {
        currentHealth -= damage;
        health.SetHealth(currentHealth);
      }

    void OnTriggerEnter(Collider collision)
    {
      Debug.Log("On Collision Enter");
      if(collision.gameObject.tag == "Out_of_Bounds")
      {
        Debug.Log("Damage");
        TakeDamage(1);
      }
    }
}
