using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;

    public Image ScreenImage;
    public CanvasGroup canvasGroup;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetFadeIn(TweenCallback callback = null, float time = 1.5f)
    {
        ScreenImage.raycastTarget = true;
        canvasGroup.DOFade(1, 1.5f).OnComplete(callback);
    }

    public void SetFadeOut(TweenCallback callback = null, float time = 1.5f)
    {
        canvasGroup.DOFade(0, 1.5f).OnComplete(callback);
        ScreenImage.raycastTarget = false;
    }
}
