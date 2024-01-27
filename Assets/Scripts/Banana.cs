using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    Vector3 direction;
    float speed;
    GameObject shooter;
    bool canDealDamage;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (canDealDamage)
            {
                shooter.GetComponent<MonkeyController>().MakeHappySound();
                collision.gameObject.GetComponent<MonkeyController>().GetHit();
            }
            else
            {
                collision.gameObject.GetComponent<MonkeyController>().CollectBanana();
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        canDealDamage = true;
    }

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        speed = Mathf.Lerp(speed, 0, timer * 0.25f);

        if (speed <= 0.1f)
        {
            canDealDamage = false;
        }

        transform.position += direction * speed * Time.deltaTime;
    }

    public void Fire(Vector3 direction, float speed, GameObject shooter)
    {
        this.direction = direction;
        this.speed = speed;
        this.shooter = shooter;
    }
}