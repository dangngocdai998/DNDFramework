using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarLookAtCamera : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

#if UNITY_EDITOR
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    // void OnValidate()
    // {
    //     if (!Application.isPlaying)
    //     {
    //         if (Camera.main)
    //             transform.LookAt(transform.position + Camera.main.gameObject.transform.forward);
    //     }
    // }
#endif

    void FixedUpdate()
    {
        transform.LookAt(transform.position + Camera.main.gameObject.transform.forward);
    }
}
