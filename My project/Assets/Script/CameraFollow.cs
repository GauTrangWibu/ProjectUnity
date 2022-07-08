using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 minValues, maxValues;
    [Range(1,10)]
    [SerializeField] private float smoothFactor;
 
    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 tagetPosition = target.position + offset;
        //check it player it out of bound or not 
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(tagetPosition.x,minValues.x, maxValues.x),
            Mathf.Clamp(tagetPosition.y, minValues.y, maxValues.y),
            Mathf.Clamp(tagetPosition.z, minValues.z, maxValues.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor*Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
