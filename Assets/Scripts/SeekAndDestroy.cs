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
    private bool isPaused = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = searchInterval;
        UpdateTarget(); // 初始更新目标
    }

    void Update()
    {
        if (isPaused)
        {
            // 如果Agent处于暂停状态，不执行任何操作
            return;
        }

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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("StrikeBall"))
        {
            StartCoroutine(PauseAgent(5f));
        }
    }

    private IEnumerator PauseAgent(float duration)
    {
        isPaused = true;
        agent.isStopped = true; // 停止NavMeshAgent

        yield return new WaitForSeconds(duration); // 等待指定时间

        isPaused = false;
        agent.isStopped = false; // 重新启动NavMeshAgent

        // 这里可以添加重新开始寻找球的逻辑
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