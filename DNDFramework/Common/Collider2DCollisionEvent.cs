using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collider2DCollisionEvent : MonoBehaviour
{
    [Header("Setup")]

    [SerializeField] protected UnityEvent<Collision2D> eventOnCollisionEnter;
    [SerializeField] protected UnityEvent<Collision2D> eventOnCollisionStay;
    [SerializeField] protected UnityEvent<Collision2D> eventOnCollisionExit;

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (eventOnCollisionEnter != null)
        {
            eventOnCollisionEnter.Invoke(other);
        }
    }
    public virtual void OnCollisionExit2D(Collision2D other)
    {
        if (eventOnCollisionExit != null)
        {
            eventOnCollisionExit.Invoke(other);
        }
    }
    public virtual void OnCollisionStay2D(Collision2D other)
    {
        if (eventOnCollisionStay != null)
        {
            eventOnCollisionStay.Invoke(other);
        }
    }
}
