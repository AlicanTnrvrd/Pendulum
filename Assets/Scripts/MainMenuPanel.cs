using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : Singleton<MainMenuPanel>
{
    [SerializeField] private Transform contents;

    
    public void OnTabButtonClik()
    {
        contents.gameObject.SetActive(false);
        GameManager.Instance.StartGame();
    }



}
