using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum PoolType { Water, Lava, Swamp };
public class FishingPool : MonoBehaviour
{
    public PoolType poolType;
    public GameObject poolSurface;
    public float spawnPositionRange;
    private FishingManager fishingManager;
    private void Awake()
    {
        fishingManager = FindObjectOfType<FishingManager>();
    }
    public void OnBaitTriggerChange(bool entered, GameObject who)
    {
        if (!who.TryGetComponent(out Bait bait))
        {
            Debug.Log("no biat");
            return;
        }
        if (entered)
        {
            Debug.Log("tried to spawn fish");
            bait.canSpawnFish = true;
            fishingManager.TrySpawnFish(bait, this);
        }
        else if (!entered)
        {
            Debug.Log("canceled fish");
            bait.canSpawnFish = false;
        }
    }
}
