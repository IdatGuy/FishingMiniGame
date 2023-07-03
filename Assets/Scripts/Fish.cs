using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fish : MonoBehaviour
{
    [Header("Health")]
    private Health m_Health;

    [Header("Rigidbody stuff")]
    [SerializeField] private float startForce;
    [SerializeField] private float startTorque;
    private Rigidbody rb;

    [Tooltip("VFX prefab to spawn upon impact")]
    [SerializeField] private GameObject ImpactVfx;

    [Tooltip("LifeTime of the VFX before being destroyed")]
    [SerializeField] private float ImpactVfxLifetime = 5f;
    [SerializeField] private float hitSpreadAngle;
    [SerializeField] private float hitForce;
    [SerializeField] private MeshRenderer meshRenderer;

    private Vector3 spawnPosition;
    private void Awake()
    {
        m_Health = GetComponent<Health>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        m_Health.OnDie += OnDie;
        m_Health.OnDamaged += OnDamaged;
    }
    private void OnDisable()
    {
        m_Health.OnDie -= OnDie;
        m_Health.OnDamaged -= OnDamaged;
    }
    // Start is called before the first frame update
    void Start()
    {
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
    public void OnDamaged(float damage, GameObject damageSource)
    {
        Vector3 hitDirection = GetRandomHitDirection();
        rb.velocity = Vector3.zero;
        rb.AddForce(hitDirection * hitForce);
    }
    public void OnDie()
    {
        rb.isKinematic = true;
        meshRenderer.enabled = false;
        // impact vfx
        if (ImpactVfx)
        {
            GameObject impactVfxInstance = Instantiate(ImpactVfx, transform);
            if (ImpactVfxLifetime > 0)
            {
                Destroy(impactVfxInstance.gameObject, ImpactVfxLifetime);
                Destroy(gameObject, ImpactVfxLifetime);
            }
        }
    }
    private Vector3 GetRandomHitDirection()
    {
        Quaternion randomRotation = Quaternion.Euler(Random.Range(-hitSpreadAngle, hitSpreadAngle), Random.Range(-hitSpreadAngle, hitSpreadAngle), 0f);
        Vector3 direction = Vector3.up;
        direction = randomRotation * direction;

        return direction;
    }
}
