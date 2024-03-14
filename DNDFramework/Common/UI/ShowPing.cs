using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowPing : MonoBehaviour
{
    public TextMeshProUGUI txt_Show;
    private int _pingTime = 0;
    public string url = "66.42.63.111";
    string version;
    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.targetFrameRate = 40;
#else
        // Application.targetFrameRate = 60;
#endif

        txt_Show.text = "";
        version = Application.version;
        // CheckPing(url);
        StartCoroutine(FPS());

    }

    public void CheckPing(string ip)
    {
        StartCoroutine(StartPing(ip));
    }

    IEnumerator StartPing(string ip)
    {
        WaitForSeconds f = new WaitForSeconds(0.05f);
        Ping p = new Ping(ip);
        while (p.isDone == false)
        {
            yield return f;
        }
        PingFinished(p);
    }


    public void PingFinished(Ping p)
    {
        Debug.Log("Zô ss");
        txt_Show.text = FramesPerSec + "fps - " + p.time + "ms";
        _pingTime = p.time;
        CheckPing(url);
        // stuff when the Ping p has finshed....
    }
    public int FramesPerSec { get; protected set; }

    [SerializeField] private float frequency = 0.2f;


    private IEnumerator FPS()
    {
        for (; ; )
        {
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);

            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
            txt_Show.text = FramesPerSec + "fps - v" + version;
        }
    }
}
