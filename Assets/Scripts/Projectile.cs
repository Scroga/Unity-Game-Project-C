using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;

    private Transform assignedTransform;
    private Vector3 localHitPoint;
    private Vector3 originalScale;
    private List<Projectile> linkedProjectiles = new List<Projectile>();

    private bool isFrozen = false;
    private bool wasFrozen = false;

    private float wholeLifeSpan = 50f;
    [SerializeField]
    private float freezeTime = 10f;
    [SerializeField]
    private float afterFreezeLifeSpan = 15f;

    [SerializeField]
    private float maxSizeFactor = 3f;
    [SerializeField]
    private float minSizeFactor = 1.5f;

    private float sizeFactor = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        sizeFactor = Random.Range(minSizeFactor, maxSizeFactor);
        originalScale = transform.localScale * Random.Range(0.7f, 1.3f);

        transform.rotation = Random.rotation;
        transform.localScale = originalScale;

        Invoke("OnDeath", wholeLifeSpan);
    }

    void Update()
    {
        if (isFrozen && assignedTransform != null)
        {
            transform.position = assignedTransform.TransformPoint(localHitPoint);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") return;
        if (isFrozen || wasFrozen) return;

        if (collision.gameObject.tag == "Projectile")
        {
            Projectile otherProjectile = collision.gameObject.GetComponent<Projectile>();
            if (!otherProjectile) return;
            if (!otherProjectile.isFrozen) return;

            if (otherProjectile.isFrozen)
            {
                if (!linkedProjectiles.Contains(otherProjectile))
                {
                    linkedProjectiles.Add(otherProjectile);
                    otherProjectile.linkedProjectiles.Add(this);
                }
            }
        }

        assignedTransform = collision.transform;
        Vector3 hitPoint = collision.contacts[0].point;
        localHitPoint = assignedTransform.InverseTransformPoint(hitPoint);

        rb.isKinematic = true;
        isFrozen = true;
        wasFrozen = true;
        Freeze();
    }

    private void Freeze()
    {
        transform.localScale = originalScale * sizeFactor;
        Invoke("Defreeze", freezeTime);
    }

    public void Defreeze()
    {
        if (!isFrozen) return;

        isFrozen = false;
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        transform.localScale = originalScale;

        foreach (var proj in linkedProjectiles)
        {
            if (proj != null && proj.isFrozen)
            {
                proj.Defreeze();
            }
        }
        Invoke("OnDeath", afterFreezeLifeSpan);
    }

    private void OnDeath()
    {
        Debug.Log("Dead");
        Destroy(gameObject);
    }
}