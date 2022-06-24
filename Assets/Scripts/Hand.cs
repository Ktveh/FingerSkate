using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HandAnimation))]
public class Hand : MonoBehaviour
{
    [SerializeField] private PhysicsMovement _movement;

    private HandAnimation _handAnimation;

    private void Start()
    {
        _handAnimation = GetComponent<HandAnimation>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Hand>())
            Fall();

        if (collision.gameObject.GetComponent<Track>())
            transform.position += Vector3.up;
    }

    public void AddBoost(float boost)
    {
        _movement.Boost(boost);
    }

    public void Fall()
    {
        if (_movement.TryFall())
            _handAnimation.StartAnimationFall();
    }
}

