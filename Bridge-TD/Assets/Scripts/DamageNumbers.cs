using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumbers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteTimer());
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);
    }

    IEnumerator DeleteTimer()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-50, 50), 200, Random.Range(-50, 50)));
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
