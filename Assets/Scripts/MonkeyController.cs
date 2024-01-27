using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    [SerializeField] int playerNumber;
    [SerializeField] float maxSpeed;
    [SerializeField] GameObject aimingSphere;
    [SerializeField] float aimRadius;

    Animator animator;
    Vector3 rightThumbStickLastPosition;
    float speed;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Movement Code
        float horizontal = Input.GetAxis($"Horizontal-{playerNumber}");
        float vertical = Input.GetAxis($"Vertical-{playerNumber}");

        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0f)
        {
            float leftThumbStickAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, leftThumbStickAngle, 0);

            animator.SetBool("Walk", true);
            speed = maxSpeed;
        }
        else
        {
            animator.SetBool("Walk", false);
            speed = 0;
        }

        transform.position +=
            (Vector3.forward * vertical * speed +
            Vector3.right * horizontal * speed)
            * Time.deltaTime;


        //Aiming Code
        float rightThumbStickHorizontal = Input.GetAxis($"RightThumbStickHorizontal-{playerNumber}");
        float rightThumbStickVertical = Input.GetAxis($"RightThumbStickVertical-{playerNumber}");

        float rightThumbStickAngle = Mathf.Atan2(rightThumbStickVertical, rightThumbStickHorizontal);

        float x = Mathf.Cos(rightThumbStickAngle) * aimRadius;
        float y = rightThumbStickLastPosition.y;
        float z = Mathf.Sin(rightThumbStickAngle) * aimRadius;

        if (Mathf.Abs(rightThumbStickHorizontal) > .75 || Mathf.Abs(rightThumbStickVertical) > .75)
        {
            aimingSphere.transform.position = transform.position + new Vector3(x, y, z);
            rightThumbStickLastPosition = aimingSphere.transform.position;
        }
    }
}