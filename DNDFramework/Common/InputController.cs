using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController
{
    private Ray _ray;
    private Plane _xy;
    private Camera _cam;
    float _distance;

    // private void Start()
    // {
    //     Init();
    // }
    public InputController()
    {
        Init();
    }
    public void Init()
    {
        _cam = Camera.main;
        _xy = new Plane(Vector3.up, new Vector3(0, 0, 0));
    }



    public Vector3 GetMousePositionOnPlane(int indexMouse = 0)
    {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
                return GetWorldPositionOnPlane(Input.mousePosition);

#endif
#if UNITY_EDITOR|| UNITY_WEBGL
        return GetWorldPositionOnPlane(Input.mousePosition);
#else

        if (Input.touchCount >= indexMouse + 1)
        {
            return GetWorldPositionOnPlane(Input.touches[indexMouse].position);
        }

        return new Vector3(-100, 100, 100);
#endif
    }
    public Vector3 GetMousePositionOnTarget(LayerMask layerMask, int indexMouse = 0)
    {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
                return GetWorldPositionOnTarget(Input.mousePosition,layerMask);

#endif
#if UNITY_EDITOR|| UNITY_WEBGL
        return GetWorldPositionOnTarget(Input.mousePosition, layerMask);
#else
        if (Input.touchCount >= indexMouse + 1)
        {
            return GetWorldPositionOnTarget(Input.touches[indexMouse].position,layerMask);
        }

        return new Vector3(-100, 100, 100);
#endif
    }

    public RaycastHit GetMouseRayCastHitOnTarget(LayerMask layerMask, int indexMouse = 0)
    {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
                return GetWorldRaycastHitOnTarget(Input.mousePosition,layerMask);

#endif
#if UNITY_EDITOR|| UNITY_WEBGL
        return GetWorldRaycastHitOnTarget(Input.mousePosition, layerMask);
#else
        if (Input.touchCount >= indexMouse + 1)
        {
            return GetWorldRaycastHitOnTarget(Input.touches[indexMouse].position,layerMask);
        }

        return new RaycastHit();
#endif
    }

    public Vector3 MousePostion(int indexMouse = 0)
    {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
                return Input.mousePosition;

#endif
#if UNITY_EDITOR|| UNITY_WEBGL
        return Input.mousePosition;
#else

        return Input.touches[indexMouse].position;
#endif

    }

    Vector3 lastPosition;
    public Vector3 MouseDeltaPostion(int indexMouse = 0)
    {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        Vector3 currentPosition = Input.mousePosition;
        Vector3 deltaPosition = currentPosition - lastPosition;
        lastPosition = currentPosition;
        return deltaPosition;
#endif
#if UNITY_EDITOR || UNITY_WEBGL

        Vector3 currentPosition = Input.mousePosition;
        Vector3 deltaPosition = currentPosition - lastPosition;
        lastPosition = currentPosition;
        return deltaPosition;
#else

        return Input.touches[indexMouse].deltaPosition;
#endif


    }


    // public bool IsMousePress

    public int TouchCount
    {
        get
        {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
                                        if (Input.GetMouseButton(0))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    lastPosition = Input.mousePosition;
                }
                return 1;
            }
            return 0;

#endif
#if UNITY_EDITOR || UNITY_WEBGL
            if (Input.GetMouseButton(0))
            {
                if (Input.GetMouseButtonDown(0))
                {

                    lastPosition = Input.mousePosition;
                }
                return 1;
            }
            return 0;
#else

            // if (Input.touchCount >= indexMouse + 1)
            // {
            return Input.touchCount;
            // }

            // return 1;
#endif
        }
    }

    public int IndexMouseTarget
    {
        get
        {
            // Debug.Log("Input touch: " + Input.touchCount);
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        return 0;
#endif
#if UNITY_EDITOR|| UNITY_WEBGL
            return 0;

#else

// Debug.Log("Input touch: "+Input.touchCount);
if(Input.touchCount > 0)
        return Input.touchCount-1;
        return 0;
#endif
        }
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition)
    {
        if (_cam == null) _cam = Camera.main;
        _ray = _cam.ScreenPointToRay(screenPosition);
        _xy.Raycast(_ray, out _distance);
        return _ray.GetPoint(_distance);
    }
    public Vector3 GetWorldPositionOnTarget(Vector3 screenPosition, LayerMask mask)
    {
        if (_cam == null) _cam = Camera.main;
        _ray = _cam.ScreenPointToRay(screenPosition);
        Physics.Raycast(_ray, out RaycastHit raycastHit, 100, mask);
        return raycastHit.point;
    }
    public RaycastHit GetWorldRaycastHitOnTarget(Vector3 screenPosition, LayerMask mask)
    {
        if (_cam == null) _cam = Camera.main;
        _ray = _cam.ScreenPointToRay(screenPosition);
        Physics.Raycast(_ray, out RaycastHit raycastHit, 100, mask);
        return raycastHit;
    }
    public static bool IsMouseDown()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return false;
                return true;
            }

            return false;
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            return true;

        return false;
    }

    public static bool IsMouseDown2()
    {
        if (Input.GetMouseButtonDown(0))
            return true;

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            return true;

        if (Input.GetMouseButtonDown(0))
            return true;

        return false;
    }

    public static bool IsMouseDown(out int idTouch)
    {
        idTouch = -1;
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(Input.touchCount - 1);
            // Input.GetTouch();
            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return false;
                idTouch = touch.fingerId;
                return true;
            }
            return false;
        }
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            return true;

        return false;
    }
    public static bool IsMouseUp(int _idTouch)
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[_idTouch].phase == TouchPhase.Ended)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                    return false;
                return true;
            }

            return false;
        }

        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            return true;

        return false;
    }
    public static bool IsMouseUp()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                    return false;
                return true;
            }

            return false;
        }

        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            return true;

        return false;
    }

    public static bool IsMouseUp2()
    {
        if (Input.GetMouseButtonUp(0))
            return true;

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                return true;
            }

            return false;
        }

        if (Input.GetMouseButtonUp(0))
            return true;

        return false;
    }

    public static bool IsMousePress()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Stationary)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                    return false;
                return true;
            }

            return false;
        }

        if (Input.GetMouseButton(0))
            return true;

        return false;
    }

    public static bool IsMousePress2()
    {
        if (Input.GetMouseButton(0))
            return true;

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Stationary)
            {
                return true;
            }

            return false;
        }

        if (Input.GetMouseButton(0))
            return true;

        return false;
    }

    public Vector3 PlanePostionDelta(Plane plane)
    {
        if (!IsMousePress())
            return Vector3.zero;
        var rayBefore = _cam.ScreenPointToRay(MousePostion() - MouseDeltaPostion());
        var rayNow = _cam.ScreenPointToRay(MousePostion());
        if (plane.Raycast(rayBefore, out var enterBefore) && plane.Raycast(rayNow, out var enterNow))
        {
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);
        }
        return Vector3.zero;
    }
}