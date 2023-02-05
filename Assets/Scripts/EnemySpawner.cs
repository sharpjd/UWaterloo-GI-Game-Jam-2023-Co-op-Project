using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public Text roundText;

    public List<EnemySpawnData> enemySpawnDatas;

    List<EnemyEntity> enemiesAlive = new();

    public int round;
    public int roundPoints;

    public float timeScale;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale= timeScale;
        OnRoundStart();
    }

    void OnRoundStart()
    {
        roundPoints = (int) (Mathf.Pow(round, 1.3f) * 2) + 3;

        StartCoroutine(DoRound());
    }

    IEnumerator DoRound()
    {
        while (roundPoints > 0)
        {
            yield return new WaitForSeconds(SpawnEnemy() + UnityEngine.Random.Range(-1.0f, 1.0f));
        }
    }

    void OnRoundEnd()
    {
        for (int i = enemySpawnDatas.Count - 2; i >= 0; i--)
        {
            if (enemySpawnDatas[i].spawnChance <= 5) continue;
            float random = UnityEngine.Random.Range(0, enemySpawnDatas[i].spawnChance / 6);
            enemySpawnDatas[i].spawnChance -= random;
            enemySpawnDatas[i+1].spawnChance += random;
        }
    } 

    float SpawnEnemy()
    {
        float random = UnityEngine.Random.Range(0.0f, 100.0f);
        foreach(EnemySpawnData enemySpawnData in enemySpawnDatas)
        {
            if (random <= enemySpawnData.spawnChance)
            {
                enemiesAlive.Add(Instantiate(enemySpawnData.enemy, transform.position, Quaternion.identity));
                roundPoints -= enemySpawnData.pointValue;
                return enemySpawnData.waitForSecondsAfter;
            }

            random -= enemySpawnData.spawnChance;
        }
        return 0.0f;
    } 

    public void OnEnemyDie(EnemyEntity deadEnemy)
    {
        enemiesAlive.Remove(deadEnemy);
        if (roundPoints <= 0 && enemiesAlive.Count == 0)
        {
            OnRoundEnd();
            round++;
            roundText.text = "Round " + round.ToString();
            OnRoundStart();
        }
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
