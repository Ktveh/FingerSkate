using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HandMover), typeof(Hand))]
public class HandInput : MonoBehaviour
{
    private HandMover _mover;
    private Hand _hand;

    private void Start()
    {
        _mover = GetComponent<HandMover>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _mover.Move();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _mover.RotateLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _mover.RotateRight();
        }
    }
}
