﻿using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }
}
