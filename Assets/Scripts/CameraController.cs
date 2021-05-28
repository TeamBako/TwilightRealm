using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
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
        this.transform.position = Vector3.Lerp(this.transform.position, playerRef.position + cameraOffset, intensity);
    }
}
