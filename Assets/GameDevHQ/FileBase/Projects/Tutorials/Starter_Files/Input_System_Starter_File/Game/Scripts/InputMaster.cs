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
    public static Action<bool> PlayerInteract;
    public static Action<bool> PlayerAction;

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
        _inputControl.Player.Interact.performed += Interact_performed;
        _inputControl.Player.Interact.canceled += Interact_canceled;
        _inputControl.Player.Action2.performed += Action2_performed;
        _inputControl.Player.Action2.canceled += Action2_canceled;

    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        PlayerInteract?.Invoke(true);
    }
    private void Interact_canceled(InputAction.CallbackContext obj)
    {
        PlayerInteract?.Invoke(false);
    }
    private void Action2_performed(InputAction.CallbackContext obj)
    {
        PlayerAction?.Invoke(true);
    }
    private void Action2_canceled(InputAction.CallbackContext obj)
    {
        PlayerAction?.Invoke(false);


        // Update is called once per frame
        void Update()
    {
        
    }
}
