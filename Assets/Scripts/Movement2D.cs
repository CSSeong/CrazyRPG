using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0;
    [SerializeField]
    private Vector3 moveDerection = Vector3.zero;

    private void Update()
    {
        transform.position += moveDerection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDerection = direction;
    }
}
