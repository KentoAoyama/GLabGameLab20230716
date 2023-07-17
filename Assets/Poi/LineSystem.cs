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
        "�l�Ԃ��I�l�Ԃ��I�l�Ԃ��I",
        "�i�H�͓��ցI",
        "���łɐ_�������E���Ă��܂��I",
        "���O���ǂ���ꂥ�[�I",
        "����ł�̂������Ă�̂����I",
        "����ۓ��Ō��t�������o���Ă����[�b�I",
        "�ǓƂȂǃK���K���H���U�炩���Ă��I",
        "���X���O�������l�Ԃ��I",
        "Captain of the ship Oh!",
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
