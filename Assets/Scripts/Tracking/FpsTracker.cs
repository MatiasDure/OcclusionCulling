using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsTracker : MonoBehaviour
{
    [SerializeField] private float _updateInterval = 5f;
    [SerializeField] private bool _tracking = false;
    [SerializeField] private TextMeshProUGUI _fpsText;
    [SerializeField] private TriangleCounter _triangleCounter;
    [SerializeField] private bool _hasTestScenario;
    private float _accum = 0f; 
    private uint _frames = 0;
    private float _timeLeft;
    private uint _secondsPassedSinceTracking = 0;

    public static event Action OnDoneGatheringFPS;

    // Would be better to make this a singleton to avoid so many subscriptions
    private void Awake()
    {
        // This is to avoid polluting data when updating/instantiating objects
        PooledObject.OnStartUpdatingPoolingObjects += StopTracking;
        PooledObject.OnStopUpdatingPoolingObjects += StartTracking;

        ScenarioManager.OnScenarioLoaded += SetUpTracker;
        FollowWaypoints.OnLastWaypoint += StopTracking;
        //CameraReposition.OnReposition += StartTracking;

        if (_tracking) ResetTrackingData();
    }

    private void Update()
    {
        if (!_tracking) return;

        _timeLeft -= Time.deltaTime;
        _accum += Time.deltaTime;
        _frames++;
        _triangleCounter.Track(_timeLeft);


        if (_timeLeft <= 0)
        {
            _secondsPassedSinceTracking++;

            float fps = 1f / (_accum / _frames);
            //_fpsText.text += fps + "\n";
            FileHandler.WriteToFile("Fps: " + fps);
            FileHandler.WriteToFile("Time: " + _secondsPassedSinceTracking);
            //Debug.Log("FPS: " + fps);
            //Debug.Log("-------------------------------------------");
            if (_hasTestScenario)
            {
                StopTracking();
                OnDoneGatheringFPS?.Invoke();
            }
            else ResetTrackingData();
        }
    }

    private void StopTracking()
    {
        _tracking = false;
    }

    private void StartTracking()
    {
        _tracking = true;
        ResetTrackingData();
    }

    private void SetUpTracker(TrackingScenario pScenario)
    {
        _updateInterval = pScenario.TrackingDuration;
        StartTracking();
    }

    private void ResetTrackingData()
    {
        _timeLeft = _updateInterval;
        _accum = 0;
        _frames = 0;
    }

    private void OnDestroy()
    {
        PooledObject.OnStartUpdatingPoolingObjects -= StopTracking;
        PooledObject.OnStopUpdatingPoolingObjects -= StartTracking;

        ScenarioManager.OnScenarioLoaded -= SetUpTracker;
        RemoveObstacle.OnRemoveObstacle -= StartTracking;
    }
}
