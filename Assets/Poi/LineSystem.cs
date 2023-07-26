using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class LineSystem : MonoBehaviour
{
    [SerializeField] Text _lineText;

    string[] Lines =
    {
        "��������������",
        "�|�C�|�C�|�C",
        "����orAlive",
        "���ʂ̃V�F�[�_�[�C�C�������"
    };

    void Awake()
    {
        _lineText.text = "";

        MessageBroker.Default.Receive<PoiGenerateController.StartMessage>().Subscribe(_=> 
        {
            InvokeRepeating(nameof(DrawLine), 0, 3.0f);
        }).AddTo(this);
        MessageBroker.Default.Receive<PoiGenerateController.StopMessage>().Subscribe(_ =>
        {
            CancelInvoke(nameof(DrawLine));
            _lineText.text = "";
        }).AddTo(this);
    }

    void DrawLine()
    {
        _lineText.text = "�u" + Lines[Random.Range(0, Lines.Length)] + "�v";
    }
}
