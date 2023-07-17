using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InsectGenerator : MonoBehaviour
{
    [SerializeField]
    private float _width = 50f;

    [SerializeField] 
    private float _height = 50f;

    [SerializeField]
    private InsectController _insectPrefab = default;

    [SerializeField]
    private Transform[] _generatePositions;

    [SerializeField]
    private float _generateInterval = 5f;

    [SerializeField]
    private InGameController _gameController;

    private float _timer;

    private static bool _isGenerate = false;

    private void Start()
    {
        _isGenerate = false;
        _timer = _generateInterval;
    }

    public static void GenerateStart()
    {
        _isGenerate = true;
    }

    public static void GenerateStop()
    {
        _isGenerate= false;
    }

    void Update()
    {
        if (!_isGenerate) return;

        _timer += Time.deltaTime;

        if (_timer > _generateInterval)
        {
            var x = Random.Range(-_width / 2, _width / 2);
            var y = Random.Range(-_height / 2 + transform.position.y, _height / 2 + transform.position.y);

            var insect = Instantiate(_insectPrefab);
            var index = Random.Range(0, _generatePositions.Length);
            insect.Generate(_generatePositions[index], new Vector3(x, y, 0f), _gameController);

            _timer = 0f;
        }
    }
}
