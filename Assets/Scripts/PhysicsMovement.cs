using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigibody;
    [SerializeField] private SurfaceSlider _surfaceSlider;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _decelerationMaxSpeed;
    [SerializeField] private int _angle;
    [SerializeField] private int _jumpForce;

    private float _speed;

    private void FixedUpdate()
    {
        Move(transform.forward);
        SpeedLeveling();
    }

    public void Boost(float boost, bool startJump)
    {
        _speed += boost;
        if (startJump)
            _rigibody.AddForce(Vector3.up * _jumpForce);
    }

    public void ResetSpeed()
    {
        _speed = 0;
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

    private void Move(Vector3 direction)
    {
        Vector3 directionAlongSurface = _surfaceSlider.Project(direction.normalized);
        Vector3 offset = directionAlongSurface * (_speed * Time.deltaTime);

        _rigibody.MovePosition(_rigibody.position + offset);
    }

    private void SpeedLeveling()
    {
        if (_speed < _maxSpeed - 1)
            _speed += _acceleration * Time.deltaTime;
        if (_speed > _maxSpeed + 1)
            _speed -= _deceleration * Time.deltaTime;
        _maxSpeed -= _decelerationMaxSpeed * Time.deltaTime;
    }
}
