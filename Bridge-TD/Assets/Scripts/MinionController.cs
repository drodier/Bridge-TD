using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    [SerializeField] private List<GameObject> Path;
    [SerializeField] private float MoveSpeed;

    private int currentPoint = 0;
    private Vector3 desiredPosition;

    // Start is called before the first frame update
    void Start()
    {
        if(Random.Range(1, 3) == 1)
        {
            Path.Add(GameObject.Find("BottomPath1"));
            Path.Add(GameObject.Find("BottomPath2"));
            Path.Add(GameObject.Find("RightSpawner"));
        }
        else
        {
            Path.Add(GameObject.Find("TopPath1"));
            Path.Add(GameObject.Find("TopPath2"));
            Path.Add(GameObject.Find("RightSpawner"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        desiredPosition = Path[currentPoint].transform.position;
        desiredPosition = new Vector3(desiredPosition.x, transform.position.y, desiredPosition.z);

        if(currentPoint < 2 && Vector3.Distance(transform.position, desiredPosition) < 2)
            currentPoint++;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * MoveSpeed);
        transform.rotation = Quaternion.LookRotation(Vector3.Normalize(desiredPosition-transform.position), transform.up);
    }
}
