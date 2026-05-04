using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; 
    [SerializeField] private Transform[] spawnPoints; 

    private GameObject currentEnemy;

    void Start()
    {
        SpawnNewEnemy();
    }

    public void SpawnNewEnemy()
    {
        if(spawnPoints.Length == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedPoint = spawnPoints[randomIndex];

        if(currentEnemy == null)
        {
            currentEnemy = Instantiate(enemyPrefab, selectedPoint.position, selectedPoint.rotation);
        }
        else
        {
            NavMeshAgent agent = currentEnemy.GetComponent<NavMeshAgent>();
            if(agent != null)
            {
                agent.Warp(selectedPoint.position);
            }
            else
            {
                currentEnemy.transform.position = selectedPoint.position;
            }
        }
    }

 
    public void RespawnEnemy(GameObject enemy)
    {
        SpawnNewEnemy();
    }
}