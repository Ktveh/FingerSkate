using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HandAnimation : MonoBehaviour
{
    private const string ShortFly = "ShortFly";
    private const string FlyRotateY = "FlyRotateY";
    private const string SkateRotateY = "SkateRotateY";
    private const string SkateRotateX = "SkateRotateX";
    private const string FlyWithRotate = "FlyWithRotate";

    [SerializeField] private Hand _hand;

    private List<string> _animations;

    private Animator _animator;
    private bool _animationIsPlay;
    private int _currentAnimation;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _animations = new List<string>();
        AddAnimation();
        if (_animations.Count > 0)
            _currentAnimation = 0;
    }

    private void Update()
    {
        if (_animationIsPlay && _hand.OnGround)
        {
            _animationIsPlay = false;
        }
    }

    public bool TryStartAnimation()
    {
        if (_animationIsPlay || _hand.OnGround)
            return false;

        StartAnimation(_animations[_currentAnimation]);
        _currentAnimation++;
        if (_currentAnimation >= _animations.Count)
            _currentAnimation = 0;

        return true;
    }

    public void StartAnimationFall()
    {
        StartAnimation(FlyWithRotate);
    }

    private void AddAnimation()
    {
        _animations.Add(SkateRotateX);
        _animations.Add(ShortFly);
        _animations.Add(FlyRotateY);
        _animations.Add(SkateRotateY);
    }

    private void StartAnimation(string animation)
    {
        _animationIsPlay = true;
        _animator.SetTrigger(animation);
    }
}