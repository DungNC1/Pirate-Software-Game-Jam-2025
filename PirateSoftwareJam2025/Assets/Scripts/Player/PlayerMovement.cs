using UnityEngine;
using System.Collections;

public sealed class PlayerMovement : MonoBehaviour
{

    [SerializeField] PlayerStats m_playerStats;
    public float speed;
    public bool isSlowed = false; 
    private Vector2 m_MovementVector = Vector2.zero;
    Rigidbody2D m_Rigidbody2D;
    [SerializeField] private float slowDuration = 2f;
    [SerializeField] private float slowFactor = 0.5f;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        speed = m_playerStats.Speed;
    }


    void FixedUpdate()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        m_MovementVector = PlayerInputHandler.Instance.GetMoveInput;
        m_Rigidbody2D.velocity = m_MovementVector * speed;
    }

    public IEnumerator ApplySlowEffect(PlayerShooting playerShooting)
    {
        isSlowed = true;

        float originalSpeed =   speed;
        float originalShootCooldown = playerShooting.shootCooldown;

        speed *= slowFactor;
        playerShooting.shootCooldown /= slowFactor;

        yield return new WaitForSeconds(slowDuration);

        speed = originalSpeed;
        playerShooting.shootCooldown = originalShootCooldown;

        isSlowed = false;
    }
}
