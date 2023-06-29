using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab;

    private Dictionary<GameObject, bool> baitSpawnAvailability = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, Vector3> fishSpawnPositions = new Dictionary<GameObject, Vector3>();
    private List<GameObject> fishPool = new List<GameObject>();

    private void Update()
    {
        // this is probably really inefficient
        for (int i = fishSpawnPositions.Count - 1; i >= 0; i--)
        {
            KeyValuePair<GameObject, Vector3> kvp = fishSpawnPositions.ElementAt(i);
            GameObject obj = kvp.Key;
            Vector3 spawnPosition = kvp.Value;

            if (obj.transform.position.y < spawnPosition.y)
            {
                fishSpawnPositions.Remove(obj);
                obj.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bait"))
        {
            if (!baitSpawnAvailability.ContainsKey(other.gameObject))
            {
                baitSpawnAvailability.Add(other.gameObject, true);
                StartCoroutine(SpawnFishWithDelay(other.gameObject));
            }
            else if (baitSpawnAvailability[other.gameObject])
            {
                StartCoroutine(SpawnFishWithDelay(other.gameObject));
            }
        }
    }

    private IEnumerator SpawnFishWithDelay(GameObject bait)
    {
        baitSpawnAvailability[bait.gameObject] = false;

        // Wait for a random duration between 2 and 8 seconds
        float delay = Random.Range(2f, 8f);
        yield return new WaitForSeconds(delay);

        // Calculate spawn position
        Vector3 spawnPosition = new Vector3(bait.transform.position.x + Random.Range(-5f, 5f), transform.position.y - 5, bait.transform.position.z + Random.Range(-5f, 5f));
        Quaternion spawnRotation = Quaternion.LookRotation(bait.transform.position - spawnPosition, Vector3.up);

        // Spawn fish // CURRENTLY NOT WORKING. FISH ARE NEVER REUSED
        GameObject fishObject;
        if (fishPool.Count > 0)
        {
            fishObject = GetFirstInactiveObject(fishPool);
            if(fishObject != null)
            {
            fishObject.SetActive(true);
            fishObject.transform.position = spawnPosition;
            fishObject.transform.rotation = spawnRotation;
            }
        }
        else
        {
            fishObject = Instantiate(fishPrefab, spawnPosition, spawnRotation);
        }

        fishSpawnPositions.Add(fishObject, spawnPosition);
    }
    public GameObject GetFirstInactiveObject(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            if (!obj.activeSelf)
            {
                return obj;
            }
        }

        return null; // If no inactive object is found
    }
}
