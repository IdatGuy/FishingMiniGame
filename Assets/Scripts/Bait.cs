using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bait : MonoBehaviour
{
    [SerializeField] private float startForce;
    [SerializeField] private float spawnPositionRange;
    [SerializeField] private GameObject fishPrefab;

    private GameObject instantiatedFish;
    private Vector3 currentWaterPosition;
    private Rigidbody rb;
    private bool canSpawnFish = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NoFishDelay());
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.AddForce(transform.forward * startForce);
    }
    public void OnAte(bool entered, GameObject who)
    {
        if(instantiatedFish == who)
        {
            // eventually play an animation or something
            Destroy(gameObject);    
        }
    }
    public void TrySpawnFish(Vector3 waterPosition)
    {
        if (waterPosition == null)
        {
            Debug.Log("Water position is null");
            return;
        }

        currentWaterPosition = waterPosition;
        StartCoroutine(RandomDelay());
    }
    public void CancelSpawnFish()
    {
        canSpawnFish = false;
    }
    private void SpawnFish()
    {
        // make sure we can still spawn the fish
        if(canSpawnFish == false)
        {
            Debug.Log("This bait cant spawn fish");
            return;
        }

        // Calculate spawn position
        Vector3 spawnPosition = new Vector3(
            transform.position.x + Random.Range(-spawnPositionRange, spawnPositionRange), 
            currentWaterPosition.y - 2f, 
            transform.position.z + Random.Range(-spawnPositionRange, spawnPositionRange)
            );

        Quaternion spawnRotation = Quaternion.LookRotation(transform.position - spawnPosition, Vector3.up);

        // if fish is instantiated correctly make sure it cant spawn fish again
        instantiatedFish = Instantiate(fishPrefab, spawnPosition, spawnRotation);
        if (instantiatedFish != null)
        {
            canSpawnFish = false;
        }
    }
    private IEnumerator RandomDelay()
    {
        // Wait for a random duration between 2 and 8 seconds
        float delay = Random.Range(2f, 8f);
        yield return new WaitForSeconds(delay);
        SpawnFish();
    }
    private IEnumerator NoFishDelay()
    {
        yield return new WaitForSeconds(45f);
        Debug.Log("wasnt able to spawn fish so i will die");
        Destroy(gameObject);
    }
}
