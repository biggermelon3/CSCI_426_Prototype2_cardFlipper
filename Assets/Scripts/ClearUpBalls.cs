using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUpBalls : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 检测碰撞的对象是否有 "Ball" 或 "StrikeBall" 标签
        if (other.CompareTag("Ball") || other.CompareTag("StrikeBall"))
        {
            Destroy(other.gameObject); // 销毁该对象
        }
    }
}