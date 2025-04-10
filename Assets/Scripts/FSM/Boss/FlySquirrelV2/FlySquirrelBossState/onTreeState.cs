using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTreeState : EnemyState
{
    /*二阶段，树上阶段：
    在三棵树上来回跳跃，跳到一棵树上后，随即往主角位置丢一颗爆炸松果。
    主角需踩上弹跳叶片跳到boss位置，把boss打到地面上
    该阶段boss不会扣血，受击只会变为地面状态，持续时间一直到被打为止
    */
    public FlySquirrelBOSS fsb;
    private List<Transform> nearbyPlatforms = new List<Transform>();
    private List<Transform> allnearbyPlatforms = new List<Transform>();
    private Transform currentPlatform;
    private bool isJumping = false;
    private float jumpProgress = 0f;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float jumpHeight;

    private Coroutine jumpRoutine;
    public onTreeState(Enemy enemy) : base(enemy)
    {
        fsb=enemy as FlySquirrelBOSS;
    }

    public override void EnterState()
    {
        FindNearbyPlatforms();
        JumpBetweenRandomPlatforms();
        
        // 开始随机跳跃协程
        jumpRoutine = fsb.StartCoroutine(RandomJumpRoutine());
        fsb.rb.gravityScale = 1f;

        //启用与平台的碰撞
        foreach (Transform platform in allnearbyPlatforms)
        {
            Physics2D.IgnoreCollision(fsb.GetComponent<Collider2D>(), platform.GetComponent<Collider2D>(), false);
        }
        

    }

    public override void ExitState()
    {
        fsb.StopCoroutine(jumpRoutine);

        // 禁用与平台的碰撞
        foreach (Transform platform in allnearbyPlatforms)
        {
            Physics2D.IgnoreCollision(fsb.GetComponent<Collider2D>(), platform.GetComponent<Collider2D>(), true);
        }

    }
    
    public override void FrameUpdate()
    {
        TreePlatformJumpAndAttack();

        if(fsb.nbvm.isHit == true)
        {
            fsb.stateMachine.ChangeState(fsb.onGroundState);
        }
        

    }

    public override void PhysicsUpdate()
    {
        
    }

    #region 功能函数

    void TreePlatformJumpAndAttack()
    {
        if (isJumping)
        {
            // 更新跳跃进度
            jumpProgress += Time.deltaTime / fsb.jumpDuration;
            
            if (jumpProgress < 1f)
            {
                // 计算抛物线跳跃的当前位置
                float parabola = 1f - Mathf.Pow(2f * jumpProgress - 1f, 2f);
                Vector2 newPosition = Vector2.Lerp(startPosition, targetPosition, jumpProgress);
                newPosition.y += parabola * jumpHeight;
                
                fsb.transform.position = newPosition;
            }
            else
            {
                // 跳跃完成
                fsb.transform.position = targetPosition;
                fsb.BoomAcornAttack();
                isJumping = false;
            }
        }
        
    }
    
        // 查找附近的平台
    void FindNearbyPlatforms()
    {
        nearbyPlatforms.Clear();
        
        // 查找所有带有"treeplatform"标签的物体
        GameObject[] allPlatforms = GameObject.FindGameObjectsWithTag("TreePlatform");
        
        foreach (GameObject platform in allPlatforms)
        {
            // 检查是否在检测半径内
            if (Vector2.Distance(fsb.transform.position, platform.transform.position) <= fsb.platformeDetectionRadius)
            {
                nearbyPlatforms.Add(platform.transform);
            }
        }
        allnearbyPlatforms = nearbyPlatforms;
        if (currentPlatform == null ||nearbyPlatforms.Contains(currentPlatform))
        {
            nearbyPlatforms.Remove(currentPlatform);
        }
    }
    void JumpBetweenRandomPlatforms()
    {
        FindNearbyPlatforms();
        if (nearbyPlatforms.Count < 2)
        {
            Debug.LogWarning("附近没有足够的平台进行跳跃");
            FindNearbyPlatforms(); // 重新查找附近的平台
            return;
        }
        
        // 随机选择两个不同的平台
        int index1 = Random.Range(0, nearbyPlatforms.Count);
        
        int index2;
        
        do {
            index2 = Random.Range(0, nearbyPlatforms.Count);
        } while (index2 == index1);
        
        Transform platform1 = nearbyPlatforms[index1];
        Transform platform2 = nearbyPlatforms[index2];
        
        // 决定从哪个平台跳到哪个平台
        //bool startFromCurrent = Random.value > 0.5f;
        bool startFromCurrent = true;
        
        if (startFromCurrent)
        {
            startPosition = fsb.transform.position;
            Vector2 targetPos = new Vector2 (platform2.position.x , platform2.position.y + 2);//调高目标位置，避免卡在平台内
            targetPosition = targetPos;
            currentPlatform = platform2;
        }
        else
        {
            startPosition = platform1.position;
            Vector2 targetPos = new Vector2 (platform2.position.x , platform2.position.y + 2);//调高目标位置，避免卡在平台内
            targetPosition = targetPos;
        }
        
        // 随机跳跃高度
        jumpHeight = Random.Range(fsb.minJumpHeight, fsb.maxJumpHeight);
        
        // 开始跳跃
        jumpProgress = 0f;
        isJumping = true;
    }

    // 随机跳跃协程
    private System.Collections.IEnumerator RandomJumpRoutine()
    {
        while (true)
        {
            if (!isJumping)
            {
                // 等待随机时间后跳跃
                float waitTime = Random.Range(fsb.minWaitTime, fsb.maxWaitTime);
                yield return new WaitForSeconds(waitTime);
                
                // 执行跳跃
                JumpBetweenRandomPlatforms();
            }
            yield return null;
        }
    }

    // 在编辑器中绘制检测范围
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(fsb.transform.position, fsb.platformeDetectionRadius);
    }
    #endregion
}
