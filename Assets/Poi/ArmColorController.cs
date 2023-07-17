using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmColorController : MonoBehaviour
{
    [SerializeField] SpriteRenderer _armSprite;
    [SerializeField] Color[] _colors;

    void Start()
    {
        _armSprite.color = _colors[Random.Range(0, _colors.Length)];
    }
}
