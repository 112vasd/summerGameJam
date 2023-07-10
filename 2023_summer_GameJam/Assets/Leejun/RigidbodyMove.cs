using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMove : MonoBehaviour
{

    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public float speed;
    public float power;
    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObA;
    public GameObject bulletObB;

    void Start()
    {

    }

    void Update()
    {
        Move();
        Fire();
        Reload();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }
    void Fire()
    {
        if (!Input.GetButton("Fire1"))
            return;
        if (curShotDelay < maxShotDelay)
            return;
        switch (power)
        {
            case 1:
                //power one
                Vector3 SpawnPos = new Vector3(transform.position.x, transform.position.y + 0.6f, 0);
                GameObject bullet = Instantiate(bulletObA, SpawnPos, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 4, ForceMode2D.Impulse);
                break;
            case 2:
                Vector3 SpawnPosR = new Vector3(transform.position.x, transform.position.y + 0.6f, 0);
                GameObject bulletR = Instantiate(bulletObA, SpawnPosR + Vector3.right * 0.1f, transform.rotation);
                Vector3 SpawnPosL = new Vector3(transform.position.x, transform.position.y + 0.6f, 0);
                GameObject bulletL = Instantiate(bulletObA, SpawnPosL + Vector3.left * 0.1f, transform.rotation);

                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 4, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 4, ForceMode2D.Impulse);
                break;
            case 3:
                Vector3 SpawnPosRR = new Vector3(transform.position.x, transform.position.y + 0.6f, 0);
                GameObject bulletRR = Instantiate(bulletObA, SpawnPosRR + Vector3.right * 0.25f, transform.rotation);
                Vector3 SpawnPosCC = new Vector3(transform.position.x, transform.position.y + 0.6f, 0);
                GameObject bulletCC = Instantiate(bulletObB, SpawnPosCC, transform.rotation);
                Vector3 SpawnPosLL = new Vector3(transform.position.x, transform.position.y + 0.6f, 0);
                GameObject bulletLL = Instantiate(bulletObA, SpawnPosLL + Vector3.left * 0.25f, transform.rotation);

                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * 4, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 4, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 4, ForceMode2D.Impulse);
                break;
        }

        curShotDelay = 0;

    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}