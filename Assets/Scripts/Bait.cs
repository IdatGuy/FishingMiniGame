using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bait : MonoBehaviour
{
    [SerializeField] private float startForce =20f;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.AddForce(transform.forward * (startForce * 10f));
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Fish")
        {
            Destroy(gameObject);
        }
    }
}
