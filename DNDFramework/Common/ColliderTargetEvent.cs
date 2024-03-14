using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTargetEvent : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] protected UnityEvent eventOnMouseDown;
    void OnMouseDown()
    {
        Debug.Log("Dai: zoo MouseDown");
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {

            eventOnMouseDown.Invoke();
        }
    }
}
