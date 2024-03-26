using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject slimePrefab;
    public GameObject npcPrefab;

    public int leftRange = 4;
    public int rightRange = 4;
    public int upRange = 4;
    public int downRange = 4;
    
    public int maxNumberOfSlimes = 10;
    public int maxNumberOfNpcs = 3;

    void Start()
    {
        SpawnSlime();
        SpawnNpc();
    }

    private void SpawnSlime()
    {
        for (int i = 0; i < Random.Range(1, maxNumberOfSlimes); i++)
        {
            Instantiate(slimePrefab, new Vector3(Random.Range(-leftRange, rightRange), Random.Range(-downRange, upRange), 0),
                Quaternion.identity);
        }
    }

    private void SpawnNpc()
    {
        for (int i = 0; i < Random.Range(1, maxNumberOfNpcs); i++)
        {
            Instantiate(npcPrefab, new Vector3(Random.Range(-leftRange, rightRange), Random.Range(-downRange, upRange), 0),
                Quaternion.identity);
        }
    }
}