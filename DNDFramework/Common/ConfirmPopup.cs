using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ConfirmPopup : SingletonMonoBehaviour<ConfirmPopup>
{
    [Header("Setup")]
    [SerializeField] TextMeshProUGUI txt_Title;
    [SerializeField] TextMeshProUGUI txt_Description;
    [SerializeField] GameObject btn_Yes, btn_No;
    [SerializeField] GameObject popup;

    bool showing = false;
    Action<bool> callBack;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        popup.SetActive(false);
    }

    public void Show(string _title, string _description, Action<bool> _callBack)
    {
        popup.SetActive(true);
        txt_Title.text = _title;
        txt_Description.text = _description;
        if (showing == true)
        {
            if (callBack != null)
            {
                callBack.Invoke(false);
            }
        }
        showing = true;
        callBack = _callBack;
    }

    public void ClickBtn(bool value)
    {
        popup.SetActive(false);
        if (callBack != null)
        {
            callBack.Invoke(value);
        }
    }
}
