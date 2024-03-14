using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPooling : MonoBehaviour
{
    [ReadOnly][SerializeField] bool _activeObj = false;
    public bool getStatusActive => _activeObj;
    public void SetActiveObj(bool value)
    {
        _activeObj = value;
        gameObject.SetActive(value);
    }
}
