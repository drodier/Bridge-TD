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
    [SerializeField] private bool isBlue;
    [SerializeField] private bool isPlayer = false;

    private int timer;
    private Vector3 spawnPosition;
    private bool isSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position + new Vector3(0, 0, isBlue ? 8 : -8);

        if(isPlayer)
            GameObject.Instantiate(Player, spawnPosition, transform.rotation);

        StartCoroutine(SpawnUnit());
    }
    
    IEnumerator SpawnUnit()
    {
        while(timer < int.MaxValue)
        {
            yield return new WaitForSecondsRealtime(1);
            timer ++;
            ui.text = timer.ToString();

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
            currentMinion.SetTeam(isBlue);

            yield return new WaitForSecondsRealtime(0.5f);
        }
        isSpawning = false;
    }

    public int GetTimer()
    {
        return timer;
    }
}
