using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField] private PooledData _pooledData;
    [SerializeField] private GameObject _parentObject;

    private List<GameObject> _deactivatedOjects = new List<GameObject>();
    private List<GameObject> _activatedObjects = new List<GameObject>();

    public static event Action OnStartUpdatingPoolingObjects;
    public static event Action OnStopUpdatingPoolingObjects;

    private void Awake()
    {
        //UIEvents.OnActivatePrefabs += ActivateObjects;
        //UIEvents.OnDeactivatePrefabs += DeactivateObjects;
        ScenarioManager.OnScenarioLoaded += LoadObjects;
        PoolObjects();
    }

    private void LoadObjects(TrackingScenario pScenario)
    {
        ActivateObjects(pScenario.MonkeysInScene);
    }
    public void ActivateObjects(uint pAmountToActivate) {
        OnStartUpdatingPoolingObjects?.Invoke();

        if(_activatedObjects.Count >= pAmountToActivate)
        {
            OnStopUpdatingPoolingObjects?.Invoke();
            return;
        }

        pAmountToActivate -= (uint) _activatedObjects.Count;

        for (int i = _deactivatedOjects.Count - 1; i >= 0; i--)
        {
            if (pAmountToActivate == 0)
            {
                OnStopUpdatingPoolingObjects?.Invoke();
                return;
            }

            GameObject objectToActive = _deactivatedOjects[i];
            objectToActive.SetActive(true);
            _deactivatedOjects.RemoveAt(i);
            _activatedObjects.Add(objectToActive);

            pAmountToActivate--;
        }

        // instatiate more if no objects to activate
        for (uint i = pAmountToActivate; i > 0; i--)
        {
            InstantiatePooledObject(true);
        }

        OnStopUpdatingPoolingObjects?.Invoke();
    }
    public void DeactivateObjects(uint pAmountToDeactivate)
    {
        OnStartUpdatingPoolingObjects?.Invoke();

        for (int i = _activatedObjects.Count - 1; i >= 0; i--)
        {
            if (pAmountToDeactivate == 0) break;

            GameObject objectToDeactivate = _activatedObjects[i];
            objectToDeactivate.SetActive(false);
            _activatedObjects.RemoveAt(i);
            _deactivatedOjects.Add(objectToDeactivate);

            pAmountToDeactivate--;
        }

        OnStopUpdatingPoolingObjects?.Invoke();
    }
    private void PoolObjects()
    {
        for (int i = 0; i < _pooledData.InitialPooledAmount; i++)
        {
            GameObject initialPooledObject = InstantiatePooledObject(false);
            _deactivatedOjects.Add(initialPooledObject);
        }
    }
    private GameObject InstantiatePooledObject(bool pIsActive)
    {
        GameObject currentInstatiation = Instantiate(_pooledData.PooledPrefab);

        if (_parentObject)
        {
            currentInstatiation.transform.localPosition = Vector3.zero;
            currentInstatiation.transform.parent = _parentObject.transform;
        }
        else currentInstatiation.transform.position = Vector3.zero;

        currentInstatiation.SetActive(pIsActive);

        return currentInstatiation;
    }

    private void OnDestroy()
    {
        ScenarioManager.OnScenarioLoaded -= LoadObjects;
    }
}
