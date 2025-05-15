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

    public AudioClip activationSound;

    private HashSet<GameObject> stayingTargets = new();

    private bool isActivated = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (isActivated) return;
            isActivated = true;
            stayingTargets.Add(other.gameObject);

            onTriggerEnterEvent.Invoke();
            GetComponent<AudioSource>().PlayOneShot(activationSound, 0.7f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (!isActivated) return;
            isActivated = false;
            stayingTargets.Remove(other.gameObject);

            onTriggerEnterExit.Invoke();
        }
    }

    private void Update()
    {
        stayingTargets.RemoveWhere(obj => obj == null || !obj.activeInHierarchy);

        if (isActivated && stayingTargets.Count == 0)
        {
            onTriggerEnterExit.Invoke();
            isActivated = false;
        }
    }
}
