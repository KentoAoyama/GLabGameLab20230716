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
        "人間だ！人間だ！人間だ！",
        "進路は東へ！",
        "ついでに神も仏も拾ってしまえ！",
        "お前が舵を取れぇー！",
        "死んでるのか生きてるのかぁ！",
        "高鳴る鼓動で血液が噴き出してきたーッ！",
        "孤独などガリガリ食い散らかしてやれ！",
        "高々お前も俺も人間だ！",
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
        _lineText.text = "「" + Lines[Random.Range(0, Lines.Length)] + "」";
    }
}
