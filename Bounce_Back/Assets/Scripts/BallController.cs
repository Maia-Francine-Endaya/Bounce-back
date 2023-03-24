using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
  Rigidbody rb;
  // [SerializeField] float movementSpeed = 6f;

  public int maxHealth = 3;
  public int currentHealth;

  public HealthBar health;

  bool shoot;
  public float shootPower = 10f;

  Vector3 lastpos;
  Vector3 shootDirection;
  
    void Start()
    {
      rb = GetComponent<Rigidbody>();
      lastpos = this.transform.position;

      currentHealth = maxHealth;
      health.SetMaxHealth(maxHealth);
        
    }
    void Update()
    {
      // float horizontalInput = Input.GetAxis("Horizontal");
      // float verticalInput = Input.GetAxis("Vertical");

      // rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

      if (Input.GetKeyDown(KeyCode.Space))
      {
        shootDirection = transform.forward + new Vector3(0, 0.25f, 0);
        shoot = true;
      }

      if (currentHealth == 0) {
        SceneManager.LoadScene(2);
      }
    }

    void FixedUpdate()
    {
      float moveHorizontal = Input.GetAxis("Horizontal");
      float moveVertical = Input.GetAxis("Vertical"); 

      Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

      rb.AddForce(movement * 10f);
      if (shoot)
      {
        rb.AddForce(shootDirection * shootPower, ForceMode.Impulse);
        shoot = false;
      }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            Debug.Log("You've hit a bouncy thing!");
            // rb.AddForce(-Vector3.forward * 10f, ForceMode.Impulse);
            rb.AddForce(collision.contacts[0].normal * 10f, ForceMode.Impulse);
        }

        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("You've hit a wall!");
            // rb.AddForce(-Vector3.forward * 5f, ForceMode.Impulse);
            rb.AddForce(collision.contacts[0].normal * 5f, ForceMode.Impulse);
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
