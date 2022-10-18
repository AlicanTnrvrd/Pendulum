using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,GamePlay,Win,GameOver
}
public class BallController : Singleton<BallController>
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private Transform startTarget;
    private PlayerState state;

    private bool isMovingRight = true;
    private bool isTouching;
    private Transform target;
    private int score = 0;

    private void Start()
    {
        InGamePanel.Instance.WriteScore(score);
    }

    private void Update()
    {
        
        if (state== PlayerState.Idle )
        {
            LineController.Instance.DrawLine(transform, startTarget);

            if (isMovingRight)
            {
                IdlePendulumMovment(true);
            }

            if (!isMovingRight )
            {
                IdlePendulumMovment(false); 
            }

            if (transform.position.x >= 3f )
            {
                isMovingRight = false;

            }

            if (transform.position.x <= -3f)
            {
                isMovingRight = true;
            }
        }


        if ( state == PlayerState.GamePlay)
        {
            if (Input.GetMouseButtonDown(0) )
            {
                target = BlockManager.Instance.GetNearestBlock();
                isTouching = true;
                rb.useGravity = false;

            }

            if (Input.GetMouseButtonUp(0) )
            {
                isTouching = false;
                rb.useGravity = true;
                LineController.Instance.DisableLine();
            }
        }
        
        if (state == PlayerState.GamePlay && isTouching)
        {
            Turn();
            LineController.Instance.DrawLine(transform, target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            state = PlayerState.GameOver;
            GameManager.Instance.GameOver();
            isTouching=false;
            rb.isKinematic = true;
            Debug.Log("çarptým");
        }

        if (other.CompareTag("Donut"))
        {
            IncreaseScore( other);
        }
    }

    private void Turn()
    {
        if (target == null) return;
        
        var direction = (target.position - transform.position).normalized;
        var forceDir = Vector3.Cross(Vector3.back, direction).normalized;

        rb.velocity = turnSpeed*forceDir;

    }

    //private void OnDrawGizmos()
    //{
    //    if(target == null) return;
    //    var direction = (target.transform.position - transform.position);
    //    Gizmos.DrawRay(transform.position, direction);
    //}
    private void IdlePendulumMovment(bool isMoveToRight)
    {
        target = startTarget;

        var idleVec = Vector3.back;

        if (!isMoveToRight)
        {
            idleVec = Vector3.forward;
        }

        var direction = (target.position - transform.position).normalized;
        var forceDir = Vector3.Cross(idleVec, direction).normalized;
        rb.velocity = turnSpeed * forceDir * 0.3f;

    }
    
    public void SetTarget(Transform obstacleHoldPoint)
    {
        target = obstacleHoldPoint;
    }

    public void StartGame()
    {
        state = PlayerState.GamePlay;

    }

    public void IncreaseScore(Collider other) 
    {
        score += 5;
        var blockController = other.attachedRigidbody.GetComponent<BlockController>();
        blockController.SetDonut(false);
        InGamePanel.Instance.WriteScore(score);
    }
}

