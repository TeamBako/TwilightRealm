using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 camerUpperBounds;
    public Vector3 camerLowerBounds;

    public Vector3 cameraOffset;
    public Transform playerRef;
    public float intensity = 1;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = playerRef.position + cameraOffset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempPos = playerRef.position + cameraOffset;

        if(tempPos.x < -camerLowerBounds.x)
        {
            tempPos.x = -camerLowerBounds.x;
        }
        else if (tempPos.x > camerUpperBounds.x)
        {
            tempPos.x = camerUpperBounds.x;
        }

        if (tempPos.z < -camerLowerBounds.z)
        {
            tempPos.z = -camerLowerBounds.z;
        }
        else if (tempPos.z > camerUpperBounds.z)
        {
            tempPos.z = camerUpperBounds.z;
        }

        this.transform.position = Vector3.Lerp(this.transform.position, tempPos, intensity);
    }
}
