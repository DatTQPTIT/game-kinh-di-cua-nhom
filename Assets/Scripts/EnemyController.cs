using UnityEngine;
using UnityEngine.AI; 

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private Transform houseEntrance;

    [SerializeField] private Transform forestCenter;
    [SerializeField] private float wanderRadius = 20f;
    [SerializeField] private float wanderTimer = 5f;
    private float timer;

    [SerializeField] private float killDistance = 1.5f;
    private EnemySpawner spawner;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        GameObject entranceObj = GameObject.Find("EntrancePoint");
        if (entranceObj != null) houseEntrance = entranceObj.transform;

        GameObject forestObj = GameObject.Find("ForestArea");
        if (forestObj != null) forestCenter = forestObj.transform;

        spawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        if (player == null || houseEntrance == null || agent == null) return;

        bool isPowerOn = GeneratorController.isPowerOn;
        bool playerInHouse = IsPlayerInHouse();

        if (!isPowerOn) // den tat
        {
            agent.SetDestination(player.position);
        }
        else // den sang
        {
            if (IsPlayerInHouse()) // ng choi trong nha
            {
                WanderInForest();
            }
            else // ng choi ben ngoai nha
            {
                agent.SetDestination(player.position);
            }
        }
    }

    void WanderInForest()
    {
        if (forestCenter == null) return;

        timer += Time.deltaTime;

        if(timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(forestCenter.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    bool IsPlayerInHouse()
    {
        Vector3 directionToPlayer = player.position - houseEntrance.position;
        return Vector3.Dot(directionToPlayer, houseEntrance.forward) > 0;
    }

    public void GetHitByFlashlight()
    {
        if (spawner != null) spawner.RespawnEnemy(this.gameObject);
    }
}