using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 desiredPosition;
    private Vector3 axis;
    private float rotationSpeed;

    [SerializeField] private float MoveSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 1;
        Camera.main.GetComponent<CameraController>().SetPlayer(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        axis = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
        desiredPosition = transform.position + axis;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * MoveSpeed);
        if(axis.magnitude > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(axis), rotationSpeed);
    }
}
