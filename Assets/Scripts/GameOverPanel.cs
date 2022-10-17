using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : Singleton<GameOverPanel>
{
    [SerializeField] private Transform contents;
    [SerializeField] private Button retry;
    [SerializeField] private float openDuration;
    [SerializeField] private float delay;


    void Start()
    {
        contents.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        DOTween.Kill("tween");
    }

    public void Open()
    {
        contents.gameObject.SetActive(true);
        contents.localScale = Vector3.zero;
        retry.enabled = false;
        contents.DOScale(Vector3.one ,openDuration).SetDelay(delay).SetId("tween").OnComplete(() => {
            retry.enabled = true;
        });
    }

    public void Close()
    {
       
        contents.DOScale(Vector3.zero, openDuration).SetId("tween").OnComplete(() =>
        {
            contents.gameObject.SetActive(false);
            GameManager.Instance.ReloadLevel();
        });

    }


}
