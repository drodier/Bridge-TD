using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    public int uid;

    [SerializeField] private List<GameObject> Path;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private int AttackDamage;
    [SerializeField] private float AttackSpeed;
    [SerializeField] private Transform HealthBar;

    private int currentPoint = 0;
    private Vector3 desiredPosition;
    private int health = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private GameObject targetedEnemy;
    [SerializeField] private string team;

    // Start is called before the first frame update
    void Start()
    {
        uid = Random.Range(int.MinValue, int.MaxValue);
    }

    // Update is called once per frame
    void Update()
    {
        if(targetedEnemy == null)
        {
            desiredPosition = Path[currentPoint].transform.position;
            desiredPosition = new Vector3(desiredPosition.x, transform.position.y, desiredPosition.z);

            if(currentPoint < 2 && Vector3.Distance(transform.position, desiredPosition) < 2)
                currentPoint++;
        }
    }

    void FixedUpdate()
    {
        if(targetedEnemy == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * MoveSpeed);
            transform.rotation = Quaternion.LookRotation(Vector3.Normalize(desiredPosition-transform.position), transform.up);
        }

        HealthBar.localScale = new Vector3(2, 2, ((float)currentHealth/(float)health)+0.03f);
        HealthBar.transform.localPosition = Vector3.MoveTowards(Vector3.zero, new Vector3(0, 0, -(1-(float)currentHealth/(float)health)/2), 1);
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Minion")
        {
            if(other.GetComponent<MinionController>().GetTeam() != team)
            {
                if(targetedEnemy == null)
                {
                    targetedEnemy = other.gameObject;
                    StartCoroutine(AttackEnemy());
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Minion" && targetedEnemy != null)
        {
            if(other.GetComponent<MinionController>().uid == targetedEnemy.GetComponent<MinionController>().uid)
            {
                targetedEnemy = null;
            }
        }
    }

    IEnumerator AttackEnemy()
    {
        while(targetedEnemy != null)
        {
            targetedEnemy.GetComponent<MinionController>().Damage(AttackDamage);
            yield return new WaitForSecondsRealtime(AttackSpeed);
        }

        yield return 0;
    }

    IEnumerator KillUnit()
    {
        targetedEnemy = null;
        
        yield return new WaitForSecondsRealtime(.5f);

        GameObject.Destroy(this.gameObject);
    }

    public string GetTeam()
    {
        return team;
    }

    public int Damage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            StartCoroutine(KillUnit());
            currentHealth = 0;
        }

        return currentHealth;
    }

    public void SetTeam(bool isBlue)
    {
        team = isBlue ? "Left" : "Right";

        if(Random.Range(1, 3) == 1)
        {
            Path.Add(GameObject.Find("BottomPath" + (isBlue ? "1" : "2")));
            Path.Add(GameObject.Find("BottomPath" + (isBlue ? "2" : "1")));
            Path.Add(GameObject.Find(isBlue ? "RightSpawner" : "LeftSpawner"));
        }
        else
        {
            Path.Add(GameObject.Find("TopPath" + (isBlue ? "1" : "2")));
            Path.Add(GameObject.Find("TopPath" + (isBlue ? "2" : "1")));
            Path.Add(GameObject.Find(isBlue ? "RightSpawner" : "LeftSpawner"));
        }
    }
}
