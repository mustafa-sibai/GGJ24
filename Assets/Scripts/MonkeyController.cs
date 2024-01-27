using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    Animator animator;
    [SerializeField] float maxSpeed;
    float speed;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    float angle;
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0f)
        {
            angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);

            animator.SetBool("Walk", true);
            speed = maxSpeed;
        }
        else
        {
            animator.SetBool("Walk", false);
            speed = 0;
        }

        print($"hor: {horizontal} ver: {vertical}");


        //print("xx" + );


        //if (Input.GetAxisRaw("RightTrigger") > 0)
        {


        }


        {
            //

        }

        /*{
            //speed = maxSpeed * vertical;
            print(speed);

        }
        {
            animator.SetBool("Walk", false);
            speed = 0;
        }*/

        transform.position += 
            (Vector3.forward * vertical * speed +
            Vector3.right * horizontal * speed)
            * Time.deltaTime;
    }
}