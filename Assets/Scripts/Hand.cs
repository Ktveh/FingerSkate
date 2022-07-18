using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HandAnimation))]
public class Hand : MonoBehaviour
{
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private int _boostOnGroud;
    [SerializeField] private int _levelingSpeed;
    
    private bool _onGround = true;
    private bool _onJump = false;

    private float _lastPositionY;

    private HandAnimation _handAnimation;

    public bool OnGround => _onGround;

    public event UnityAction Boosted;
    public event UnityAction Fallen;
    public event UnityAction Jumped;
    public event UnityAction Landed;

    private void Start()
    {
        _handAnimation = GetComponent<HandAnimation>();  
    }

    private void FixedUpdate()
    {
        CheckHeight();
        if (!_onGround || _onJump)
            LevelingRotate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Hand>())
        {
            Fall();
        }

        if (collision.gameObject.TryGetComponent<Track>(out Track track))
        {
            if (track.IsLower)
            {
                transform.position += Vector3.up * Time.deltaTime;
            }
            else
            {
                if (!_onGround)
                {
                    _onGround = true;
                    _onJump = false;
                    Landed?.Invoke();
                    Boost(_boostOnGroud, false);
                }
            }
        }
        else
        {
            if (_lastPositionY < transform.position.y)
            {
                _onGround = false;
                _onJump = true;
                Jumped?.Invoke();
            }
        }

        if (collision.gameObject.GetComponent<Springboard>())
        {
            _onJump = true;
            _onGround = false;
        }
    }

    public void Boost(float boost, bool startJump)
    {
        _movement.Boost(boost, startJump);
        Boosted?.Invoke();
        if (startJump)
        {
            _onGround = false;
            _onJump = true;
            Jumped?.Invoke();
        }
    }

    private void Fall()
    {
        if (!_onGround)
        {
            _movement.ResetSpeed();
            _handAnimation.StartAnimationFall();
        }
    }

    private void CheckHeight()
    {
        _lastPositionY = transform.position.y;
        if (_lastPositionY > transform.position.y && _onJump)
        {
            _onJump = false;
            Fallen?.Invoke();
        }
    }

    private void LevelingRotate()
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
}

