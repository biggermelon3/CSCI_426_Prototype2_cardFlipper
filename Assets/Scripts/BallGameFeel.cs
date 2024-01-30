using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGameFeel : MonoBehaviour
{
    public ParticleSystem collisionEffect; // 碰撞时播放的特效
    public AudioClip collisionSound; // 碰撞时播放的声音
    private AudioSource audioSource; // 音频源组件

    private void Start()
    {
        // 获取或添加 AudioSource 组件
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 播放特效
        if (collisionEffect != null)
        {
            Instantiate(collisionEffect, transform.position, Quaternion.identity);
        }

        // 播放声音
        if (collisionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collisionSound);
        }
    }
}
