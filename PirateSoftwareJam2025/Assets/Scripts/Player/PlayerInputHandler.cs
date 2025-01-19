using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public sealed class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Instance;
    PlayerInputAction playerInputAction;


    Vector2 m_MoveInput = Vector2.zero;
    public Vector2 GetMoveInput { get { return m_MoveInput; } }

    bool m_MinionInput = false;
    public bool GetMinionInput { get { return m_MinionInput; } }

    [SerializeField] float m_AmmoChangeInput = 0f;
    public float GetAmmoChangeInput { get { return m_AmmoChangeInput; } }
    UnityEvent<int> ScrollUp;
    public UnityEvent<int> GetScrollUpEvent { get { return ScrollUp; } }
    UnityEvent<int> ScrollDown;
    public UnityEvent<int> GetScrollDownEvent { get { return ScrollDown; } }

    public void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        Instance = this;

        if (ScrollUp == null)
            ScrollUp = new UnityEvent<int>();
        if (ScrollDown == null)
            ScrollDown = new UnityEvent<int>();
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

        playerInputAction.Player.AmmoChange.performed += SetAmmoChange;
        playerInputAction.Player.AmmoChange.canceled += SetAmmoChange;
    }

    void UnbindInputs()
    {
        playerInputAction.Player.Move.performed -= SetMove;
        playerInputAction.Player.Move.canceled -= SetMove;

        playerInputAction.Player.CreateMinion.performed -= SetMinionCreation;
        playerInputAction.Player.CreateMinion.canceled -= SetMinionCreation;

        playerInputAction.Player.AmmoChange.performed -= SetAmmoChange;
        playerInputAction.Player.AmmoChange.canceled -= SetAmmoChange;
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

    private void SetAmmoChange(InputAction.CallbackContext context)
    {
        m_AmmoChangeInput = context.ReadValue<float>() / 120;

        if (m_AmmoChangeInput == 0)
            return;

        if (m_AmmoChangeInput < 0.5f)
            ScrollUp.Invoke(1);
        else if(m_AmmoChangeInput > 0.5f)
            ScrollDown.Invoke(-1);
    }
}
