using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject leftUpperLeg;
    [SerializeField] GameObject leftLeg;
    [SerializeField] GameObject leftFoot;

    [SerializeField] GameObject rightUpperLeg;
    [SerializeField] GameObject rightLeg;
    [SerializeField] GameObject rightFoot;

    [SerializeField] GameObject leftTarget;
    [SerializeField] GameObject rightTarget;

    [SerializeField] Vector3 originalTarget;
    [SerializeField] bool theBeaconsAreLit;
    Animator animator;
    float t = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 randomPos = Random.insideUnitCircle / 2;
            leftTarget.transform.position =
                transform.position +
                transform.forward * 1.25f +
                -transform.right +
                new Vector3(randomPos.x, 0, randomPos.y);

            randomPos = Random.insideUnitCircle / 2;
            rightTarget.transform.position =
                transform.position +
                transform.forward * 1.25f +
                transform.right +
                new Vector3(randomPos.x, 0, randomPos.y);
        }
    }

    void LateUpdate()
    {
        leftUpperLeg.transform.position = leftTarget.transform.position;
        rightUpperLeg.transform.position = rightTarget.transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftTarget.transform.position);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
        animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftTarget.transform.rotation);



    }
}