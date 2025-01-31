using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phlegm : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private bool isPhlegnnon = true;
    [SerializeField] private float slowDuration = 2f;
    [SerializeField] private float slowFactor = 0.5f;

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
                if (!playerMovement.isSlowed && isPhlegnnon)
                {
                    StartCoroutine(ApplySlowEffect(playerMovement, playerShooting));
                }
            }
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        Destroy(gameObject);
    }

    private IEnumerator ApplySlowEffect(PlayerMovement playerMovement, PlayerShooting playerShooting)
    {
        playerMovement.isSlowed = true;

        float originalSpeed = playerMovement.speed;
        float originalShootCooldown = playerShooting.shootCooldown;

        playerMovement.speed *= slowFactor;
        playerShooting.shootCooldown /= slowFactor;

        yield return new WaitForSeconds(slowDuration);

        playerMovement.speed = originalSpeed;
        playerShooting.shootCooldown = originalShootCooldown;

        playerMovement.isSlowed = false;
    }
}
