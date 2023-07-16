using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishController : MonoBehaviour
{
    [SerializeField]
    private float _movePower = 1000f;

    [SerializeField]
    private Rigidbody2D _rb;

    void Start()
    {
        
    }

    void Update()
    {
        _rb.AddForce();
    }
}
