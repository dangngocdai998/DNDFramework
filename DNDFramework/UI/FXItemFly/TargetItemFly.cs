using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetItemFly : MonoBehaviour
{

    [SerializeField] private string typeItem;



    private void Start()
    {
        if (FXItemFlyManager.Exists())
        {
            FXItemFlyManager.Instance.AddTransformTarget(typeItem, transform);
        }
    }
}
