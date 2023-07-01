using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fish : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int hitsToCapture;
    private int currentHitsToCapture;

    [Header("Rigidbody stuff")]
    [SerializeField] private float startForce;
    [SerializeField] private float startTorque;
    private Rigidbody rb;

    [SerializeField] private float hitSpreadAngle;
    [SerializeField] private float hitForce;

    private Vector3 spawnPosition;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHitsToCapture = hitsToCapture;
        spawnPosition = transform.position;
        rb.AddForce(transform.forward * startForce);
        rb.AddTorque(transform.right * startTorque);
    }
    private void Update()
    {
        if (spawnPosition.y - 5f >= transform.position.y)
        {
            Destroy(gameObject);
        }
    }
    public void ReceiveAnAttack(int damage)
    {
        if(currentHitsToCapture <= 0)
        {
            KillFish();
        }
        else
        {
            currentHitsToCapture--;
            HitFish();
        }
    }
    private void HitFish()
    {
        Vector3 hitDirection = GetRandomHitDirection();
        rb.AddForce(hitDirection * hitForce);
    }
    private void KillFish()
    {
        //play a special particle effect
        Destroy(gameObject);
    }
    private Vector3 GetRandomHitDirection()
    {
        Quaternion randomRotation = Quaternion.Euler(Random.Range(-hitSpreadAngle, hitSpreadAngle), Random.Range(-hitSpreadAngle, hitSpreadAngle), 0f);
        Vector3 direction = Vector3.up;
        direction = randomRotation * direction;

        return direction;
    }
}
