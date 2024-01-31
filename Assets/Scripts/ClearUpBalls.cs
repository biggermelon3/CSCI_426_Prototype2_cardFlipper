using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUpBalls : MonoBehaviour
{
    private AudioSource audioSource; // 用于存储 AudioSource 组件的引用

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 获取当前游戏对象上的 AudioSource 组件
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on the object!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检测碰撞的对象是否有 "Ball" 或 "StrikeBall" 标签
        if (other.CompareTag("Ball") || other.CompareTag("StrikeBall"))
        {
            Destroy(other.gameObject); // 销毁该对象
            // 检查是否存在 AudioSource 组件
            if (audioSource != null)
            {
                audioSource.Play(); // 播放音频
            }
        }
    }
}