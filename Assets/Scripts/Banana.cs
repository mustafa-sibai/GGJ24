using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    Vector3 direction;
    float speed;

    void Start()
    {

    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Fire(Vector3 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }
}