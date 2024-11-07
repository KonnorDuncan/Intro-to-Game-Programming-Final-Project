using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour //manages movement
{
    public static Vector2 Movement; //2d movement

    private PlayerInput _playerInput; //Taking input
    private InputAction _moveAction;

    public void Awake()
    {
        _playerInput = GetComponent<PlayerInput>(); //getting input

        _moveAction = _playerInput.actions["Move"]; //setting inputs
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>(); //vector2 values yippie
    }

}
