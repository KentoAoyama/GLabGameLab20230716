using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Random = UnityEngine.Random;

/// <summary>
/// �|�C�̐������n�߂�/���߂�
/// </summary>
public static class PoiGenerateController
{
    public struct StartMessage { }
    public struct StopMessage { }

    /// <summary>
    /// �����J�n
    /// </summary>
    public static void Start() => MessageBroker.Default.Publish(new StartMessage());
    /// <summary>
    /// �����~�߂�A�ċN���s�\
    /// </summary>
    public static void Stop() => MessageBroker.Default.Publish(new StopMessage());
}

public class PoiGenerator : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] GameObject _nukePrefab;
    [Header("�����Ԋu")]
    [SerializeField] float _interval = 3.0f;
    [SerializeField] float _minInterval = 0.5f;
    [Header("���������グ��臒l")]
    [SerializeField] int _levelupThreshold = 10;

    Transform[] _spawnPoints;
    /// <summary>
    /// ���������|�C��ێ����Ă����B�v�f���͓K��
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

        // ���b�Z�[�W����M�����琶���J�n/�~�߂�
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
    /// ��\���ɂȂ��Ă���|�C���폜
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
    /// �|�C�����ׂč폜����
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
    /// ���g�̎q��N���ꏊ�Ƃ��ēo�^����
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
