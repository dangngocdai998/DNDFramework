﻿using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

public class ButtonScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 startScale;
    public Vector2 endScale;
    [SerializeField] bool useAudio = true;

    [SerializeField] protected UnityEvent eventOnPointDown;
    [SerializeField] protected UnityEvent eventOnPointUp;

#if UNITY_EDITOR
    private void OnValidate()
    {
        startScale = transform.localScale;

        endScale = new Vector2(startScale.x + 0.1f, startScale.y + 0.1f);
    }
#endif
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(endScale, 0.1f).SetEase(Ease.Linear).SetUpdate(true);
        if (eventOnPointDown != null)
        {
            eventOnPointDown.Invoke();
        }
        if (useAudio)
        {
            // if (AudioManager.Exists())
            // {
            //     AudioManager.Instance.Shot("ButtonClick");
            // }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(startScale, 0.1f).SetEase(Ease.Linear).SetUpdate(true);
        if (eventOnPointUp != null)
        {
            eventOnPointUp.Invoke();
        }
    }

}
