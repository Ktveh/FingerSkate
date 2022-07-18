using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEffect : MonoBehaviour
{
    [SerializeField] private Hand _hand;
    [SerializeField] private ParticleSystem _fall;
    [SerializeField] private ParticleSystem _leftTrace;
    [SerializeField] private ParticleSystem _righTrace;
    [SerializeField] private ParticleSystem _wind;

    private void OnEnable()
    {
        _hand.Landed += PlayEffectLanded;
        _hand.Landed += StopEffectWind;
        _hand.Boosted += PlayEffectWind;
    }

    private void OnDisable()
    {
        _hand.Landed -= PlayEffectLanded;
        _hand.Landed -= StopEffectWind;
        _hand.Boosted -= PlayEffectWind;
    }

    private void PlayEffectLanded()
    {
        _leftTrace.Play();
        _righTrace.Play();
        _fall.Play();
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
