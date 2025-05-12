using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Vector3 SliderDirection = Vector3.up;

    private Vector3 StartPosition;

    [SerializeField]
    private float SlideAmount = 10.0f;
    [SerializeField]
    private float Speed = 1.0f;

    private bool IsOpen = false;
    private Coroutine AnimationCoroutine;

    [SerializeField]
    public AudioClip PlatformSound;

    void Start()
    {
        StartPosition = transform.position;

    }

    public void MoveForward()
    {
        if (IsOpen) return;

        if (AnimationCoroutine != null)
            StopCoroutine(AnimationCoroutine);

        AnimationCoroutine = StartCoroutine(DoSlidigForward());
        GetComponent<AudioSource>().PlayOneShot(PlatformSound, 0.7f);
    }

    private IEnumerator DoSlidigForward()
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
    public void MoveBack()
    {
        if (!IsOpen) return;

        if (AnimationCoroutine != null)
            StopCoroutine(AnimationCoroutine);
        AnimationCoroutine = StartCoroutine(DoSlidigBack());
        GetComponent<AudioSource>().PlayOneShot(PlatformSound, 0.7f);
    }

    private IEnumerator DoSlidigBack()
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
