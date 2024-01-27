using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    Vector3 direction;
    float speed;
    GameObject shooter;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            shooter.GetComponent<MonkeyController>().MakeHappySound();
            collision.gameObject.GetComponent<MonkeyController>().GetHit();
        }
    }

    void Start()
    {

    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Fire(Vector3 direction, float speed, GameObject shooter)
    {
        this.direction = direction;
        this.speed = speed;
        this.shooter = shooter;
    }
}