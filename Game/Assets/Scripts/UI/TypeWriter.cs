using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TypeWriter : MonoEventDispatcher
{
    public static readonly string Finished = "TypeWrited.Finished";

    public bool IsWriting { get; set; }

    private Text _textField;
    private string _targetText;

    protected override void Awake()
    {
        base.Awake();

        _textField = GetComponent<Text>();
        _textField.text = "";
    }

    public void Type(string text)
    {
        _targetText = text;
        StartCoroutine(TypeWriteText(text));
    }

    public void Finish()
    {
        StopAllCoroutines();
        _textField.text = _targetText;
        IsWriting = false;

        Dispatch(new EventObject
        {
            Sender = this,
            Type = Finished,
            Data = _targetText
        });
    }

    private IEnumerator TypeWriteText(string targetText)
    {
        _textField.text = "";
        IsWriting = true;

        for (var i = 0; i < targetText.Length; i++)
        {
            _textField.text += targetText[i];
            yield return new WaitForSeconds(0.01f);
        }

        Dispatch(new EventObject
        {
            Sender = this,
            Type = Finished,
            Data = targetText
        });
    }
}
