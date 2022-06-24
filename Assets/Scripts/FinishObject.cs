using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishObject : MonoBehaviour
{
    public void StartAnimation()
    {
        Vector3 angle = transform.rotation.eulerAngles;
        transform.DOMoveY(transform.position.y + 1f, 0.7f);
        transform.DORotate(new Vector3(angle.x - 15, angle.y - 25, angle.z), 0.7f);
    }
}
