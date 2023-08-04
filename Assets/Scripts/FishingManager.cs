using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FishingManager : MonoBehaviour
{
    [Tooltip("The fish is spawned under the bait with a random x & z position Random.Range(-spawnPositionRange, spawnPositionRange)")]

    public static FishingManager Instance { get; private set; }
    private GameObject instantiatedFish;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void TrySpawnFish(Bait bait, FishingPool fishingPool)
    {
        if (fishingPool == null || bait == null)
        {
            Debug.Log("Something is null");
            return;
        }
        StartCoroutine(RandomDelay(bait, fishingPool));
    }
    // Coroutine for introducing a random delay before spawning fish
    private IEnumerator RandomDelay(Bait bait, FishingPool fishingPool)
    {
        // Wait for a random duration between 2 and 8 seconds
        float delay = UnityEngine.Random.Range(1f, 3f);
        yield return new WaitForSeconds(delay);
        SpawnFish(bait, fishingPool);
    }
    private void SpawnFish(Bait bait, FishingPool fishingPool)
    {
        // Calculate spawn position
        Vector3 spawnPosition = new Vector3(
            bait.transform.position.x + UnityEngine.Random.Range(-fishingPool.spawnPositionRange, fishingPool.spawnPositionRange),
            fishingPool.poolSurface.transform.position.y - 2f,
            bait.transform.position.z + UnityEngine.Random.Range(-fishingPool.spawnPositionRange, fishingPool.spawnPositionRange)
            );

        Quaternion spawnRotation = Quaternion.LookRotation(bait.transform.position - spawnPosition, Vector3.up);

        // Get the fish prefab from the fishList based on the currentPoolType
        GameObject fishPrefab = GetFish(bait, fishingPool.poolType);

        if (fishPrefab != null && bait.canSpawnFish)
        {
            instantiatedFish = Instantiate(fishPrefab, spawnPosition, spawnRotation);

            if (instantiatedFish != null)
            {
                bait.canSpawnFish = false;
            }
            else
            {
                Debug.LogError("Failed to instantiate fish!");
            }
        }
        else
        {
            // Handle the case where fishPrefab is null (e.g., fish not found in the fishList)
            Debug.Log("Fish prefab not found for currentPoolType: " + fishingPool.poolType);
        }
    }
    private GameObject GetFish(Bait bait, PoolType poolType)
    {
        List<FishData> currentList;
        if (!bait.fishLists.TryGetValue(poolType, out currentList))
        {
            // If the pool type is not found, use the water list as a default
            if (!bait.fishLists.TryGetValue(PoolType.Water, out currentList))
            {
                return null;
            }
        }

        // Using LINQ, calculate the total spawn chance
        float totalSpawnChance = currentList.Sum(item => item.spawnChance);

        float dropRoll = UnityEngine.Random.Range(0f, totalSpawnChance);

        foreach (FishData item in currentList)
        {
            if (dropRoll <= item.spawnChance)
            {
                return item.fishPrefab;
            }
            dropRoll -= item.spawnChance;
        }
        return null;
    }
}
