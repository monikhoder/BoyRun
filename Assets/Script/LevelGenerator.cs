using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform; // player
    [SerializeField] private GameObject groundPrefab;   // ground
    [SerializeField] private GameObject coinPrefab;     // coin

    [Header("Ground Settings")]
    [SerializeField] private float groundWidth;
    [SerializeField] private int groundInitial;
    [SerializeField] private float minDistanceToSpawn;


    [Header("Coin Settings")]
    [SerializeField] private int maxCoinsPerGround = 3;
    [SerializeField] private float coinYNormal = 1f;
    [SerializeField] private float coinYJump = 3.5f;

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
        SpawnCoins(newGround);
        // Add to active list
        groundActive.Add(newGround);

        // Update last end position
        lastEndPosition += new Vector3(groundWidth, 0, 0);
    }

    private void SpawnCoins(GameObject groundObj)
    {
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
            float segmentWidth = groundWidth / (coinsToSpawn + 1);
            float xPos = segmentWidth * (i + 1);

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

            //create coin
            GameObject tempCoin = Instantiate(coinPrefab, groundObj.transform);

            tempCoin.transform.localPosition = new Vector3(xPos, yPos, 0);

            // set tag
            tempCoin.tag = "Coin";
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