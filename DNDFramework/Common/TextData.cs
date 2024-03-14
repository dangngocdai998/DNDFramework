using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextData : MonoBehaviour
{
    [Header("Setup Data")]
    [Header("Use for textMeshProUI")]
    [SerializeField] DataUpdateType typeData;
    [SerializeField] TextMeshProUGUI txt_Data;


    public void OnEnable()
    {
        this.RegisterListener(EventID.UpdateData, (obj) => UpdateDataView(obj != null ? (DataUpdateType)obj : typeData));
        UpdateDataView(typeData);
    }
    public void OnDisable()
    {
        this.RemoveListener(EventID.UpdateData);
    }
    void UpdateDataView(DataUpdateType type)
    {
        if (type != typeData)
            return;
        if (!txt_Data)
        {
            Debug.LogError("NULL TEXT MESH PRO in " + gameObject.name);
            return;
        }

        switch (typeData)
        {
            // case DataUpdateType.STAR:
            //     txt_Data.text = $"<color=yellow>{GameplayController.Instance.numberStarCollect}</color>/3";
            //     break;
            // case DataUpdateType.TIMEPLAY:
            //     txt_Data.text = CovertTimeDisplay(GameplayController.Instance.timePlaying);
            //     break;
            // case DataType.DIAMON:

            //     break;
            // case DataType.AMETA:
            //     if (ConnectManager.I.CurrentUser.token >= 0)
            //     {
            //         // txt_Data.text = GameData.CutString(GameData.I.ametaBalance.ToString(), 8);
            //         txt_Data.text = GameData.I.ChangeNumber((decimal)Math.Floor(ConnectManager.I.CurrentUser.token));
            //     }
            //     else
            //         txt_Data.text = "---";
            //     break;
            // case DataType.KEYGOLD:
            //     key = GameData.I.dataItemUser.Find(data => data.GetItemCode() == ItemCode.GOLDENKEY && data.group == ItemGroup.PRODUCT);
            //     if (key != null)
            //     {
            //         txt_Data.text = GameData.I.ChangeNumber((decimal)key.quantity);
            //     }
            //     else
            //     {
            //         txt_Data.text = "0";
            //     }
            //     break;
            // case DataType.KEYSILVER:
            //     key = GameData.I.dataItemUser.Find(data => data.GetItemCode() == ItemCode.SILVERKEY && data.group == ItemGroup.PRODUCT);
            //     if (key != null)
            //     {
            //         txt_Data.text = GameData.I.ChangeNumber((decimal)key.quantity);
            //     }
            //     else
            //     {
            //         txt_Data.text = "0";
            //     }
            //     break;
        }
    }

    string CovertTimeDisplay(float time)
    {
        int _time = Mathf.FloorToInt(time);
        return $"{Mathf.FloorToInt(_time / 60f).ToString("D2")}:{(_time % 60).ToString("D2")}";
    }
}


public enum DataUpdateType
{

}


