using Game.Scripts.LiveObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public static Action CamSwitch;
    public static Action CamEnd;

    public static Action<Vector2> DroneTilt;
    public static Action<Vector2> DroneMove;
    public static Action<float> DroneLift;
    public static Action DroneExit;


    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _inputControl = new InputControl();
        PlayerInit();

        Laptop.onHackComplete += CamInit;
        Laptop.onHackEnded += PlayerInit;
        Drone.OnEnterFlightMode += DroneInit;
        Drone.onExitFlightmode += PlayerInit;
    }
    public void PlayerInit()
    {
        _inputControl.Player.Enable();
        _inputControl.Laptop.Disable();
        _inputControl.Drone.Disable();

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
        //PlayerInteractCancel?.Invoke();
    }
    private void PlayerActionPerformed(InputAction.CallbackContext obj)
    {
        PlayerAction?.Invoke();
    }
    private void PlayerActionCanceled(InputAction.CallbackContext obj)
    {
        PlayerActionCancel?.Invoke();
    }
    public void CamInit()
    {
        _inputControl.Player.Disable();
        _inputControl.Laptop.Enable();
        _inputControl.Drone.Disable();

        _inputControl.Laptop.NextCam.performed += NextCamPerformed;
        _inputControl.Laptop.NextCam.canceled += NextCamCanceled;

        _inputControl.Laptop.Escape.performed += CamEscapePerformed;
    }
    private void NextCamPerformed(InputAction.CallbackContext obj)
    {
        CamSwitch?.Invoke();
    }
    private void NextCamCanceled(InputAction.CallbackContext obj)
    {
        PlayerInteractCancel?.Invoke();
    }
    private void CamEscapePerformed(InputAction.CallbackContext obj)
    {
        CamEnd?.Invoke();
    }

    public void DroneInit()
    {
        Debug.Log("drone");
        _inputControl.Player.Disable();
        _inputControl.Laptop.Disable();
        _inputControl.Drone.Enable();

        _inputControl.Drone.Tilt.performed += DroneTiltPerformed;
        _inputControl.Drone.Tilt.canceled += DroneTiltCanceled;

        _inputControl.Drone.Move.performed += DroneMovePerformed;
        _inputControl.Drone.Move.canceled += DroneMoveCanceled;

        _inputControl.Drone.Lift.performed += DroneLiftPerformed;
        _inputControl.Drone.Lift.canceled += DroneLiftCanceled;

        _inputControl.Drone.Exit.performed += DroneExitPerformed;

    }

    private void DroneTiltPerformed(InputAction.CallbackContext obj)
    {

        DroneTilt?.Invoke(_inputControl.Drone.Tilt.ReadValue<Vector2>());
    }
    private void DroneTiltCanceled(InputAction.CallbackContext obj)
    {

        DroneTilt?.Invoke(Vector2.zero);
    }
    private void DroneMovePerformed(InputAction.CallbackContext obj)
    {
        DroneMove?.Invoke(_inputControl.Drone.Move.ReadValue<Vector2>());
    }
    private void DroneMoveCanceled(InputAction.CallbackContext obj)
    {
        DroneMove?.Invoke(Vector2.zero);
    }

    private void DroneLiftPerformed(InputAction.CallbackContext obj)
    {
        DroneLift?.Invoke(_inputControl.Drone.Lift.ReadValue<float>());
    }
    private void DroneLiftCanceled(InputAction.CallbackContext obj)
    {
        DroneLift?.Invoke(0);
    }
    private void DroneExitPerformed(InputAction.CallbackContext obj)
    {
        DroneExit?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
