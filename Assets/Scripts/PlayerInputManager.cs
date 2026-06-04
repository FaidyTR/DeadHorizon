using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private PlayerInputActions playerInputActions;
    public event EventHandler OnJump;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }
    private void Start()
    {
        playerInputActions.OnFoot.Enable();
        playerInputActions.OnFoot.Jump.performed += ctx => Jump();
    }

    public void Jump()
    {
        OnJump?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetInputVector2()
    {
        Vector2 inputVector2 = playerInputActions.OnFoot.Movement.ReadValue<Vector2>();
        return inputVector2;
    }
    public Vector2 GetInputLookDirection()
    {
        Vector2 InputLookDirection = playerInputActions.OnFoot.Look.ReadValue<Vector2>();
        return InputLookDirection;
    }


}
