using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum NamePrefabPool
{
    Ball

}
[System.Serializable]
public enum PrefabPoolType
{
    VFX,
    GAMEOBJECT
}
[System.Serializable]
public class PoolingInfo
{
    public string name;
    public PrefabPoolType prefabType;
    public NamePrefabPool namePrefab;
    public GameObject prefab;
    public int countStartSpawn;
    public int maxPoolChangeScene = 20;
    public int maxPoolOnScene = 70;

}

public class PoolingManager : SingletonMonoBehaviour<PoolingManager>
{
    [Header("VFX POOL OBJECTS")]
    public PoolingInfo[] vfxPoolingInfo;
    // List<GameObject> listVFXPool = new List<GameObject>();

    [Header("GAMEOBJECT POOL")]
    public PoolingInfo[] gameObjectPoolingInfo;
    // List<GameObject> listObjectPool = new List<GameObject>();

    Dictionary<string, List<GameObject>> dicPooling = new Dictionary<string, List<GameObject>>();
    Dictionary<string, List<ItemPooling>> dic1Pooling = new Dictionary<string, List<ItemPooling>>();

    private void Start()
    {
        StartSpawnObject();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (vfxPoolingInfo != null && vfxPoolingInfo.Length > 0)
        {
            foreach (PoolingInfo pl in vfxPoolingInfo)
            {
                pl.name = pl.namePrefab.ToString();
            }
        }

        if (gameObjectPoolingInfo != null && gameObjectPoolingInfo.Length > 0)
        {
            foreach (PoolingInfo pl in gameObjectPoolingInfo)
            {
                pl.name = pl.namePrefab.ToString();
            }
        }
    }
#endif

    public void StartSpawnObject()
    {
        foreach (PoolingInfo pooling in vfxPoolingInfo)
        {
            if (pooling.prefabType == PrefabPoolType.VFX)
            {
                List<GameObject> vfxs = new List<GameObject>();
                for (int i = 0; i < pooling.countStartSpawn; i++)
                {
                    GameObject obj = Instantiate(pooling.prefab, transform);
                    obj.name = pooling.namePrefab.ToString();
                    obj.SetActive(false);
                    vfxs.Add(obj);
                }
                dicPooling.Add(pooling.name + "VFX", vfxs);


            }
        }

        foreach (PoolingInfo pooling in gameObjectPoolingInfo)
        {
            if (pooling.prefabType == PrefabPoolType.GAMEOBJECT)
            {
                List<GameObject> objects = new List<GameObject>();
                for (int i = 0; i < pooling.countStartSpawn; i++)
                {
                    GameObject obj = Instantiate(pooling.prefab, transform);
                    obj.name = pooling.namePrefab.ToString();
                    obj.SetActive(false);
                    objects.Add(obj);
                }
                dicPooling.Add(pooling.name + "Obj", objects);
            }
        }
    }

    public GameObject GetVFX(NamePrefabPool name)
    {
        string nameObj = name + "VFX";
        if (dicPooling[nameObj].Count > 0)
        {
            foreach (GameObject obj in dicPooling[nameObj])
            {
                if (!obj.activeSelf/*  && obj.name == name.ToString() */)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            for (int i = 0; i < vfxPoolingInfo.Length; i++)
            {
                if (name == vfxPoolingInfo[i].namePrefab)
                {
                    GameObject vfx = Instantiate(vfxPoolingInfo[i].prefab, transform);
                    vfx.name = name.ToString();
                    // listVFXPool.Add(vfx);
                    dicPooling[nameObj].Add(vfx);
                    return vfx;
                }
            }
        }
        else
        {
            for (int i = 0; i < vfxPoolingInfo.Length; i++)
            {
                if (name == vfxPoolingInfo[i].namePrefab)
                {
                    GameObject vfx = Instantiate(vfxPoolingInfo[i].prefab, transform);
                    vfx.name = name.ToString();
                    // listVFXPool.Add(vfx);
                    dicPooling[nameObj].Add(vfx);
                    return vfx;
                }
            }
        }

        return null;
    }
    public GameObject GetVFX(NamePrefabPool name, Vector3 position, Transform parent = null, bool insideCanvas = false)
    {
        string nameObj = name + "VFX";
        if (dicPooling[nameObj].Count > 0)
        {
            foreach (GameObject obj in dicPooling[nameObj])
            {
                if (!obj.activeSelf)
                {
                    if (parent == null)
                    {
                        obj.transform.SetParent(transform);
                    }
                    else
                    {
                        obj.transform.SetParent(parent);
                    }
                    if (insideCanvas)
                    {
                        obj.GetComponent<RectTransform>().anchoredPosition = position;
                        obj.transform.localScale = Vector3.one;
                    }
                    else
                        obj.transform.position = position;
                    obj.SetActive(true);
                    return obj;
                }
            }

            for (int i = 0; i < vfxPoolingInfo.Length; i++)
            {
                if (name == vfxPoolingInfo[i].namePrefab)
                {
                    GameObject vfx = Instantiate(vfxPoolingInfo[i].prefab, transform);
                    vfx.name = name.ToString();
                    if (parent == null)
                    {
                        vfx.transform.SetParent(transform);
                    }
                    else
                    {
                        vfx.transform.SetParent(parent);
                    }
                    if (insideCanvas)
                    {
                        vfx.GetComponent<RectTransform>().anchoredPosition = position;
                        vfx.transform.localScale = Vector3.one;
                    }
                    else
                        vfx.transform.position = position;
                    dicPooling[nameObj].Add(vfx);
                    return vfx;
                }
            }
        }
        else
        {
            for (int i = 0; i < vfxPoolingInfo.Length; i++)
            {
                if (name == vfxPoolingInfo[i].namePrefab)
                {
                    GameObject vfx = Instantiate(vfxPoolingInfo[i].prefab, transform);
                    vfx.name = name.ToString();
                    if (parent == null)
                    {
                        vfx.transform.SetParent(transform);
                    }
                    else
                    {
                        vfx.transform.SetParent(parent);
                    }

                    if (insideCanvas)
                    {
                        vfx.GetComponent<RectTransform>().anchoredPosition = position;
                        vfx.transform.localScale = Vector3.one;
                    }
                    else
                        vfx.transform.position = position;
                    dicPooling[nameObj].Add(vfx);
                    return vfx;
                }
            }
        }

        return null;
    }
    public GameObject GetObject(NamePrefabPool name, Vector3 position, Transform parent = null)
    {
        string nameObj = name + "Obj";
        if (dicPooling[nameObj].Count > 0)
        {
            foreach (GameObject obj in dicPooling[nameObj])
            {
                if (!obj.activeSelf && obj.name == name.ToString())
                {
                    if (parent == null)
                    {
                        obj.transform.SetParent(transform);
                    }
                    else
                    {
                        obj.transform.SetParent(parent);
                    }
                    obj.transform.position = position;
                    obj.SetActive(true);
                    return obj;
                }
            }

            for (int i = 0; i < gameObjectPoolingInfo.Length; i++)
            {
                if (name == gameObjectPoolingInfo[i].namePrefab)
                {
                    GameObject vfx = Instantiate(gameObjectPoolingInfo[i].prefab, transform);
                    vfx.name = name.ToString();
                    if (parent == null)
                    {
                        vfx.transform.SetParent(transform);
                    }
                    else
                    {
                        vfx.transform.SetParent(parent);
                    }

                    vfx.transform.position = position;
                    dicPooling[nameObj].Add(vfx);
                    return vfx;
                }
            }
        }
        else
        {
            for (int i = 0; i < gameObjectPoolingInfo.Length; i++)
            {
                if (name == gameObjectPoolingInfo[i].namePrefab)
                {
                    GameObject vfx = Instantiate(gameObjectPoolingInfo[i].prefab, transform);
                    vfx.name = name.ToString();
                    if (parent == null)
                    {
                        vfx.transform.SetParent(transform);
                    }
                    else
                    {
                        vfx.transform.SetParent(parent);
                    }
                    vfx.transform.position = position;
                    dicPooling[nameObj].Add(vfx);
                    return vfx;
                }
            }
        }

        return null;
    }
    public GameObject GetObjectPrefab(GameObject objSpaw, Vector3 position, Transform parent = null)
    {
        string nameObj = objSpaw.name + "_ObjPrefab";
        if (dicPooling.ContainsKey(nameObj) && dicPooling[nameObj].Count > 0)
        {
            foreach (GameObject obj in dicPooling[nameObj])
            {
                if (!obj.activeSelf)
                {
                    // Debug.Log(obj.name + " // " + obj.activeSelf + " // " + obj.gameObject.activeSelf);
                    if (parent == null)
                    {
                        obj.transform.SetParent(transform);
                    }
                    else
                    {
                        obj.transform.SetParent(parent);
                    }
                    obj.transform.position = position;
                    obj.SetActive(true);
                    return obj;
                }
            }
        }
        GameObject vfx = Instantiate(objSpaw, transform);
        vfx.name = nameObj;
        if (parent == null)
        {
            vfx.transform.SetParent(transform);
        }
        else
        {
            vfx.transform.SetParent(parent);
        }
        vfx.transform.position = position;
        if (dicPooling.ContainsKey(nameObj))
        {
            dicPooling[nameObj].Add(vfx);
        }
        else
        {
            dicPooling.Add(nameObj, new List<GameObject> { vfx });
        }

        return vfx;
    }
    /* public GameObject GetObjectPrefab(GameObject objSpaw, Vector3 position, Transform parent = null)
    {
        string nameObj = objSpaw.name + "_ObjPrefab";
        if (dic1Pooling.ContainsKey(nameObj) && dic1Pooling[nameObj].Count > 0)
        {
            List<ItemPooling> listObject = dic1Pooling[nameObj];
            for (int i = 0; i < listObject.Count; i++)
            {
                if (!listObject[i].getStatusActive)
                {
                    Debug.Log(listObject[i].name + " // " + listObject[i].getStatusActive);
                    if (parent == null)
                    {
                        listObject[i].transform.SetParent(transform);
                    }
                    else
                    {
                        listObject[i].transform.SetParent(parent);
                    }
                    listObject[i].transform.position = position;
                    listObject[i].SetActiveObj(true);
                    dic1Pooling[nameObj] = listObject;
                    return listObject[i].gameObject;
                }
            }
        }
        GameObject vfx = Instantiate(objSpaw, transform);
        vfx.name = nameObj;
        if (parent == null)
        {
            vfx.transform.SetParent(transform);
        }
        else
        {
            vfx.transform.SetParent(parent);
        }
        vfx.transform.position = position;
        ItemPooling item = vfx.GetComponent<ItemPooling>();
        item.SetActiveObj(true);
        if (dic1Pooling.ContainsKey(nameObj))
        {
            dic1Pooling[nameObj].Add(item);
        }
        else
        {
            dic1Pooling.Add(nameObj, new List<ItemPooling> { item });
        }

        return vfx;
    } */

    public GameObject GetObject(NamePrefabPool name)
    {
        string nameObj = name + "Obj";
        if (dicPooling[nameObj].Count > 0)
        {
            foreach (GameObject obj in dicPooling[nameObj])
            {
                if (!obj.activeSelf && obj.name == name.ToString())
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            for (int i = 0; i < gameObjectPoolingInfo.Length; i++)
            {
                if (name == gameObjectPoolingInfo[i].namePrefab)
                {
                    GameObject vfx = Instantiate(gameObjectPoolingInfo[i].prefab, transform);
                    vfx.name = name.ToString();
                    dicPooling[nameObj].Add(vfx);
                    return vfx;
                }
            }
        }
        else
        {
            for (int i = 0; i < gameObjectPoolingInfo.Length; i++)
            {
                if (name == gameObjectPoolingInfo[i].namePrefab)
                {
                    GameObject vfx = Instantiate(gameObjectPoolingInfo[i].prefab, transform);
                    vfx.name = name.ToString();
                    dicPooling[nameObj].Add(vfx);
                    return vfx;
                }
            }
        }

        return null;
    }
    public void DisableAllObject()
    {
        StopAllCoroutines();

        foreach (string key in dicPooling.Keys)
        {
            for (int i = dicPooling[key].Count - 1; i >= 0; i--)
            {
                if (dicPooling[key][i] == null)
                {
                    dicPooling[key].RemoveAt(i);
                    // obj.transform.SetParent(transform);
                }
                else
                {
                    dicPooling[key][i].SetActive(false);
                    dicPooling[key][i].transform.SetParent(transform);

                }
            }
        }
        /* foreach (string key in dic1Pooling.Keys)
        {
            for (int i = dic1Pooling[key].Count - 1; i >= 0; i--)
            {
                if (dic1Pooling[key][i] == null)
                {
                    dic1Pooling[key].RemoveAt(i);
                    // obj.transform.SetParent(transform);
                }
                else
                {
                    dic1Pooling[key][i].SetActiveObj(false);
                    dic1Pooling[key][i].transform.SetParent(transform);

                }
            } */
        // }
    }

    public void DisableObjectWithTime(GameObject obj, float time)
    {
        StartCoroutine(TimeDisableObject(obj, time));
    }

    public void DisableObjPooling(GameObject obj)
    {
        /* if (obj.GetComponent<ItemPooling>())
        {
            obj.GetComponent<ItemPooling>().SetActiveObj(false);
        }
        else
        { */
        obj.SetActive(false);
        // }
    }

    IEnumerator TimeDisableObject(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
        obj.transform.SetParent(transform);
    }
}
