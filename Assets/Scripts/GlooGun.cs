using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro.EditorUtilities;
using UnityEngine;

public class GlooGun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    public float projectileVelocity = 300;
    public AudioClip gunShotSound;
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
        transform.DOShakePosition(0.2f, strength: 0.1f, vibrato: 10, randomness: 90, snapping: false, fadeOut: true);
    }
}
