using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private HandAnimation _handAnimation;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            _movement.RotateLeft();
        if (Input.GetKey(KeyCode.RightArrow))
            _movement.RotateRight();
        if (Input.GetKeyDown(KeyCode.UpArrow))
            _handAnimation.TryStartAnimation();
    }
}
