using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private HandAnimation _handAnimation;

    [SerializeField] private int _chanceOfMove;
    [SerializeField] private int _chanceOfStartAnimation;
    [SerializeField] private int _chanceOfIdle;
    [SerializeField] private Transform _target;

    private void FixedUpdate()
    {
        int randomNumber = Random.Range(0, _chanceOfMove + _chanceOfStartAnimation + _chanceOfIdle);

        if (randomNumber >= 0 && randomNumber < _chanceOfMove)
            Move();
        if (randomNumber >= _chanceOfMove && randomNumber < _chanceOfMove + _chanceOfStartAnimation)
            _handAnimation.TryStartAnimation();   
    }

    private void Move()
    {
        if (transform.position.x < _target.position.x)
            _movement.RotateRight();
        else
            _movement.RotateLeft();

        _chanceOfMove++;
    }
}
