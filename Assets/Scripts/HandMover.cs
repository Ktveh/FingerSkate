using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMover : MonoBehaviour
{
    [SerializeField] private int _minSpeed;
    [SerializeField] private int _angle;

    private Coroutine _setHeightJob;
    private float _jumpForce;
    private float _speed;

    private void Start()
    {
        _jumpForce = 0;
        _speed = 0;
    }

    private void Update()
    {
        if (_jumpForce > 0)
            _jumpForce -= 2 * Time.deltaTime;
        if (_speed < _minSpeed)
            _speed += 5 * Time.deltaTime;

        Move();
    }

    public void Move()
    {
        transform.Translate(0, _jumpForce * Time.deltaTime, _speed * Time.deltaTime);
    }

    public void RotateLeft()
    {
        Rotate(-_angle);
    }

    public void RotateRight()
    {
        Rotate(_angle);
    }

    private void Rotate(float angle)
    {
        transform.Rotate(0, angle * Time.deltaTime, 0);
    }

    public void Jump(float jumpForce)
    {
        _jumpForce = jumpForce;
    }
}
