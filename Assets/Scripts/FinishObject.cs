using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishObject : MonoBehaviour
{
    private const float SpeedAnimation = 0.7f;
    private const float Distance = 1f;
    private const int AngleX = -15;
    private const int AngleY = -25;
    private const int AngleZ = 0;

    public void StartAnimation()
    {
        Vector3 angle = transform.rotation.eulerAngles;
        transform.DOMoveY(transform.position.y + Distance, SpeedAnimation);
        transform.DORotate(new Vector3(angle.x + AngleX, angle.y + AngleY, angle.z + AngleZ), SpeedAnimation);
    }
}
