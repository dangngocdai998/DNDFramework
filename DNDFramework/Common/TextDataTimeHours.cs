using System;
using System.Collections;
using System.Collections.Generic;
using DNDFramework;
using TMPro;
using UnityEngine;

public class TextDataTimeHours : MonoBehaviour
{
    [Header("Setup Data")]
    [Header("Use for textMeshProUI")]
    // [SerializeField] DataType typeData;
    [SerializeField] TextMeshProUGUI txt_Data;
    const string eventUpdateDataTime = "UpdateDataTime";
    public void OnEnable()
    {
        this.RegisterListener(eventUpdateDataTime, (obj) => UpdateDataTime((double)obj));
        // UpdateDataTime(GameData.I.GetTimeReal);
    }
    public void OnDisable()
    {
        this.RemoveListener(eventUpdateDataTime);
    }
    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void UpdateDataTime(double time)
    {
        if (!txt_Data)
        {
            Debug.LogError("NULL TEXT MESH PRO in "/*  + gameObject.name */);
            return;
        }
        txt_Data.text = ConvertTime(time);

    }

    string ConvertTime(double _time)
    {
        // Debug.Log("CovertTime: " + _time);
        int hours = Mathf.FloorToInt((long)_time / 3600f);
        int minus = Mathf.FloorToInt(((long)_time - hours * 3600f) / 60f);
        /* if (hours > 12)
        {
            return $"{hours - 12}:{minus.ToString("D2")} PM";
        }
        else
        { */
        return $"{hours.ToString("D2")}:{minus.ToString("D2")}";
        // }

    }
}
