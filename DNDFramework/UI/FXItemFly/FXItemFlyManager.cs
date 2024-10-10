using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class FXItemFlyManager : SingletonMonoBehaviour<FXItemFlyManager>
{


    [Header("Setup")]
    [SerializeField] private RectTransform transformCanvasFx;
    [SerializeField] private RectTransform itemFlyContainer;

    [SerializeField] float timeDelayShowItem = 0.1f;
    [SerializeField] float timeFly = 0.8f;

    private readonly Dictionary<string, Transform> _dictionaryFlyTargets = new();
    private readonly Dictionary<string, Action<bool>> _dictionaryCallBack = new();

    public void AddTransformTarget(string typeItem, Transform trans)
    {
        _dictionaryFlyTargets[typeItem] = trans;
    }
    public Transform GetTransformTarget(string typeItem)
    {
        return _dictionaryFlyTargets[typeItem];
    }

    public void ShowItemCollect(string typeItem, int numberStar, Transform posStart, bool collectUI = false, Action<bool> completedFly = null)
    {
        if (!_dictionaryFlyTargets.ContainsKey(typeItem) || _dictionaryFlyTargets[typeItem] == null)
        {
            completedFly?.Invoke(true);
            return;
        }


        _dictionaryCallBack[typeItem] = completedFly;
        if (collectUI)
        {
            ShowItemStartUI(typeItem, numberStar, posStart, _dictionaryFlyTargets[typeItem]);
        }
        else
        {
            ShowItemStartInGame(typeItem, numberStar, posStart, _dictionaryFlyTargets[typeItem]);
        }
    }


    private void ShowItemStartInGame(string typeItem, int numberStar, Transform posStart, Transform transformTarget)
    {
        if (numberStar > 20)
        {
            numberStar = 20;
        }

        if (Camera.main != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(posStart.position);
            Vector2 posAnchored = (screenPos - (transformCanvasFx.anchoredPosition)) / (float)transformCanvasFx.localScale.x;

            StartCoroutine(DelayShowItem(typeItem, numberStar, posAnchored, transformTarget, true));
        }
    }

    private void ShowItemStartUI(string typeItem, int numberStar, Transform posStart, Transform transformTarget)
    {
        if (numberStar > 20)
        {
            numberStar = 20;
        }
        // Vector2 screenPos = Camera.main.WorldToScreenPoint(posStart);
        // Vector2 posAnchored = (screenPos - (trans_CanvasFX.anchoredPosition)) / (float)trans_CanvasFX.localScale.x;

        StartCoroutine(DelayShowItem(typeItem, numberStar, posStart.position, transformTarget, false));

    }

    private IEnumerator DelayShowItem(string typeItem, int numberStar, Vector3 posAnchored, Transform transformTarget, bool insideCanvas)
    {
        string typeItemFly = typeItem;
        for (int i = 0; i < numberStar; i++)
        {
            GameObject itemSpawn = PoolingManager.Instance.GetObject(typeItemFly, posAnchored, itemFlyContainer, insideCanvas);

            FlyItem(typeItemFly, itemSpawn, transformTarget, i == numberStar - 1);
            yield return new WaitForSeconds(timeDelayShowItem);
        }
    }

    void FlyItem(string typeItemFly, GameObject item, Transform transformTarget, bool isLastItem)
    {
        var itemPosition = item.transform.position;
        var targetPosition = transformTarget.position;
        Vector3 dir = targetPosition - itemPosition;

        Vector3 pos2 = itemPosition + dir * 0.3f;

        float distance2Point = Vector3.Distance(itemPosition, targetPosition) * 0.3f;

        pos2 += new Vector3(Random.Range(-distance2Point, distance2Point), Random.Range(-distance2Point, distance2Point), 0);

        item.transform.DOPath(new Vector3[] { itemPosition, pos2, targetPosition, }, timeFly, PathType.CatmullRom).SetEase(Ease.InQuad).OnComplete(() =>
        {

            PoolingManager.Instance.DisableObjPooling(item);
            if (_dictionaryCallBack[typeItemFly] != null)
            {
                _dictionaryCallBack[typeItemFly].Invoke(isLastItem);
            }
        });
    }

}
