using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Transform donut;

    
    public Transform HoldPoint => holdPoint;

    private void Update()
    {
        if (BallController.Instance.transform.position.x - transform.position.x > 10 )
        {
            BlockManager.Instance.BackToPool(this); 
        }
    }

    public void SetDonut(bool isActive )
    {
        
       donut.gameObject.SetActive(isActive);
       
    }
}
