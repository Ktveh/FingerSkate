using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private bool _isLower;

    public bool IsLower => _isLower;
}
