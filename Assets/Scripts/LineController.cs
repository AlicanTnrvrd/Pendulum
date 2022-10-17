using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : Singleton<LineController>
{
    [SerializeField] private LineRenderer lR;
    
    public void DrawLine(Transform ballPos, Transform holdPoint)
    {
        lR.enabled = true;
        lR.SetPosition(0, ballPos.position);
        lR.SetPosition(1, holdPoint.position);

    }

    public void DisableLine()
    {
        lR.enabled = false;
    }
}
