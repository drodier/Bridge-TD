using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class MinionController : MonoBehaviour
{
    public int uid;

    [SerializeField] private List<GameObject> Path;
    [SerializeField] private int AttackDamage;
    [SerializeField] private float AttackSpeed;
    [SerializeField] private Transform HealthBar;
    [SerializeField] private GameObject DamageNumber;

    [SerializeField] private int currentPoint = 0;
    private int health = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private GameObject targetedEnemy;
    [SerializeField] private string team;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        uid = Random.Range(int.MinValue, int.MaxValue);
    }

    // Update is called once per frame
    void Update()
    {
        if(targetedEnemy == null)
        {
            if(currentPoint < 2 && Vector3.Distance(transform.position, Path[currentPoint].transform.position) <= 5)
                currentPoint++;

            agent.SetDestination(Path[currentPoint].transform.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    void FixedUpdate()
    {
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
            transform.rotation = Quaternion.LookRotation(Vector3.Normalize(targetedEnemy.transform.position-transform.position), transform.up);
            if(targetedEnemy.GetComponent<MinionController>().Damage(AttackDamage + Random.Range(-2, 1)) <= 0)
                targetedEnemy = null;
            yield return new WaitForSecondsRealtime(AttackSpeed);
        }

        yield return 0;
    }

    IEnumerator KillUnit()
    {
        targetedEnemy = null;

        yield return null;

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

        Instantiate(DamageNumber, transform.position, transform.rotation).GetComponent<TMP_Text>().text = damage.ToString();

        return currentHealth;
    }

    public void SetTeam(bool isBlue)
    {
        team = isBlue ? "Left" : "Right";

        Path.Add(GameObject.Find("PathNode" + team));
        Path.Add(GameObject.Find("PathNode" + (team == "Left" ? "Right" : "Left")));
        Path.Add(GameObject.Find(team + "Spawner"));
    }
}
