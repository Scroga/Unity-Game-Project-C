using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [Tooltip("Tag to check for on entering objects")]
    public string targetTag = "Projectile";

    [Tooltip("Events to invoke when the object with the tag enters the trigger")]
    public UnityEvent onTriggerEnterEvent;
    [Tooltip("Events to invoke when the object with the tag exits the trigger")]
    public UnityEvent onTriggerEnterExit;

    public AudioClip gunShotSound;

    private bool wasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (wasActivated) return;
            wasActivated = true;

            Debug.Log($"Object with tag '{targetTag}' entered the trigger.");
            onTriggerEnterEvent.Invoke();
            GetComponent<AudioSource>().PlayOneShot(gunShotSound, 0.7f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (!wasActivated) return;
            wasActivated = false;

            Debug.Log($"Object with tag '{targetTag}' exited the trigger.");
            onTriggerEnterExit.Invoke();
        }
    }
}
