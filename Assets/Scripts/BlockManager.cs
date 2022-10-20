using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;



public class BlockManager : Singleton<BlockManager>
{
    private BallController ballController => BallController.Instance;
    
    private List<BlockController> pooledObjects = new List<BlockController>();
    private List<BlockController> blocks = new List<BlockController>();
    
    private bool canBlockCheckable = true;
    
    [SerializeField] private List<BlockController> sceneBlocks;
    [SerializeField] private BlockController blockPrefab;
    [SerializeField] private int poolSize;
    [SerializeField] private Transform lastBlockPoint;
    [SerializeField] private float vecY;
    private int blockCounter;

    private void Awake()
    {
       
        for (int i = 0; i < poolSize; i++)
        {
            CreateBlock();
        }

        foreach (var item in sceneBlocks)
        {
            blocks.Add(item);
        }
        
    }
    private void Update()
    {
        if (canBlockCheckable)
        {
            CheckBlockSpawn();
        }
    }

    private void CheckBlockSpawn()
    {
        if (lastBlockPoint.position.x - ballController.transform.position.x < 20f)
        {
             BlocksPoolStartPointCalculated();
        }
    }

    private void CreateBlock()
    {
        var obj = Instantiate(blockPrefab);
        obj.gameObject.SetActive(false);
        pooledObjects.Add(obj);
    }

    public BlockController GetPooledObject() 
    {
        if (pooledObjects.Count<=0 )
        {
            CreateBlock();
        }
        

        var obj = pooledObjects[0];
        obj.gameObject.SetActive(true);
        blocks.Add(obj);
        pooledObjects.Remove(obj);
        blockCounter++; 
        if (blockCounter % 10 == 0 && blockCounter != 0)
        {
            obj.SetDonut(true);
            blockCounter = 0;
        }
        return obj;
       
    }

     public void BlocksPoolStartPointCalculated() 
    {
        canBlockCheckable = false;
        Vector3 startedPoint = lastBlockPoint.transform.position;
        float pointX = startedPoint.x;
        

        for (int i = 0; i < 5; i++)
        {
            
            var block = GetPooledObject();
            block.transform.position = lastBlockPoint.transform.position;
            pointX += 2.8f ;
            vecY = Random.Range(10f,15f);
            lastBlockPoint.transform.position += Vector3.right * 2.8f;
            var blockPos = lastBlockPoint.transform.position;
            blockPos.y = vecY;
            lastBlockPoint.transform.position = blockPos;

           

        }

        canBlockCheckable = true;
    }

    public Transform GetNearestBlock()
    {
        
        blocks = blocks.OrderBy(item1 => Vector3.Distance(item1.transform.position, Vector3.zero)).ToList();
        var nearestBlock = blocks.FirstOrDefault(item2 => (item2.transform.position.x - ballController.transform.position.x) > 3);
        return nearestBlock.HoldPoint;
    }


    public void BackToPool(BlockController block) 
    {
        block.gameObject.SetActive(false);
        pooledObjects.Add(block);
        block.SetDonut(false);
        

        
    }


    


  
    
}
