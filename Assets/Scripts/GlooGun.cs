using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GlooGun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    public float projectileVelocity = 300;
    public AudioClip gunShotSound;
    public GameObject particleSource;
    private ParticleSystem gunShotParticles;

    private void Start()
    {
        gunShotParticles = particleSource?.GetComponent<ParticleSystem>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Shoot();
        }
    }

    private void Shoot() 
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward.normalized * projectileVelocity, ForceMode.Force);
        GetComponent<AudioSource>().PlayOneShot(gunShotSound, 0.7f);
        gunShotParticles?.Play();
        transform.DOShakePosition(0.1f, strength: 0.1f, vibrato: 10, snapping: false, fadeOut: true);
    }
}
