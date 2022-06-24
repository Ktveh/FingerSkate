using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] Camera _secondCamera;
    [SerializeField] ParticleSystem _confetti;
    [SerializeField] ParticleSystem _spotlight;
    [SerializeField] FinishObject _finishHand;
    [SerializeField] FinishObject _cup;
    [SerializeField] GameObject[] _players;
    [SerializeField] GameObject[] _handsWithoutSkate;

    private void OnTriggerStay(Collider other)
    {
        Hand hand;

        if (other.gameObject.TryGetComponent<Hand>(out hand))
        {
            SelectCamera();
            SetActiveObjects();
            PlayEffects();
            RotateHand();

            hand.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void PlayEffects()
    {
        _confetti.Play();
        _spotlight.Play();
    }

    private void SetActiveObjects()
    {
        _finishHand.gameObject.SetActive(true);

        foreach (var player in _players)
            player.gameObject.SetActive(false);
        foreach (var handWithoutSkate in _handsWithoutSkate)
            handWithoutSkate.gameObject.SetActive(true);
    }

    private void SelectCamera()
    {
        _mainCamera.gameObject.SetActive(false);
        _secondCamera.gameObject.SetActive(true);
    }
     
    private void RotateHand()
    {
        _finishHand.StartAnimation();
        _cup.StartAnimation();
    }
}
