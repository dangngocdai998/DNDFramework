﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DNDFramework.UI
{
    public class SystemLoadingPopup : BaseUIMenu
    {
        TMPro.TextMeshProUGUI mWaitText;

        private void Awake()
        {
            _UILayer = eUILayer.AlwaysOnTop;

            var background = new GameObject("Background").AddComponent<Image>();
            background.transform.SetParent(this.transform);
            background.transform.localPosition = Vector2.zero;
            background.transform.localScale = Vector2.one;
            background.color = new Color(0, 0, 0, 0.75f);
            var backgroundRect = background.GetComponent<RectTransform>();
            backgroundRect.anchorMin = Vector2.zero;
            backgroundRect.anchorMax = Vector2.one;
            backgroundRect.anchoredPosition = Vector2.zero;

            var waitText = new GameObject("WaitText").AddComponent<TMPro.TextMeshProUGUI>();
            waitText.transform.SetParent(this.transform);
            waitText.transform.localPosition = Vector2.zero;
            waitText.color = Color.white;
            waitText.enableWordWrapping = false;
            waitText.alignment = TMPro.TextAlignmentOptions.Center;
            if (DNDFramework.UI.CanvasManager.UICanvas != null)
            {
                float minSize = Mathf.Min(DNDFramework.UI.CanvasManager.UIRectTrans.rect.width, DNDFramework.UI.CanvasManager.UIRectTrans.rect.height);
                waitText.fontSize = 80 * (minSize / 1080);
            }
            else
            {
                waitText.fontSize = 80;
            }

            waitText.text = "Loading...";
            mWaitText = waitText;
        }

        private void Update()
        {
            this.transform.SetAsLastSibling();
            var timeOffset = Time.unscaledTime % 3;
            mWaitText.text = "Loading." + (timeOffset >= 1 ? "." : " ") + (timeOffset >= 2 ? "." : " ");
        }
    }
}