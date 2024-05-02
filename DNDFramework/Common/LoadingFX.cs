using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingFX : SingletonMonoBehaviour<LoadingFX>
{
    // [SerializeField]Animator animator;
    [SerializeField] GameObject obj_Loading;

    const string eventLoading = "Loading";

    bool loaded = false;
    // public bool GetLoading => loaded;
    public bool Loading
    {
        get => loaded;
        set
        {
            if (value)
            {
                if (!loaded)
                {
                    StartFade();
                    this.PostEvent(eventLoading, true);
                }
            }
            else
            {
                EndFade();
                this.PostEvent(eventLoading, false);
            }
            loaded = value;
        }
    }


    void StartFade()
    {
        // animator.Play("LoadingOpen", -1, 0f);
        obj_Loading.SetActive(true);
    }

    void EndFade()
    {
        // animator.Play("LoadingClose", -1, 0f);
        obj_Loading.SetActive(false);
    }
}
