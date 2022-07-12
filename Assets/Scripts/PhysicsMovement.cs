using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigibody;
    [SerializeField] private SurfaceSlider _surfaceSlider;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private int _angle;
    [SerializeField] private float _minVelocityForJump;
    [SerializeField] private int _boostOnGroud;
    [SerializeField] private int _jumpForce;
    [SerializeField] private int _levelingSpeed;

    public event UnityAction Boosted;
    public event UnityAction Fallen;
    public event UnityAction Jumped;
    public event UnityAction Landed;

    private bool _onGround = true;
    private bool _onJump = false;

    private float _speed;
    private float _lastPositionY;

    private void Update()
    {
        CheckState();
        _lastPositionY = transform.position.y;
    }

    private void FixedUpdate()
    {
        Move(transform.forward);
        SpeedLeveling();
        if (!_onGround || _onJump)
            RotateLeveling();
    }

    public void Boost(float boost, bool startJump = true)
    {
        _speed += boost;
        if (startJump)
        {
            _onJump = true;
            Jumped?.Invoke();
            _rigibody.AddForce(Vector3.up * _jumpForce);
        }
        if (_speed >= _maxSpeed)
        {
            Boosted?.Invoke();
        }
    }

    public bool TryFall()
    {
        if ((!_onGround || _onJump) && _speed > _maxSpeed)
        {
            _speed = 0;
            _onGround = true;
            _onJump = false;
            Fallen?.Invoke();
            return true;
        }
        return false;
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

    private void CheckState()
    {
        if (_rigibody.velocity.y < _minVelocityForJump)
        {
            _onGround = false;
            
            if (_lastPositionY > transform.position.y && _onJump)
            {
                _onJump = false;
                Fallen?.Invoke();
            }
        }
        if (_rigibody.velocity.y > _minVelocityForJump && !_onGround)
        {
            _onGround = true;
            Boost(_boostOnGroud, false);
            Landed?.Invoke();
        }
    }

    private void RotateLeveling()
    {
        if (transform.rotation.x < 0)
            transform.Rotate(_levelingSpeed * Time.deltaTime, 0, 0);
        if (transform.rotation.x > 0)
            transform.Rotate(-_levelingSpeed * Time.deltaTime, 0, 0);
        if (transform.rotation.z < 0)
            transform.Rotate(0, 0, _levelingSpeed * Time.deltaTime);
        if (transform.rotation.z > 0)
            transform.Rotate(0, 0, -_levelingSpeed * Time.deltaTime);      
    }

    private void SpeedLeveling()
    {
        if (_speed < _maxSpeed - 1)
            _speed += _acceleration * Time.deltaTime;
        if (_speed > _maxSpeed + 1)
            _speed -= _deceleration * Time.deltaTime;
    }
}
