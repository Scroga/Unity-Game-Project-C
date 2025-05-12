using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GunFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 2f;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, followSpeed * Time.deltaTime);
    }
}
