using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Random = UnityEngine.Random;

public class TransitionFX : SingletonMonoBehaviour<TransitionFX>
{
    [SerializeField]
    Animator animator;
    Action endStartFadeAction;
    Action endEndFadeAction;
    bool onStartFade = false;
    // [SerializeField] RectTransform lineProgress;
    // [SerializeField] TextMeshProUGUI txt_ValueProgres;
    public void StartFade(Action action = null)
    {
        if (onStartFade)
        {
            if (action != null)
            {
                endStartFadeAction += action;
            }
        }
        else
        {
            onStartFade = true;
            if (action != null)
            {
                endStartFadeAction += action;
            }
            animator.Play("TransitionMaskIn", -1, 0f);
            // lineProgress.sizeDelta = new Vector2(0, lineProgress.sizeDelta.y);
            // txt_ValueProgres.text = "0%";
        }
    }

    public void EndFade(Action action = null)
    {
        // if (lineProgress.sizeDelta.x == maxLine)
        {
            onStartFade = false;
            if (action != null)
            {
                endEndFadeAction = action;
            }

            animator.Play("TransitionMaskOut", -1, 0f);
        }
    }

    public void EndStartFadeAction()
    {
        endStartFadeAction?.Invoke();

        endStartFadeAction = null;
    }

    public void EndFadeAction()
    {
        endEndFadeAction?.Invoke();
        endEndFadeAction = null;
    }

    // float maxLine = 845;

    public void ChangeValueProgress(float value)
    {
        /* DOTween.Kill(lineProgress);
        lineProgress.DOSizeDelta(new Vector2(maxLine * value, lineProgress.sizeDelta.y), 1000).SetSpeedBased().SetEase(Ease.Linear).OnUpdate(() =>
        {
            txt_ValueProgres.text = (int)((lineProgress.sizeDelta.x / maxLine) * 100f) + "%";
        }); */
    }

    public void FakeRunProgress(bool endFade = false)
    {
        StartCoroutine("DelayFakeRun", endFade);
    }
    public void SetRunFullProgress()
    {
        // DOTween.Kill(lineProgress);
        // lineProgress.DOSizeDelta(new Vector2(maxLine, lineProgress.sizeDelta.y), 1000).SetSpeedBased().SetEase(Ease.Linear).OnUpdate(() =>
        // {
        //     txt_ValueProgres.text = (int)((lineProgress.sizeDelta.x / maxLine) * 100f) + "%";
        // }).OnComplete(() =>
        // {
        //     EndFade();
        // });
    }
    IEnumerator DelayFakeRun(bool endFade)
    {
        ChangeValueProgress(Random.Range(0.1f, 0.5f));
        yield return new WaitForSeconds(Random.Range(2f, 3f));
        ChangeValueProgress(Random.Range(0.7f, 0.9f));
        yield return new WaitForSeconds(Random.Range(2f, 3f));
        ChangeValueProgress(1);
        if (endFade)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            EndFade();
        }

    }
}
