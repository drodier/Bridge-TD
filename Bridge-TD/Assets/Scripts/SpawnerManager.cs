using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private int SpawnRate;
    [SerializeField] private GameObject Minion;
    [SerializeField] private int WaveSize = 10;
    [SerializeField] private bool isLeft;

    private int timer;
    private Vector3 spawnPosition;
    private bool isSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position + new Vector3(5, -1, 0);

        StartCoroutine(SpawnUnit());
    }
    
    IEnumerator SpawnUnit()
    {
        while(timer < int.MaxValue)
        {
            yield return new WaitForSecondsRealtime(1);
            timer ++;

            if(timer % SpawnRate == 0 && !isSpawning)
            {
                StartCoroutine(SpawnWave());
            }
        }
    }

    IEnumerator SpawnWave()
    {
        isSpawning = true;
        for(int i=0; i<WaveSize; i++)
        {
            MinionController currentMinion = GameObject.Instantiate(Minion, spawnPosition + Vector3.up, transform.rotation).GetComponent<MinionController>();
            currentMinion.SetTeam(isLeft);

            yield return new WaitForSecondsRealtime(0.5f);
        }
        isSpawning = false;
    }

    public bool IsLeft()
    {
        return isLeft;
    }

    public int GetTimer()
    {
        return timer;
    }

    public void SetSpawnPosition(Vector3 newPos)
    {
        spawnPosition = newPos;
    }
}
