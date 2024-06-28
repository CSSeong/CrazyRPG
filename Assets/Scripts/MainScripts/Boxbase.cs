using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxbase : MonoBehaviour
{
   public virtual void UpdateCollision()
    {
        Debug.Log($"{gameObject.name} 보물상자 충돌");
    }
}
