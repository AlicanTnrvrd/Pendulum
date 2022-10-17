using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedPanel : Singleton<LevelCompletedPanel>
{
    [SerializeField] private Transform contents;

    void Start()
    {
       contents.gameObject.SetActive(false);

    }


   
}
