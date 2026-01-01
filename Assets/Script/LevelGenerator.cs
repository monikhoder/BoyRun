using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Pool;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform; // player
    [SerializeField] private GameObject groundPrefab;   // ground
    [SerializeField] private GameObject coinPrefab;     // coin
    [SerializeField] private GameObject FlyingEnemyPrefab; // flying enemy
    [SerializeField] private GameObject GroundEnemyPrefab; // ground enemy

    [Header("Ground Settings")]
    [SerializeField] private float groundWidth;
    [SerializeField] private int groundInitial;
    [SerializeField] private float minDistanceToSpawn;


    [Header("Coin Settings")]
    [SerializeField] private int maxCoinsPerGround = 3;
    [SerializeField] private float coinYNormal = 1f;
    [SerializeField] private float coinYJump = 3.5f;

    [Header("Enemy Settings")]
    [SerializeField] private int maxEnemiesPerGround = 4;
    [SerializeField] private float enemyYGround = -0.5f;
    [SerializeField] private float enemyYFly = 2.0f;
    [SerializeField] private float minSpacing = 1.5f;  //spacing between coins and enemies



    private Vector3 lastEndPosition;
    private List<GameObject> groundActive = new List<GameObject>();
    private ObjectPool<GameObject> pool;

    void Awake()
    {
        // Create a pool
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(groundPrefab),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );
    }

    void Start()
    {

        lastEndPosition = transform.position;

        //Create start grounds
        for (int i = 0; i < groundInitial; i++)
        {
            SpawnGround();
        }
    }

    void Update()
    {
        // Add new ground
        if (Vector3.Distance(playerTransform.position, lastEndPosition) < minDistanceToSpawn)
        {
            SpawnGround();
        }

        // Remove old ground
        RemoveOldGround();
    }

    private void SpawnGround()
    {

        GameObject newGround = pool.Get();

        newGround.transform.position = lastEndPosition;
        newGround.transform.rotation = Quaternion.identity;

        // Spawn coins
        List<Vector3> spawnCoinPositions = SpawnCoins(newGround);

        // Spawn enemies
        SpawnEnemies(newGround, spawnCoinPositions);

        // Add to active list
        groundActive.Add(newGround);

        // Update last end position
        lastEndPosition += new Vector3(groundWidth, 0, 0);
    }

    private List<Vector3> SpawnCoins(GameObject groundObj)
    {
        //list coin positions
        List<Vector3> spawnCoinPositions = new List<Vector3>();

        // Remove old coin
        foreach (Transform child in groundObj.transform)
        {
            if (child.CompareTag("Coin"))
            {
                Destroy(child.gameObject);
            }
        }

        // set coin parent to ground
        int coinsToSpawn = Random.Range(1, maxCoinsPerGround + 1);

        // create coins
        for (int i = 0; i < coinsToSpawn; i++)
        {
            // calculate position
            // float segmentWidth = groundWidth / (coinsToSpawn + 1);
            // float xPos = segmentWidth * (i + 1);
            float xPos = Random.Range(2f, groundWidth - 2f);

            float yPos;
            float randomValue = Random.Range(0f, 1f);

            if (randomValue > 0.5f)
            {
                yPos = coinYJump;
            }
            else
            {
                yPos = coinYNormal;
            }

            //create Fly enemy
            GameObject tempCoin = Instantiate(coinPrefab, groundObj.transform);

            tempCoin.transform.localPosition = new Vector3(xPos, yPos, 0);

            // set tag
            tempCoin.tag = "Coin";

            spawnCoinPositions.Add(new Vector3(xPos, yPos, 0));
        }
        return spawnCoinPositions;
    }
    private void SpawnEnemies(GameObject groundObj, List<Vector3> coinPositions)
    {
        //list enemies positions
        List<Vector3> enemiesPositions = new List<Vector3>();

        // Remove old Enemy
        foreach (Transform child in groundObj.transform)
        {
            if (child.CompareTag("Enemy"))
            {
                Destroy(child.gameObject);
            }
        }

        // set enemy parent to ground
        int enemiesToSpawn = Random.Range(1, maxEnemiesPerGround + 1);

        // create enemies
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // calculate position
            float xPos = Random.Range(2f, groundWidth - 2f);
            float yPos;
            GameObject enemyToSpawn;


            if (Random.value > 0.5f )
            {
                enemyToSpawn = FlyingEnemyPrefab;
                yPos = enemyYFly;
            }
            else
            {
                enemyToSpawn = GroundEnemyPrefab;
                yPos = enemyYGround;
            }
            bool isNearEnemy = false;
          foreach(Vector3 enemyPos in enemiesPositions)
            {
                float distanceX = Mathf.Abs(xPos - enemyPos.x);
                if(distanceX < minSpacing)
                {
                    isNearEnemy = true;
                   break;
                }

            }
            if(!isNearEnemy){
                foreach(Vector3 coinPos in coinPositions)
                {
                    float distanceX = Mathf.Abs(xPos - coinPos.x);

                    if(distanceX < minSpacing)
                    {
                        if(coinPos.y == coinYNormal)
                        {
                            enemyToSpawn = FlyingEnemyPrefab;
                            yPos = enemyYFly;
                        }
                        else
                        {   enemyToSpawn = GroundEnemyPrefab;
                            yPos = enemyYGround;
                        }
                        break;
                    }
                }
            }
            if (!isNearEnemy)
            {
                //create enemy
                GameObject tempEnemy = Instantiate(enemyToSpawn, groundObj.transform);

                tempEnemy.transform.localPosition = new Vector3(xPos, yPos, 0);
                // set tag
                tempEnemy.tag = "Enemy";
            }
            enemiesPositions.Add(new Vector3(xPos,yPos, 0));
        }

    }

    private void RemoveOldGround()
    {

        if (groundActive.Count > groundInitial + 2)
        {
            GameObject oldesGround = groundActive[0];
            if (playerTransform.position.x - oldesGround.transform.position.x > 15f)
            {
                pool.Release(oldesGround);
                groundActive.RemoveAt(0);
            }
        }
    }
}


