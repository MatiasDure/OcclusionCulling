using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class RemoveObstacle : MonoBehaviour
{
    [SerializeField] GameObject _obstacle;

    public static event Action OnRemoveObstacle;

    private void Awake()
    {
        FpsTracker.OnDoneGatheringFPS += Reposition;
    }

    private void Start()
    {
        // trigger fps to start tracking
        OnRemoveObstacle?.Invoke();
    }

    private void Reposition()
    { 
        _obstacle.SetActive(false);
        OnRemoveObstacle?.Invoke();
    }

    private void OnDestroy()
    {
        FpsTracker.OnDoneGatheringFPS -= Reposition;
    }
}
