using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private Vector3 desiredPosition;
    private Vector3 cameraOffset;

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
