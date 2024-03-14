using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomInput : MonoBehaviour
{
    // bool isClickAction = false;
    static Dictionary<TypeInput, float> inputs = new Dictionary<TypeInput, float>();
    public static List<TypeInput> listInputClick = new List<TypeInput>();
    static public float GetAxis(TypeInput _axis)
    {
        if (!inputs.ContainsKey(_axis))
        {
            inputs.Add(_axis, 0);
        }
        return inputs[_axis];
    }

    static public void SetAxis(TypeInput _axis, float _value)
    {
        if (!inputs.ContainsKey(_axis))
        {
            inputs.Add(_axis, 0);
        }
        inputs[_axis] = _value;
    }

    void LateUpdate()
    {
        if (listInputClick.Count > 0)
        {
            foreach (TypeInput type in listInputClick)
            {
                SetAxis(type, 0);
            }
        }
    }

    public static void ResetInput()
    {
        inputs[TypeInput.LEFT] = 0;
        inputs[TypeInput.RIGHT] = 0;
    }
}
