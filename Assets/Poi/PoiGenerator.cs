using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Random = UnityEngine.Random;

/// <summary>
/// ポイの生成を始める/辞める
/// </summary>
public static class PoiGenerateController
{
    public struct StartMessage { }
    public struct StopMessage { }

    /// <summary>
    /// 生成開始
    /// </summary>
    public static void Start() => MessageBroker.Default.Publish(new StartMessage());
    /// <summary>
    /// 生成止める、再起動不可能
    /// </summary>
    public static void Stop() => MessageBroker.Default.Publish(new StopMessage());
}

public class PoiGenerator : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] GameObject _nukePrefab;
    [Header("生成間隔")]
    [SerializeField] float _interval = 3.0f;
    [SerializeField] float _minInterval = 0.5f;
    [Header("生成個数を上げる閾値")]
    [SerializeField] int _levelupThreshold = 10;

    Transform[] _spawnPoints;
    /// <summary>
    /// 生成したポイを保持しておく。要素数は適当
    /// </summary>
    Queue<GameObject> _poiQueue = new(10);
    float _currentInterval;
    float _spawnCount;
    float _timer;
    bool _isValid;

    void Awake()
    {
        Init();
        RegisterSpawnPoints();

        // メッセージを受信したら生成開始/止める
        MessageBroker.Default.Receive<PoiGenerateController.StartMessage>().Subscribe(_ =>
        {
            _isValid = true;
            _timer = 0;
        }).AddTo(this);
        MessageBroker.Default.Receive<PoiGenerateController.StopMessage>().Subscribe(_ =>
        {
            _isValid = false;
            DestroyPoiAll();
            Init();
        }).AddTo(this);
    }

    void Init()
    {
        _currentInterval = _interval;
        _timer = 0;
        _spawnCount = 0;
    }

    void Update()
    {
        if (!_isValid) return;

        _timer += Time.deltaTime;
        if (_timer > _currentInterval)
        {
            _currentInterval -= Random.Range(0.05f, 0.1f);
            _currentInterval = Mathf.Max(_currentInterval, _minInterval);

            _spawnCount++;
            _timer = 0;
            SpawnPoi();
            CheckPoi();
        }
    }

    void SpawnPoi()
    {
        int count = (int)(_spawnCount / _levelupThreshold) + 1;
        for(int i = 0; i < count; i++)
        {
            Vector3 spawnPos = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;

            if (Random.value < 0.05f)
            {
                GameObject instance = Instantiate(_nukePrefab, spawnPos, Quaternion.identity);
                _poiQueue.Enqueue(instance);
            }
            else
            {
                GameObject instance = Instantiate(_prefab, spawnPos, Quaternion.identity);
                _poiQueue.Enqueue(instance);
            }
        }
    }

    /// <summary>
    /// 非表示になっているポイを削除
    /// </summary>
    void CheckPoi()
    {
        for(int i = 0; i < _poiQueue.Count; i++)
        {
            GameObject poi = _poiQueue.Dequeue();
            if (!poi.activeInHierarchy)
            {
                Destroy(poi);
            }
            else
            {
                _poiQueue.Enqueue(poi);
            }
        }
    }

    /// <summary>
    /// ポイをすべて削除する
    /// </summary>
    void DestroyPoiAll()
    {
        if (_poiQueue == null || _poiQueue.Count == 0) return;
        foreach (GameObject poi in _poiQueue)
        {
            if (poi == null) continue;
            Destroy(poi);
        }
    }

    /// <summary>
    /// 自身の子を湧き場所として登録する
    /// </summary>
    void RegisterSpawnPoints()
    {
        _spawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _spawnPoints[i] = transform.GetChild(i);
        }
    }
}
