using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemySpawnData> enemySpawnDatas;

    public int round;
    public int roundPoints;

    void Start()
    {
        OnRoundStart();
    }

    void OnRoundStart()
    {
        roundPoints = round ^ 2 * 10;

        for (int i = 0; i < enemySpawnDatas.Count - 1; i++)
        {
            float random = UnityEngine.Random.Range(0, enemySpawnDatas[i].spawnChance / 2);
            enemySpawnDatas[i].spawnChance -= random;
            enemySpawnDatas[i+1].spawnChance += random;
        }

        StartCoroutine(DoRound());
    }

    IEnumerator DoRound()
    {
        while (roundPoints > 0)
        {
            yield return new WaitForSeconds(SpawnEnemy() + UnityEngine.Random.Range(-1.0f, 1.0f));
        }

        round++;
        OnRoundStart();
    }

    float SpawnEnemy()
    {
        float random = UnityEngine.Random.Range(0.0f, 100.0f);
        foreach(EnemySpawnData enemySpawnData in enemySpawnDatas)
        {
            if (random < enemySpawnData.spawnChance)
            {
                Instantiate(enemySpawnData.enemy, transform.position, Quaternion.identity);
                roundPoints -= enemySpawnData.pointValue;
                return enemySpawnData.waitForSecondsAfter;
            }

            random -= enemySpawnData.spawnChance;
        }
        return 0.0f;
    }
}

[Serializable]
public class EnemySpawnData
{
    public EnemyEntity enemy;
    public float spawnChance;
    public int pointValue;
    public float waitForSecondsAfter;
}
