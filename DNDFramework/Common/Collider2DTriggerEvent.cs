using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collider2DTriggerEvent : MonoBehaviour
{
    [Header("Setup")]

    [SerializeField] protected UnityEvent<Collider2D> eventOnTriggerEnter;
    [SerializeField] protected UnityEvent<Collider2D> eventOnTriggerStay;
    [SerializeField] protected UnityEvent<Collider2D> eventOnTriggerExit;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (eventOnTriggerEnter != null)
        {
            eventOnTriggerEnter.Invoke(other);
        }
    }
    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (eventOnTriggerExit != null)
        {
            eventOnTriggerExit.Invoke(other);
        }
    }
    public virtual void OnTriggerStay2D(Collider2D other)
    {
        if (eventOnTriggerStay != null)
        {
            eventOnTriggerStay.Invoke(other);
        }
    }
}
