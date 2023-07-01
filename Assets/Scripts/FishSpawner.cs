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

    /*private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the bait
        if (other.CompareTag("Bait"))
        {
            // Trigger the fish spawning logic for this bait
            Bait bait = other.GetComponent<Bait>();
            bait.TrySpawnFish(waterTransform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the bait
        if (other.CompareTag("Bait"))
        {
            Bait bait = other.GetComponent<Bait>();
            bait.CancelSpawnFish();
        }
    }*/
}
