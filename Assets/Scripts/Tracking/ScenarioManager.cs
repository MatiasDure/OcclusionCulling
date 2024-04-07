using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField] private TrackingScenarios _trackingScenarios;
    private uint _scenarioIndex = 0;
    bool _loadNextScenario = true;

    public static event Action<TrackingScenario> OnScenarioLoaded;

    private void Awake()
    {
        FpsTracker.OnDoneGatheringFPS += LoadScenario;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadScenario();
    }
    private void LoadScenario()
    {
        if (!_loadNextScenario)
        {
            Debug.Log("No more scenarios to load");
            return;
        }

        TrackingScenario upcomingScenario = _trackingScenarios.ScenariosToTrack[_scenarioIndex];

        Debug.Log("-------------------------------------------");
        Debug.Log("Loading the following scenario:\n"+upcomingScenario.ToString());

        OnScenarioLoaded?.Invoke(upcomingScenario);

        _scenarioIndex++;

        _loadNextScenario = _scenarioIndex >= _trackingScenarios.ScenariosToTrack.Length;
    }
    private void OnDestroy()
    {
        FpsTracker.OnDoneGatheringFPS -= LoadScenario;
    }
}
