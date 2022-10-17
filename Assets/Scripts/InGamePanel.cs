using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGamePanel : Singleton<InGamePanel>
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void WriteScore(int score)
    {
        scoreText.text = "Score: "+ score.ToString();

    }

}
