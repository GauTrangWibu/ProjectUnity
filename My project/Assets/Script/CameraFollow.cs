using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [Range(1,10)]
    [SerializeField] public float smoothFactor;
 
    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 tagetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, tagetPosition, smoothFactor*Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
