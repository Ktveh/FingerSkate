using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class Follow : MonoBehaviour
{
    [SerializeField] private Hand _target;
    [SerializeField] private float _speedMoveNear;
    [SerializeField] private float _speedMoveFar;
    [SerializeField] private float _minYOffset;
    [SerializeField] private float _maxYOffset;
    [SerializeField] private float _minZDamping;
    [SerializeField] private float _maxZDamping;
    [SerializeField] private float _minYDamping;
    [SerializeField] private float _maxYDamping;

    private Coroutine _setDistanceJob;
    private CinemachineVirtualCamera _vCamera;
    private bool _onNear = false;
    private bool _onFar = true;

    private void Start()
    {
        _vCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        _target.Jumped += MoveNear;
        _target.Fallen += MoveFar;
        _target.Landed += MoveFar;
    }

    private void OnDisable()
    {
        _target.Jumped -= MoveNear;
        _target.Fallen -= MoveFar;
        _target.Landed -= MoveFar;
    }

    private void MoveNear()
    {
        if (_onFar)
            StartSetDistance(_minYOffset, _minYDamping, _minZDamping,_speedMoveNear);
    }

    private void MoveFar()
    {
        if (_onNear)
            StartSetDistance(_maxYOffset, _maxYDamping, _maxZDamping, _speedMoveFar);
    }

    private void StartSetDistance(float yOffset, float yDamping, float zDamping, float speed)
    {
        if (_setDistanceJob != null)
            StopCoroutine(_setDistanceJob);
        _setDistanceJob = StartCoroutine(SetDistance(yOffset, yDamping, zDamping, speed));
        _onFar = !_onFar;
        _onNear = !_onNear;
    }

    private IEnumerator SetDistance(float yOffset, float yDamping, float zDamping, float speed)
    {
        var transposer = _vCamera.GetCinemachineComponent<CinemachineTransposer>();
        while (transposer.m_FollowOffset.y != yOffset || transposer.m_ZDamping != zDamping || transposer.m_YDamping != yDamping)
        {
            _vCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = Mathf.Lerp(transposer.m_FollowOffset.y, yOffset, speed * Time.deltaTime);
            _vCamera.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = Mathf.Lerp(transposer.m_ZDamping, zDamping, speed * Time.deltaTime);
            _vCamera.GetCinemachineComponent<CinemachineTransposer>().m_YDamping = Mathf.Lerp(transposer.m_YDamping, yDamping, speed * Time.deltaTime);
            yield return null;
        }
    }
}
