using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Vector2 _movement; //for 2d movement
    public Rigidbody2D _rb; //for rigid body
    private Animator _animator; //for animator

    private const string _horizontal = "Horizontal"; //Setting parameters as strings
    private const string _vertical = "Vertical";

    private void Awake()
    {
        _animator = GetComponent<Animator>(); //Calling animator and rigidbody
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y); //setting movement every frame

        _rb.velocity = _movement * _moveSpeed; //No delta time required

        _animator.SetFloat(_horizontal, _movement.x); //movement stuff
        _animator.SetFloat(_vertical, _movement.y);
    }

}
