using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PooledData", menuName = "ScriptableObject/PooledData")]
public class PooledData : ScriptableObject
{
    public GameObject PooledPrefab;
    public uint InitialPooledAmount;
}
