using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishController : MonoBehaviour
{
    [SerializeField]
    private float _movePower = 1000f;

    [SerializeField]
    private float _speedLimit = 10f;

    [SerializeField]
    private Rigidbody2D _rb;

    private float _currentMoveSpeed = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        Vector2 input = GetInput();

        if (input.sqrMagnitude > 0)
        {

        }
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
