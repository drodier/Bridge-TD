using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamSpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private TMP_Text ui;
    [SerializeField] private bool isPlayer;

    private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position + new Vector3(0, -1, GetComponent<SpawnerManager>().IsLeft() ? 8 : -8);

        if(isPlayer)
            GameObject.Instantiate(Player, spawnPosition, transform.rotation);

        spawnPosition = spawnPosition + new Vector3(0, -2.5f, 0);
    }

    void FixedUpdate()
    {
        ui.text = GetComponent<SpawnerManager>().GetTimer().ToString();

        GetComponent<SpawnerManager>().SetSpawnPosition(spawnPosition);
    }
}
