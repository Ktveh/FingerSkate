using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springboard : MonoBehaviour
{
    [SerializeField] private float _boost;

    private void OnTriggerEnter(Collider other)
    {
        Hand hand;

        if (other.gameObject.TryGetComponent<Hand>(out hand))
            hand.Boost(_boost, true);
    }
}
