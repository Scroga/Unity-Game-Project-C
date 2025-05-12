using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    [SerializeField]
    private Vector3 SliderDirection = Vector3.back;

    private Vector3 StartPosition;

    [SerializeField]
    private float SlideAmount = 1.9f;
    [SerializeField]
    private float Speed = 1.0f;

    private bool IsOpen = false;
    private Coroutine AnimationCoroutine;

    [SerializeField]
    public AudioClip DoorSound;

    void Start()
    {
        StartPosition = transform.position;

    }

    public void Open() 
    {
        if (IsOpen) return;

        if (AnimationCoroutine != null)
            StopCoroutine(AnimationCoroutine);

        AnimationCoroutine = StartCoroutine(DoSlidigOpen());
        GetComponent<AudioSource>().PlayOneShot(DoorSound, 0.7f);
    }

    private IEnumerator DoSlidigOpen() 
    {
        Vector3 endPosition = StartPosition + SlideAmount * SliderDirection;
        Vector3 startPosition = transform.position;

        float time = 0;
        IsOpen = true;
        while (time < 1) 
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }

    }
    public void Close()
    {
        if (!IsOpen) return;

        if (AnimationCoroutine != null)
            StopCoroutine(AnimationCoroutine);
        AnimationCoroutine = StartCoroutine(DoSlidigClose());
        GetComponent<AudioSource>().PlayOneShot(DoorSound, 0.7f);
    }

    private IEnumerator DoSlidigClose()
    {
        Vector3 endPosition = StartPosition;
        Vector3 startPosition = transform.position;

        float time = 0;
        IsOpen = false;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }

    }


}
