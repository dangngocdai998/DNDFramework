using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ToastPopup : SingletonMonoBehaviour<ToastPopup>
{
    [Header("Setup")]
    [SerializeField] TextMeshProUGUI txt_Title;
    [SerializeField] TextMeshProUGUI txt_Description;
    [SerializeField] Image icon, bg;
    [SerializeField] Animator animator;

    [Header("Sprite")]
    [SerializeField] Sprite sprite_Success;
    [SerializeField] Sprite sprite_Info;
    [SerializeField] Sprite sprite_Warning;
    [SerializeField] Sprite sprite_Error;

    bool showing = false;

    Queue<ToastData> queueToast = new Queue<ToastData>();
    public void Show(TypeToast type, string _title, string _description, string _fishCode = "")
    {
        queueToast.Enqueue(new ToastData(type, _title, _description, _fishCode));
        if (showing == false)
        {
            showing = true;
            StartCoroutine("ShowToast");
        }
    }
    Color color;
    IEnumerator ShowToast()
    {
        ToastData data = queueToast.Dequeue();
        txt_Title.text = data.title;
        txt_Description.text = data.description;

        switch (data.type)
        {
            case TypeToast.SUCCESS:

                icon.sprite = sprite_Success;
                if (ColorUtility.TryParseHtmlString("#b2ffc4", out color))
                { bg.color = color; }
                if (ColorUtility.TryParseHtmlString("#055605", out color))
                { txt_Title.color = color; }
                if (ColorUtility.TryParseHtmlString("#198305", out color))
                { txt_Description.color = color; }
                break;
            case TypeToast.INFO:
                icon.sprite = sprite_Info;
                if (ColorUtility.TryParseHtmlString("#b2f3ff", out color))
                { bg.color = color; }
                if (ColorUtility.TryParseHtmlString("#04209b", out color))
                { txt_Title.color = color; }
                if (ColorUtility.TryParseHtmlString("#0d4ab9", out color))
                { txt_Description.color = color; }
                break;
            case TypeToast.WARNING:
                icon.sprite = sprite_Warning;
                if (ColorUtility.TryParseHtmlString("#ffe591", out color))
                { bg.color = color; }
                if (ColorUtility.TryParseHtmlString("#980f0f", out color))
                { txt_Title.color = color; }
                if (ColorUtility.TryParseHtmlString("#a7300b", out color))
                { txt_Description.color = color; }
                break;
            case TypeToast.ERROR:
                icon.sprite = sprite_Error;
                if (ColorUtility.TryParseHtmlString("#ffdfdf", out color))
                { bg.color = color; }
                if (ColorUtility.TryParseHtmlString("#921111", out color))
                { txt_Title.color = color; }
                if (ColorUtility.TryParseHtmlString("#ad1a1a", out color))
                { txt_Description.color = color; }
                break;
        }




        animator.Play("ToastShow", -1, 0);
        yield return new WaitForSeconds(2);
        animator.Play("ToastClose", -1, 0);
        yield return new WaitForSeconds(0.2f);

        if (queueToast.Count > 0)
        {
            StartCoroutine("ShowToast");
        }
        else
        {
            showing = false;
        }
    }

}
[Serializable]
public class ToastData
{
    public TypeToast type;
    public string title;
    public string description;
    public string fishCode;
    public ToastData(TypeToast _type, string _title, string _description, string _fishCode)
    {
        type = _type;
        title = _title;
        description = _description;
        fishCode = _fishCode;
    }
}