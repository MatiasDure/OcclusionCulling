using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraReposition : MonoBehaviour
{
    [SerializeField] private Vector3 _newPosition;
    [SerializeField] private Vector3 _newRotation;

    private bool _reposition = false;

    public static event Action OnReposition;

    private void Awake()
    {
        FpsTracker.OnDoneGatheringFPS += Reposition;
    }

    private void Start()
    {
        OnReposition?.Invoke();
    }

    private void Reposition()
    {
        if (_reposition) return;

        transform.position = _newPosition;
        transform.rotation = Quaternion.Euler(_newRotation);
        _reposition = true;

        OnReposition?.Invoke();
    }

    private void OnDestroy()
    {
        FpsTracker.OnDoneGatheringFPS -= Reposition;
    }
}
