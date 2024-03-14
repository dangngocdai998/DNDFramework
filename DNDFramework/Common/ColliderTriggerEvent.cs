using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTriggerEvent : MonoBehaviour
{
    [Header("Setup")]

    [SerializeField] protected UnityEvent<Collider> eventOnTriggerEnter;
    [SerializeField] protected UnityEvent<Collider> eventOnTriggerStay;
    [SerializeField] protected UnityEvent<Collider> eventOnTriggerExit;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (eventOnTriggerEnter != null)
        {
            eventOnTriggerEnter.Invoke(other);
        }
    }
    public virtual void OnTriggerExit(Collider other)
    {
        if (eventOnTriggerExit != null)
        {
            eventOnTriggerExit.Invoke(other);
        }
    }
    public virtual void OnTriggerStay(Collider other)
    {
        if (eventOnTriggerStay != null)
        {
            eventOnTriggerStay.Invoke(other);
        }
    }
}
