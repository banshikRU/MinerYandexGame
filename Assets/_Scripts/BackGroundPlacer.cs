using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundPlacer : MonoBehaviour
{
    public Transform Player;
    public BackGround [] LevelPrefabs;
    public BackGround FirstLevel;

    private List<BackGround> spawnedLevels = new List<BackGround>();

    private void Start()
    {
        spawnedLevels.Add(FirstLevel);
    }
    public void SpawnLevel()
    {
        if (LevelPrefabs == null || LevelPrefabs.Length == 0)
        {
            return;
        }
        BackGround newLevel = Instantiate(LevelPrefabs[Random.Range(0, LevelPrefabs.Length)]);
        if (spawnedLevels.Count > 0)
        {
            newLevel.transform.position = spawnedLevels[spawnedLevels.Count - 1].End.position - newLevel.Begin.localPosition;
        }
        spawnedLevels.Add(newLevel);
        if (spawnedLevels.Count >= 10)
        {
            Destroy(spawnedLevels[0].gameObject);
            spawnedLevels.RemoveAt(0);
        }
    }
    public void CheckForPlayerPosition()
    {
        if (Player.position.y - spawnedLevels[spawnedLevels.Count -1].Begin.position.y < 5)
        {
            SpawnLevel();
        }
    }
}
