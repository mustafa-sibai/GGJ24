using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BananaManager : MonoBehaviour
{
    [SerializeField] GameObject bananasPrefab;
    [SerializeField] GameObject BananaSpawnPoint;
    [SerializeField] GameObject WhenToSpawnBanana;
    [SerializeField] GameObject WhenToDestroyBanana;

    [SerializeField] GameObject currentBananaGameObject;
    [SerializeField] private float d;

    void Start()
    {
        currentBananaGameObject.GetComponent<BGScroller>().WhenToDestroyBanana = WhenToDestroyBanana;
    }

    void Update()
    {
        float spawnDistance = Vector2.Distance(
            currentBananaGameObject.transform.position,
            WhenToSpawnBanana.transform.position);
        print(spawnDistance);
        if (spawnDistance <= d)//70
        {
            currentBananaGameObject = Instantiate(bananasPrefab, BananaSpawnPoint.transform.position, Quaternion.identity);
            currentBananaGameObject.transform.parent = gameObject.transform;
            currentBananaGameObject.transform.localScale = Vector3.one;
            currentBananaGameObject.transform.localRotation = Quaternion.identity;

            currentBananaGameObject.GetComponent<BGScroller>().WhenToDestroyBanana = WhenToDestroyBanana;
        }
    }
}