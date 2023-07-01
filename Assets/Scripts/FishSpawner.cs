using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private Transform waterTransform;

    public void OnBaitTriggerChange(bool entered, GameObject who)
    {
        if(!who.TryGetComponent(out Bait bait))
        {
            Debug.Log("not bait");
            return;
        }
        if (entered)
        {
            Debug.Log("Fish spawner trying to spawn");
            bait.TrySpawnFish(waterTransform.position);
        }
        else if (!entered)
        {
            Debug.Log("Fish spawner cancelling spawn");
            bait.CancelSpawnFish();
        }
    }
}
