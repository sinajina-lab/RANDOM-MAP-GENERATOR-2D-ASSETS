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

    //different groups of nuts that will be spawned
    [SerializeField]private List<Transform> spawnedLevelParts = new List<Transform>();
    private Vector3 lastEndPosition;

    private void Awake()
    {
        if (levelPart_Start == null)
        {
            Debug.LogError("Level part start transform is null!");
            return;
        }

        if (player == null)
        {
            Debug.LogError("Player object is null!");
            return;
        }

        lastEndPosition = levelPart_Start.Find("EndPosition").position;

        
        for (int i = 0; i < spawnedLevelParts.Count; i++)
        {
            SpawnLevelPart();
        }
    }

    private void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player object is null!");
            return;
        }

        if (player !=null && Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
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

        //if (levelPart_1 == null)
        //{
        //    Debug.LogError("Level part prefab is null!");
        //    return;
        //}

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

        //if (levelPartTransform == null)
        //{
        //    Debug.LogError("Level part transform is null!");
        //    return;
        //}

        levelPartTransform.position = lastEndPosition;
        var t = levelPartTransform.Find("EndPosition");
        if(t == null)
        {
            Debug.Log("Can't find EndPosition");
        }
        //lastEndPosition = levelPartTransform.Find("EndPosition").position;

        spawnedLevelParts.Add(levelPartTransform);
    }

    private void DisableLevelPart(Transform levelPartTransform)
    {
        levelPartTransform.gameObject.SetActive(false);
        spawnedLevelParts.Remove(levelPartTransform);
        Destroy(levelPartTransform.gameObject);
    }
}
