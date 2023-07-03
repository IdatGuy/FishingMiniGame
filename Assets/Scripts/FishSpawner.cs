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
            return;
        }
        if (entered)
        {
            bait.TrySpawnFish(waterTransform.position);
        }
        else if (!entered)
        {
            bait.CancelSpawnFish();
        }
    }
}
