using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;
    [SerializeField] const float PLAYER_DISTANCE_REMOVE_LEVEL_PART = 100f;

    [SerializeField] Transform levelPart_Start;
    [SerializeField] Transform levelPart_1;
    [SerializeField] private GameObject player;

    private List<Transform> spawnedLevelParts = new List<Transform>();
    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;

        int startingSpawnLevelParts = 5;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnLevelPart();
        }

        if (spawnedLevelParts.Count > 0 && Vector3.Distance(player.transform.position, spawnedLevelParts[0].position) > PLAYER_DISTANCE_REMOVE_LEVEL_PART)
        {
            DisableLevelPart(spawnedLevelParts[0]);
        }
    }

    private void SpawnLevelPart()
    {
        Transform levelPartTransform;

        if (spawnedLevelParts.Count > 0)
        {
            levelPartTransform = spawnedLevelParts[0];
            spawnedLevelParts.RemoveAt(0);
            levelPartTransform.gameObject.SetActive(true);
        }
        else
        {
            levelPartTransform = Instantiate(levelPart_1);
        }

        levelPartTransform.position = lastEndPosition;
        lastEndPosition = levelPartTransform.Find("EndPosition").position;

        spawnedLevelParts.Add(levelPartTransform);
    }

    private void DisableLevelPart(Transform levelPartTransform)
    {
        levelPartTransform.gameObject.SetActive(false);
        spawnedLevelParts.Remove(levelPartTransform);
        Destroy(levelPartTransform.gameObject);
    }
}
