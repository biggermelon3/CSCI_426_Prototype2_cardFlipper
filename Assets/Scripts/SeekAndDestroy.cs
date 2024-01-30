using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SeekAndDestroy : MonoBehaviour
{
    public float searchInterval = 1.0f; // 搜索间隔时间
    private NavMeshAgent agent;
    private float timer;
    private GameObject target; // 当前目标

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = searchInterval;
        UpdateTarget(); // 初始更新目标
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= searchInterval)
        {
            timer = 0f;
            UpdateTarget();
        }

        if (!agent.pathPending && target != null)
        {
            // 计算忽略y坐标的距离
            Vector3 agentPosition = new Vector3(agent.transform.position.x, 0, agent.transform.position.z);
            Vector3 targetPosition = new Vector3(target.transform.position.x, 0, target.transform.position.z);
            float distanceToTarget = Vector3.Distance(agentPosition, targetPosition);

            if (distanceToTarget <= 0.5f) // 到达距离的阈值
            {
                Destroy(target); // 销毁球体
                Debug.Log("Destroying ball: " + target.name);
                UpdateTarget(); // 更新目标
            }
        }
    }

    void UpdateTarget()
    {
        target = FindClosestBall();
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    GameObject FindClosestBall()
    {
        GameObject[] balls;
        balls = GameObject.FindGameObjectsWithTag("Ball");
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject ball in balls)
        {
            Vector3 directionToBall = ball.transform.position - currentPosition;
            directionToBall.y = 0; // 忽略高度差异
            float distance = directionToBall.magnitude;

            if (distance < minDistance)
            {
                closest = ball;
                minDistance = distance;
            }
        }

        return closest;
    }
}