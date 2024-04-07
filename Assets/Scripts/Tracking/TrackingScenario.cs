using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrackingScenario", menuName = "ScriptableObject/TrackingScenario")]
public class TrackingScenario : ScriptableObject
{
    public TrackingScenarioType Type;
    public uint MonkeysInScene;
    public float TrackingDuration; // in seconds
    public string ScenarioDescription;

    public override string ToString()
    {
        return string.Format("Scenario: {0}\n\tMonkeys in scene: {1}\n\tDuration: {2}\n\tDescription: {3}", Type.ToString(), MonkeysInScene, TrackingDuration, ScenarioDescription);
    }
}

public enum TrackingScenarioType
{
    LOD0Monkey,
    LOD1Monkey,
    LOD2Monkey,
    LOD3Monkey,
    Occlusion
}
