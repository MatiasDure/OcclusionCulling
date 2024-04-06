using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public SpawnData SpawnerData;
    public static event Action<uint> OnActivatePrefabs;
    public static event Action<uint> OnDeactivatePrefabs;

    public void ActivatePrefabs()
    {
        OnActivatePrefabs?.Invoke(SpawnerData.AmountToSpawn);
        SpawnerData.AmountToSpawn = 0;
    }

    public void DeactivatePrefabs()
    {
        OnDeactivatePrefabs?.Invoke(SpawnerData.AmountToDelete);
        SpawnerData.AmountToDelete = 0;
    }
}
