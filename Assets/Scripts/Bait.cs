using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FishData
{
    public GameObject fishPrefab;
    [Range(0f, 1f)]
    public float spawnChance;
}

[RequireComponent(typeof(Rigidbody))]
public class Bait : MonoBehaviour
{
    [Header("Bait Settings")]
    [SerializeField] private float startForce;

    // References and variables for managing fish spawning
    [SerializeField] private float floatTime;
    private Rigidbody rb;
    public bool canSpawnFish = true;

    [Header("Fish Lists for Different Pool Types")]
    [SerializeField] private List<FishData> water;
    [SerializeField] private List<FishData> lava;
    [SerializeField] private List<FishData> swamp;

    // Dictionary for different pool types and their corresponding fish lists
    public Dictionary<PoolType, List<FishData>> fishLists { get; private set; } = new Dictionary<PoolType, List<FishData>>();
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * startForce);
        fishLists.Add(PoolType.Water, water);
        fishLists.Add(PoolType.Lava, lava);
        fishLists.Add(PoolType.Swamp, swamp);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NoFishDelay());
    }
    public void OnAte(bool entered, GameObject who)
    {
        if (!who.transform.root.gameObject.TryGetComponent<Fish>(out Fish fish))
        {
            Debug.Log("no fish");
            return;
        }
        if (fish.hasAte)
        {
            Debug.Log("fish already ate");
            return;
        }
        fish.hasAte = true;
        fish.StartFloatFish(floatTime);
        // eventually play an animation or something
        Destroy(gameObject);
    }

    // Coroutine for handling the delay when no fish are spawned
    private IEnumerator NoFishDelay()
    {
        yield return new WaitForSeconds(30f);
        Debug.Log("wasnt able to spawn fish so i will die");
        Destroy(gameObject);
    }
}