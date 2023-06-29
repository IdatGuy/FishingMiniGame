using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fish : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private HealthConfigSO _healthConfigSO;
    [SerializeField] private HealthSO _currentHealthSO;

    [SerializeField] private float startForce = 20f;
    private Rigidbody rb;

    [SerializeField] private float hitSpreadAngle;
    [SerializeField] private float hitForce;

    public bool IsDead { get; set; }

    public event UnityAction OnDie;
    private void Awake()
    {
        //If the HealthSO hasn't been provided in the Inspector (as it's the case for the player),
        //we create a new SO unique to this instance of the component. This is typical for enemies.
        if (_currentHealthSO == null)
        {
            _currentHealthSO = ScriptableObject.CreateInstance<HealthSO>();
            _currentHealthSO.SetMaxHealth(_healthConfigSO.InitialHealth);
            _currentHealthSO.SetCurrentHealth(_healthConfigSO.InitialHealth);
        }
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * (startForce * 10f));
    }

    public void ReceiveAnAttack(int damage)
    {
        if (IsDead)
            return;

        _currentHealthSO.InflictDamage(damage);

        if (_currentHealthSO.CurrentHealth <= 0)
        {
            IsDead = true;

            if (OnDie != null)
                OnDie.Invoke();

            //_currentHealthSO.SetCurrentHealth(_healthConfigSO.InitialHealth);
        }
        else
        {
            //take the hit then get launched in a new direction
            HitFish();
        }
    }
    private void HitFish()
    {
        Vector3 hitDirection = GetHitDirection();
        rb.AddForce(hitDirection * hitForce);
    }

    private Vector3 GetHitDirection()
    {
        Quaternion randomRotation = Quaternion.Euler(Random.Range(-hitSpreadAngle, hitSpreadAngle), Random.Range(-hitSpreadAngle, hitSpreadAngle), 0f);
        Vector3 direction = Vector3.up;
        direction = randomRotation * direction;

        return direction;
    }
}
