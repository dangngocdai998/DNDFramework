using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFollowDevice : SingletonMonoBehaviour<CanvasFollowDevice>
{
    [ReadOnly] public float Aspect;
    public bool GizmosUpdate = true;
    public bool changeCamSize = true;
    public bool showDrawLine = false;
    public bool IsInvert;
    [ReadOnly] public float CurrentCamSize;

#if UNITY_EDITOR
    public static Action OnSolutionChanged;
#endif

    public List<ResolutionInfor> Resolutions = new List<ResolutionInfor>
    {
        new ResolutionInfor
        {
            Name = "Fold 2 5G Tablet",
            Aspect = 2208f / 1768f
        },
        new ResolutionInfor
        {
            Name = "Ipad",
            Aspect = 2732f / 2048f
        },
        new ResolutionInfor
        {
            Name = "Iphone 7",
            Aspect = 2208f / 1242f
        },
        new ResolutionInfor
        {
            Name = "Iphone XS Max",
            Aspect = 2437f / 1125f
        },
        new ResolutionInfor
        {
            Name = "Fold2 5G Phone",
            Aspect = (2658f / 960f) + 0.1f
        }
    };

    [SerializeField] private CanvasScaler[] _canvasScalers;
    [SerializeField] private Camera _cam;

    public override void Awake()
    {
        base.Awake();


        // var canvas = gameObject.GetComponent<Canvas>();
        if (_cam)
        {
            _cam = Camera.main;
            // canvas.worldCamera = _cam;
        }
        FixCamSizeFollowScreen();


        Debug.Log("Resolution: " + Screen.width + "/" + Screen.height);
        // #if UNITY_EDITOR
        //         OnSolutionChanged += Update;
        // #endif
    }


    [ContextMenu("Fix cam zide follow screen")]
    private void FixCamSizeFollowScreen()
    {
        if (this != null && !enabled)
            return;

        if (_cam == null)
        {
            _cam = Camera.main;
        }
        if (IsInvert)
        {
            if (_cam)
            {
                Aspect = 1 / _cam.aspect;
            }
        }
        else
        {
            if (_cam)
            {
                Aspect = _cam.aspect;
            }
        }

        for (int i = 0; i < Resolutions.Count - 1; i++)
        {
            if (Mathf.Approximately(Aspect, Resolutions[i].Aspect))
            {
                SetCanvasScalers(Mathf.Clamp(Resolutions[i].Scaler, 0f, 1f));

                if (_cam && changeCamSize)
                {
                    if (_cam.orthographic)
                    {
                        _cam.orthographicSize = Resolutions[i].CamSize;
                    }
                    // else
                    // {
                    //     _cam.fieldOfView = Resolutions[i].PerspectiveSize;
                    // }
                }
                if (_cam)
                {
                    CurrentCamSize = _cam.orthographicSize;
                }
                return;
            }
            else
            {
                if (Aspect > Resolutions[i].Aspect && Aspect < Resolutions[i + 1].Aspect)
                {
                    SetCanvasScalers(Mathf.Clamp(Resolutions[i].Scaler + (Aspect - Resolutions[i].Aspect) / (Resolutions[i + 1].Aspect - Resolutions[i].Aspect) * (Resolutions[i + 1].Scaler - Resolutions[i].Scaler), 0f, 1f));
                    if (_cam && changeCamSize)
                    {
                        if (_cam.orthographic)
                        {
                            _cam.orthographicSize = Resolutions[i].CamSize + (Aspect - Resolutions[i].Aspect) / (Resolutions[i + 1].Aspect - Resolutions[i].Aspect) * (Resolutions[i + 1].CamSize - Resolutions[i].CamSize);
                        }
                        else
                        {
                            //_cam.fieldOfView = Resolutions[i].PerspectiveSize + (Aspect - Resolutions[i].Aspect) / (Resolutions[i + 1].Aspect - Resolutions[i].Aspect) * (Resolutions[i + 1].PerspectiveSize - Resolutions[i].PerspectiveSize);
                        }
                    }
                    if (_cam)
                    {
                        CurrentCamSize = _cam.orthographicSize;
                    }
                    return;
                }
            }
        }
        // if (_cam)
        // {
        //     CurCamSize = _cam.orthographicSize;
        // }
    }

    void DrawResolution()
    {
        for (int i = 0; i < Resolutions.Count; i++)
        {
            float height = 2f * Resolutions[i].CamSize;

            float width;

            if (IsInvert)
            {
                width = height * (1 / Resolutions[i].Aspect);
            }
            else
            {
                width = height * Resolutions[i].Aspect;
            }
            // Debug.Log(height + "/" + width);
            float posY = height / (float)2;
            float posX = width / (float)2;
            DrawLine(posX, posY);
        }
    }

    void DrawLine(float width, float height)
    {
        Debug.DrawLine(new Vector3(-width, height) + _cam.transform.position, new Vector3(width, height) + _cam.transform.position, Color.blue);
        Debug.DrawLine(new Vector3(width, height) + _cam.transform.position, new Vector3(width, -height) + _cam.transform.position, Color.blue);
        Debug.DrawLine(new Vector3(width, -height) + _cam.transform.position, new Vector3(-width, -height) + _cam.transform.position, Color.blue);
        Debug.DrawLine(new Vector3(-width, -height) + _cam.transform.position, new Vector3(-width, height) + _cam.transform.position, Color.blue);
    }

    [ContextMenu("OrderResolutions")]
    public void OrderResolutions()
    {
        Resolutions.OrderBy(s => s.Aspect);
    }

    void SetCanvasScalers(float value)
    {
        if (_canvasScalers != null && _canvasScalers.Length > 0)
        {
            for (int i = 0; i < _canvasScalers.Length; i++)
            {
                if (_canvasScalers[i])
                    _canvasScalers[i].matchWidthOrHeight = value;
            }
        }
    }

    public void _FixCamSizeFollowScreen() => FixCamSizeFollowScreen();

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            if (GizmosUpdate)
            {
                FixCamSizeFollowScreen();
            }
        if (showDrawLine)
        {
            DrawResolution();
        }
    }
#endif
}

[Serializable]
public class ResolutionInfor
{
    public string Name;
    public float Aspect;
    public float CamSize = 5f;
    public float PerspectiveSize = 60f;
    public float Scaler = 0.5f;
}