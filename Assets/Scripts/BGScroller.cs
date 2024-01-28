using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScroller : MonoBehaviour
{
    [SerializeField] private float _x, _y, _z;
    [SerializeField] private float d;

    public GameObject WhenToDestroyBanana;

    void Update()
    {
        transform.position += new Vector3(_x, _y, _z) * Time.deltaTime;

        float distace = Vector2.Distance(WhenToDestroyBanana.transform.position, transform.position);
        //print(distace);

        if(distace <= 180)//180
        {
            Destroy(gameObject);
        }
    }
}
