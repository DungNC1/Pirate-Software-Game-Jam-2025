using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phlegm : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
                PlayerShooting playerShooting = collision.gameObject.GetComponentInChildren<PlayerShooting>();
                if (!playerMovement.isSlowed)
                {
                    StartCoroutine(playerMovement.ApplySlowEffect(playerShooting));
                }
            }
        }

        Destroy(gameObject);
    }

}
