using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField]
    private Vector2 spawnForce = new Vector2(1, 7);

    private bool allowCollect = true;

    public void Setup()
    {
        StartCoroutine(nameof(SpawnItemProcess));
    }

    private IEnumerator SpawnItemProcess()
    {
        allowCollect = false;

        var rigid = gameObject.AddComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        rigid.velocity = new Vector2(Random.Range(-spawnForce.x, spawnForce.x), spawnForce.y);

        while(rigid.velocity.y > 0)
        {
            yield return null;
        }

        allowCollect = true;
        GetComponent<Collider2D>().isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(allowCollect && collision.CompareTag("Player"))
        {
            UpdateCollision(collision.transform);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(allowCollect && collision.transform.CompareTag("Player"))
        {
            UpdateCollision(collision.transform);
            Destroy(gameObject);
        }
    }

    public abstract void UpdateCollision(Transform target);
}
