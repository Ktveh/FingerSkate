using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEffect : MonoBehaviour
{
    [SerializeField] private PhysicsMovement _movement;
    [SerializeField] private ParticleSystem _fall;
    [SerializeField] private ParticleSystem _leftTrace;
    [SerializeField] private ParticleSystem _righTrace;
    [SerializeField] private ParticleSystem _wind;

    private void OnEnable()
    {
        _movement.Landed += PlayEffectLanded;
        _movement.Landed += StopEffectWind;
        _movement.Boosted += PlayEffectWind;
    }

    private void OnDisable()
    {
        _movement.Landed -= PlayEffectLanded;
        _movement.Landed -= StopEffectWind;
        _movement.Boosted -= PlayEffectWind;
    }

    private void PlayEffectLanded()
    {
        _fall.Play();
        _leftTrace.Play();
        _righTrace.Play();
    }

    private void PlayEffectWind()
    {
        _wind.Play();
    }

    private void StopEffectWind()
    {
        _wind.Stop();
    }
}
