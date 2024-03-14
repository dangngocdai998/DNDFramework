using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ButtonAction))]
public class InputKeyboard : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] KeyCode keyCode;
    [SerializeField] ButtonAction buttonAction;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!buttonAction)
        {
            buttonAction = GetComponent<ButtonAction>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            buttonAction.OnTargetDown();
        }
        if (Input.GetKeyUp(keyCode))
        {
            buttonAction.OnTargetUp();
        }
    }
#endif
}
