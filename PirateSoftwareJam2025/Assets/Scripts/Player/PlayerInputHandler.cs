using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Instance;
    PlayerInputAction playerInputAction;


    Vector2 m_MoveInput = Vector2.zero;
    public Vector2 GetMoveInput { get { return m_MoveInput; } }

    [SerializeField] bool m_MinionInput = false;
    public bool GetMinionInput { get { return m_MinionInput; } }

    public void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        Instance = this;
    }

    private void OnEnable()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();
        BindInputs();
    }

    private void OnDisable()
    {
        UnbindInputs();
        playerInputAction.Disable();
    }

    void BindInputs()
    {
        playerInputAction.Player.Move.performed += SetMove;
        playerInputAction.Player.Move.canceled += SetMove;

        playerInputAction.Player.CreateMinion.performed += SetMinionCreation;
        playerInputAction.Player.CreateMinion.canceled += SetMinionCreation;
    }

    void UnbindInputs()
    {
        playerInputAction.Player.Move.performed -= SetMove;
        playerInputAction.Player.Move.canceled -= SetMove;

        playerInputAction.Player.CreateMinion.performed -= SetMinionCreation;
        playerInputAction.Player.CreateMinion.canceled -= SetMinionCreation;
    }

    private void SetMove(InputAction.CallbackContext context)
    {
        m_MoveInput = context.ReadValue<Vector2>();
    }

    private void SetMinionCreation(InputAction.CallbackContext context)
    {
        if (context.performed)
            m_MinionInput = true;
        else
            m_MinionInput = false;
    }
}
