using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform; // player
    [SerializeField] private GameObject groundPrefab;   // ground

    [Header("Settings")]
    [SerializeField] private float groundWidth;
    [SerializeField] private int groundInitial;
    [SerializeField] private float minDistanceToSpawn;

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

        // Add to active list
        groundActive.Add(newGround);

        // Update last end position
        lastEndPosition += new Vector3(groundWidth, 0, 0);
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