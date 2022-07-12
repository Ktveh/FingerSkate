using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private HandAnimation _handAnimation;

    [SerializeField] private int _multiplierOfChance;
    [SerializeField] private int _chanceMoveRight;
    [SerializeField] private int _chanceMoveLeft;
    [SerializeField] private int _chanceTryStartAnimation;
    [SerializeField] private int _chanceNoAction;
    [SerializeField] private GameObject _target;

    private int _chanceMove;

    private void Start()
    {
        SetChanceMove(0);
    }

    private void FixedUpdate()
    {
        int randomNumber = Random.Range(0, _chanceMove + _chanceTryStartAnimation + _chanceNoAction);

        if (randomNumber >= 0 && randomNumber < _chanceMoveLeft)
            _movement.RotateLeft();
        if (randomNumber >= _chanceMoveLeft && randomNumber < _chanceMove)
            _movement.RotateRight();
        if (randomNumber >= _chanceMove && randomNumber < _chanceNoAction)
            _handAnimation.TryStartAnimation();

        if (transform.position.x < _target.transform.position.x)
            SetChanceMove(1);
        else
            SetChanceMove(-1);
    }

    private void SetChanceMove(int direction)
    {
        _chanceMoveLeft += -direction * _multiplierOfChance;
        _chanceMoveRight += direction * _multiplierOfChance;
        _chanceMove = _chanceMoveLeft + _chanceMoveRight;
    }
}
