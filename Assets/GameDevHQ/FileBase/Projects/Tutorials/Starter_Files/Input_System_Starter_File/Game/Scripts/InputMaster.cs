using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMaster : MonoBehaviour
{

    private static InputMaster _instance;
    public static InputMaster Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("input Manager");
                go.AddComponent<InputMaster>();
            }
            return _instance;
        }
    }

    private InputControl _inputControl;

    public static Action<Vector3> PlayerMove;
    public static Action PlayerInteract;
    public static Action PlayerInteractCancel;

    public static Action PlayerAction;
    public static Action PlayerActionCancel;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _inputControl = new InputControl();
        PlayerInit();
    }
    public void PlayerInit()
    {
        _inputControl.Player.Enable();
        _inputControl.Player.Move.performed += PlayerMovePerformed;
        _inputControl.Player.Move.canceled += PlayerMoveCanceled;
        _inputControl.Player.Interact.performed += PlayerInteractPerformed;
        _inputControl.Player.Interact.canceled += PlayerInteractCanceled;
        _inputControl.Player.Action2.performed += PlayerActionPerformed;
        _inputControl.Player.Action2.canceled += PlayerActionCanceled;

    }
    private void PlayerMovePerformed(InputAction.CallbackContext obj)
    {
        PlayerMove?.Invoke(_inputControl.Player.Move.ReadValue<Vector2>());
    }
    private void PlayerMoveCanceled(InputAction.CallbackContext obj)
    {
        PlayerMove?.Invoke(Vector3.zero);
    }
    private void PlayerInteractPerformed(InputAction.CallbackContext obj)
    {
        PlayerInteract?.Invoke();
    }
    private void PlayerInteractCanceled(InputAction.CallbackContext obj)
    {
        PlayerInteractCancel?.Invoke();
    }
    private void PlayerActionPerformed(InputAction.CallbackContext obj)
    {
        PlayerAction?.Invoke();
    }
    private void PlayerActionCanceled(InputAction.CallbackContext obj)
    {
        PlayerActionCancel?.Invoke();
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
