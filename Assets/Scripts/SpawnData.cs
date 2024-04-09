using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "ScriptableObject/SpawnData")]
public class SpawnData : ScriptableObject
{
    public uint AmountToSpawn = 0;
    public uint AmountToDelete = 0;
}
