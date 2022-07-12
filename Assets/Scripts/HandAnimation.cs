using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class HandAnimation : MonoBehaviour
{
    [SerializeField] private float _minVelocityForAnimation;

    private const string ShortFly = "ShortFly";
    private const string FlyRotateY = "FlyRotateY";
    private const string SkateRotateY = "SkateRotateY";
    private const string SkateRotateX = "SkateRotateX";
    private const string FlyWithRotate = "FlyWithRotate";

    private List<string> _animations;

    private Animator _animator;
    private Rigidbody _rigibody;
    private bool _animationIsPlay;
    private int _currentAnimation;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigibody = GetComponent<Rigidbody>();

        _animations = new List<string>();
        AddAnimation();
        if (_animations.Count > 0)
            _currentAnimation = 0;
    }

    private void Update()
    {
        if (_animationIsPlay && _rigibody.velocity.y > _minVelocityForAnimation)
            _animationIsPlay = false;
    }

    public bool TryStartAnimation()
    {
        if (_animationIsPlay)
            return false;

        StartAnimation(_animations[_currentAnimation]);
        _currentAnimation++;
        if (_currentAnimation >= _animations.Count - 1)
            _currentAnimation = 0;

        return true;
    }

    public void StartAnimationFall()
    {
        StartAnimation(FlyWithRotate);
    }

    private void AddAnimation()
    {
        _animations.Add(ShortFly);
        _animations.Add(FlyRotateY);
        _animations.Add(SkateRotateY);
        _animations.Add(SkateRotateX);
    }

    private void StartAnimation(string animation)
    {
        _animationIsPlay = true;
        _animator.SetTrigger(animation);
    }
}