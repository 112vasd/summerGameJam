using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxShotDelay;
    public float curShotDelay;
    void Update()
    {
        curShotDelay += Time.deltaTime;

        if (curShotDelay > maxShotDelay)
        {
            SpawnEnemy();
            maxShotDelay = Random.Range(0.5f, 3f);
            curShotDelay = 0;
        }
    }
    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 9);
        GameObject enemy = Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        momster enemyLogic = enemy.GetComponent<momster>();
        if (ranPoint == 5 || ranPoint == 6)//right spawn
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (ranPoint == 7 || ranPoint == 8)//left spawn
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else//front spawn
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
    }
}
