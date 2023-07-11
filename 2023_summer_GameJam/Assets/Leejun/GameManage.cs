using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManage : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject player;
    public Text scoreText;
    public Image[] lifeImage;
    public GameObject gameOverset;
    public GameObject gameClear;

    void Update()
    {
        curShotDelay += Time.deltaTime;

        if (curShotDelay > maxShotDelay)
        {
            SpawnEnemy();
            maxShotDelay = Random.Range(0.5f, 3f);
            curShotDelay = 0;
        }

        //UI score Update
        RigidbodyMove playerLogic = player.GetComponent<RigidbodyMove>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
        if (playerLogic.score >= 1000)
        {
            GameClear();
        }
    }
    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 9);
        GameObject enemy = Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        momster enemyLogic = enemy.GetComponent<momster>();
        enemyLogic.player = player;

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

    public void UpdateLifeIcon(int life)
    {
        //UI Life Init Disable
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }

        //UI Life Init Active
        for (int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
        player.transform.position = Vector3.down * 4.3f;
        player.SetActive(true);
    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 4.3f;
        player.SetActive(true);
        RigidbodyMove.Die = true;
    }
    public void GameOver()
    {
        gameOverset.SetActive(true);
    }
    public void GameClear()
    {
        gameClear.SetActive(true);
    }
}
