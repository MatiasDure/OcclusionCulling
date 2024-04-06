using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraPosition : MonoBehaviour
{
    [SerializeField] private Vector3 _positionLod0;
    [SerializeField] private Vector3 _positionLod1;
    [SerializeField] private Vector3 _positionLod2;
    [SerializeField] private Vector3 _positionLod3;
    [SerializeField] private Vector3 _occlusion;

    private Dictionary<TrackingScenarioType, Vector3> _lodPositions;
    // Start is called before the first frame update

    private void Awake()
    {   
        _lodPositions = new Dictionary<TrackingScenarioType, Vector3>()
        {
            { TrackingScenarioType.LOD0Monkey, _positionLod0 },
            { TrackingScenarioType.LOD1Monkey, _positionLod1 },
            { TrackingScenarioType.LOD2Monkey, _positionLod2 },
            { TrackingScenarioType.LOD3Monkey, _positionLod3 },
            { TrackingScenarioType.Occlusion, _occlusion },
        };

        ScenarioManager.OnScenarioLoaded += UpdatePosition;
    }

    private void OnDestroy()
    {
        ScenarioManager.OnScenarioLoaded -= UpdatePosition;
    }

    private void UpdatePosition(TrackingScenario pScenario) => transform.position = _lodPositions[pScenario.Type];
}
