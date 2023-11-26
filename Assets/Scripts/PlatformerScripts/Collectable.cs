using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, IItem
{
    public static event Action<int> OnCollect;
    public int worth = 5;
    public void Collect()
    {
        OnCollect.Invoke(worth);
        Destroy(gameObject);
    }


}
