using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class momster : MonoBehaviour
{
    float time;

    public string enemyName;
    public int enemyScore;
    public float speed;
    public float health;
    public Sprite[] sprites;
    public GameManage manage;


    public float maxShotDelay = 3f;
    public float curShotDelay;

    public GameObject bulletObA;
    public GameObject bulletObB;
    public GameObject player;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        time = 0.0f;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.8f)
        {
            Debug.Log("0");
            Fire();
            time = 0.0f;
        }
        Reload();
    }

    void Fire()
    {
        if (curShotDelay < maxShotDelay)
            return;


        if (enemyName == "S")
        {
            Vector2 rot = new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
            float angle = Mathf.Atan2(rot.x, rot.y) * Mathf.Rad2Deg * -1;
            Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject bullet = Instantiate(bulletObA, transform.position, angleAxis);

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec * 0.5f, ForceMode2D.Impulse);
        }
        else if (enemyName == "L")
        {
            Debug.Log("1");
            Vector2 rot = new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
            float angle = Mathf.Atan2(rot.x, rot.y) * Mathf.Rad2Deg * -1;
            Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject bulletR = Instantiate(bulletObA, transform.position + Vector3.right * 0.3f, angleAxis);
            GameObject bulletL = Instantiate(bulletObA, transform.position + Vector3.left * 0.3f, angleAxis);


            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            //Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            //Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            Vector3 dirVecR = player.transform.position - transform.position;
            Vector3 dirVecL = player.transform.position - transform.position;

            rigidR.AddForce(dirVecR * 0.5f, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL * 0.5f, ForceMode2D.Impulse);

        }

        curShotDelay = 0;
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }


    void OnHit(int dmg)
    {
        RigidbodyMove playerLogic = player.GetComponent<RigidbodyMove>();
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);
        if (health <= 0)
        {
            playerLogic.score += enemyScore;

            Destroy(gameObject);
        }
        if (playerLogic.score >= 100)
        {
            manage.GameClear();
        }
    }
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border Bullet")
            Destroy(gameObject);
        else if (collision.gameObject.tag == "playerbullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
        }
    }
}
