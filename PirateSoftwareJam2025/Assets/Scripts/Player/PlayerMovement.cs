using UnityEngine;

public sealed class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerStats m_playerStats;
    private Vector2 m_MovementVector = Vector2.zero;
    Rigidbody2D m_Rigidbody2D;
    // Update is called once per frame

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        m_MovementVector = PlayerInputHandler.Instance.GetMoveInput;
        m_Rigidbody2D.velocity = m_MovementVector * m_playerStats.Speed;
    }
}
