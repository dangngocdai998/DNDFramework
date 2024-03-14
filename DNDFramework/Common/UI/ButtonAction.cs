using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAction : MonoBehaviour
{
    [SerializeField] TypeInput typeInput;

    [SerializeField] Image[] img_Target;

    [SerializeField] bool scalerTarget = false;

    [Range(0, 1)]
    [SerializeField] float valueAlphaColor = 1;

    const float valueTransTarget = 0.7f;

    public void OnTargetDown()
    {
        ChangeImgTarget(true);
        CustomInput.SetAxis(typeInput, 1);
        if (scalerTarget)
            ScalerTarget(Vector3.one * 1.01f);
    }
    public void OnTargetUp()
    {
        ChangeImgTarget(false);
        CustomInput.SetAxis(typeInput, 0);
        if (scalerTarget)
            ScalerTarget(Vector3.one);
    }

    public void OnClickAction()
    {
        CustomInput.SetAxis(typeInput, 1);
        CustomInput.listInputClick.Add(typeInput);
    }


    public void ChangeImgTarget(bool value)
    {
        if (img_Target.Length > 0)
        {
            for (int i = 0; i < img_Target.Length; i++)
            {
                img_Target[i].color = new Color(img_Target[i].color.r, img_Target[i].color.g, img_Target[i].color.b, value ? valueTransTarget * valueAlphaColor : valueAlphaColor);
            }
        }
    }

    public void ScalerTarget(Vector3 value)
    {
        if (img_Target.Length > 0)
        {
            for (int i = 0; i < img_Target.Length; i++)
            {
                img_Target[i].transform.DOScale(value, 0.1f).SetEase(Ease.Linear).SetUpdate(true);
            }
        }
    }
}
