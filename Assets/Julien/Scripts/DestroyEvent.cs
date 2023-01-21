using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEvent : MonoBehaviour
{
    public void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}
