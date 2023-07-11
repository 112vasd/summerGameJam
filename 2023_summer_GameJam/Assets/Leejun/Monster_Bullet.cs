using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"|| other.tag == "Border Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
