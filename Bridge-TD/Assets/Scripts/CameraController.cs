using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float minCamZoom;
    [SerializeField] private float maxCamZoom;

    private GameObject player;
    private Vector3 desiredPosition;
    private Vector3 cameraOffset;
    private Vector3 scrollDelta;

    [SerializeField] private float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = new Vector3(10, 13, 0);
    }

    void Update()
    {
        if(player != null)
            desiredPosition = player.transform.position + cameraOffset;

        scrollDelta = new Vector3(Input.mouseScrollDelta.y, Input.mouseScrollDelta.y*1.1f, 0);

        cameraOffset -= scrollDelta;

        if(cameraOffset.magnitude < minCamZoom || cameraOffset.magnitude > maxCamZoom)
            cameraOffset += scrollDelta;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * MoveSpeed);
    }

    public void SetPlayer(GameObject other)
    {
        transform.position = other.transform.position;
        player = other;
    }
}
