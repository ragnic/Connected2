using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float FollowSpeed = 2f;
    public float offset = 1f;

    void Update()
    {
        Vector3 newPosition = new Vector3(target.position.x,target.position.y + offset,-10);
        transform.position = Vector3.Lerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
    }
}
