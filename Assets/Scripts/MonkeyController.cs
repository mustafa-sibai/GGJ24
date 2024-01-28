using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    [SerializeField] int playerNumber;

    [SerializeField] int health;

    [SerializeField] Renderer renderer;

    [SerializeField] float maxMovementSpeed;
    [SerializeField] GameObject aimingSphere;
    [SerializeField] float aimRadius;

    [SerializeField] GameObject bananaPrefab;
    [SerializeField] float bananaSpeed;
    public float currentHeldBananas; //Made this public so I can access the player within the monkeyJar
    [SerializeField] float bananaFireRate;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip deathAudioClip;
    [SerializeField] AudioClip happyAudioClip;

    [SerializeField] TMP_Text NoOfBananastext;

    [SerializeField] float dodgeForce;
    Vector3 dodgeVector;
    float dodgeTimer = 0;

    Rigidbody rb;
    Vector3 bananaDirection;

    Animator animator;
    Vector3 rightThumbStickLastPosition;
    float speed;

    float bananaFireRateTimer;

    Vector3 direction;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        if (NoOfBananastext != null)
            NoOfBananastext.text = "Player " + playerNumber + "'s Bananas: " + currentHeldBananas;

        //Dodge Code
        dodgeTimer += Time.deltaTime;
        if (Input.GetButtonDown($"X-{playerNumber}"))
        {
            dodgeVector = transform.forward * dodgeForce;
            dodgeTimer = 0;
        }
        else
        {
            dodgeVector = Vector3.Lerp(dodgeVector, Vector3.zero, dodgeTimer);
        }

        //Movement Code
        float horizontal = Input.GetAxis($"Horizontal-{playerNumber}");
        float vertical = Input.GetAxis($"Vertical-{playerNumber}");

        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0f)
        {
            float leftThumbStickAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, leftThumbStickAngle, 0);

            animator.SetBool("Walk", true);
            speed = maxMovementSpeed;
        }
        else
        {
            animator.SetBool("Walk", false);
            speed = 0;
        }

        direction = (Vector3.forward * vertical) + (Vector3.right * horizontal) + dodgeVector;
        transform.position += direction * speed * Time.deltaTime;

        //Aiming Code
        float rightThumbStickHorizontal = Input.GetAxis($"RightThumbStickHorizontal-{playerNumber}");
        float rightThumbStickVertical = Input.GetAxis($"RightThumbStickVertical-{playerNumber}");

        float rightThumbStickAngle = Mathf.Atan2(rightThumbStickVertical, rightThumbStickHorizontal);

        float x = Mathf.Cos(rightThumbStickAngle) * aimRadius;
        float y = 0;
        float z = Mathf.Sin(rightThumbStickAngle) * aimRadius;

        if (Mathf.Abs(rightThumbStickHorizontal) > .75 || Mathf.Abs(rightThumbStickVertical) > .75)
        {
            aimingSphere.transform.position = transform.position + new Vector3(x, y, z);
            rightThumbStickLastPosition = aimingSphere.transform.position;
        }

        //Shooting
        bananaFireRateTimer += Time.deltaTime;
        bananaDirection = (aimingSphere.transform.position - transform.position).normalized;
        if (Input.GetAxis($"RightTrigger-{playerNumber}") > 0 && bananaFireRateTimer > bananaFireRate && currentHeldBananas > 0)
        {
            GameObject go = Instantiate(bananaPrefab, transform.position + bananaDirection * 1.75f, Quaternion.identity);
            go.GetComponent<Banana>().Fire(bananaDirection, bananaSpeed, gameObject);
            bananaFireRateTimer = 0;
            currentHeldBananas--;
        }
    }

    public void MakeHappySound()
    {
        Invoke("PlayHappySound", 1);
    }

    void PlayHappySound()
    {
        //audioSource.PlayOneShot(happyAudioClip);
    }

    public void GetHit()
    {
        animator.SetTrigger("Spin");
        StartCoroutine(FlashRed());
        audioSource.PlayOneShot(happyAudioClip);
    }

    public void CollectBanana()
    {
        currentHeldBananas++;
    }

    IEnumerator FlashRed()
    {
        for (int i = 0; i < 3; i++)
        {
            renderer.material.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            renderer.material.color = Color.white;
            yield return new WaitForSeconds(0.15f);
        }

        yield return null;
    }

    public void Die()
    {
        animator.SetBool("Die", true);
        audioSource.PlayOneShot(deathAudioClip);
        Destroy(gameObject, 3);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, bananaDirection * 10);
    }
}