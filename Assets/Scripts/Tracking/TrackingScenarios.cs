using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrackingScenarios", menuName = "ScriptableObject/TrackingScenarios")]
public class TrackingScenarios : ScriptableObject
{
    public TrackingScenario[] ScenariosToTrack;
}
