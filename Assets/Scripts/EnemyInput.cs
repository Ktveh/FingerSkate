using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private HandAnimation _handAnimation;

    [SerializeField] private int _chanceMoveRight;
    [SerializeField] private int _chanceMoveLeft;
    [SerializeField] private int _chanceTryStartAnimation;

    private int _chanceMove;

    private void Start()
    {
        _chanceMove = _chanceMoveLeft + _chanceMoveRight;
    }

    private void Update()
    {
        int randomNumber = Random.Range(0, _chanceMove + _chanceTryStartAnimation);

        if (randomNumber >= 0 && randomNumber < _chanceMoveLeft)
            _movement.RotateLeft();
        if (randomNumber >= _chanceMoveLeft && randomNumber < _chanceMove)
            _movement.RotateRight();
        if (randomNumber >= _chanceMove)
            _handAnimation.TryStartAnimation();
    }
}
