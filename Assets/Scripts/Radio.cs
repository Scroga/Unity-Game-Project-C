using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Radio : MonoBehaviour
{
    private HashSet<Projectile> stayingProjectiles = new();
    private string targetTag = "Projectile";
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
            if (!projectile) return;
            if (!projectile.isFrozen) return;

            stayingProjectiles.Add(projectile);
            if (!audioSource.isPlaying) return;
            audioSource.Pause();
        }
    }

    private void Update()
    {
        stayingProjectiles.RemoveWhere(obj => obj == null || !obj.isFrozen);
        if (!audioSource.isPlaying && stayingProjectiles.Count == 0)
        {
            audioSource.UnPause();
        }
    }
}
