using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private int SpawnRate;
    [SerializeField] private GameObject Minion;
    [SerializeField] private int WaveSize = 10;
    [SerializeField] private TMP_Text ui;

    private int timer;
    private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position + new Vector3(0, 0, 8);

        GameObject.Instantiate(Player, spawnPosition, transform.rotation);

        StartCoroutine(SpawnUnit());
    }
    
    IEnumerator SpawnUnit()
    {
        yield return new WaitForSecondsRealtime(3);
        StartCoroutine(SpawnWave());
        while(timer < int.MaxValue)
        {
            yield return new WaitForSecondsRealtime(1);
            timer ++;
            ui.text = timer.ToString();

            if(timer % SpawnRate == 0)
            {
                StartCoroutine(SpawnWave());
            }
        }
    }

    IEnumerator SpawnWave()
    {
        for(int i=0; i<WaveSize; i++)
        {
            MinionController currentMinion = GameObject.Instantiate(Minion, spawnPosition, transform.rotation).GetComponent<MinionController>();


            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    public int GetTimer()
    {
        return timer;
    }
}
