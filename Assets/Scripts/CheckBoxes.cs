using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoxes : MonoBehaviour
{
    public GameObject effectPrefab;
    private bool hasGeneratedFirstEffect = false; // 新增变量用于标记是否生成了第一个特效
    private bool ballInside = false; // 标记球是否在触发器内
    private Coroutine generationCoroutine; // 用于存储协程的引用


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            ballInside = true;
            if (generationCoroutine == null)
            {
                // 开始协程
                generationCoroutine = StartCoroutine(GenerateEffectAfterDelay());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            ballInside = false;
            // 如果球离开了，停止协程
            if (generationCoroutine != null)
            {
                StopCoroutine(generationCoroutine);
                generationCoroutine = null;
            }
        }
    }

    private IEnumerator GenerateEffectAfterDelay()
    {
        // 等待3秒
        yield return new WaitForSeconds(3f);

        // 生成特效
        Instantiate(effectPrefab, transform.position, Quaternion.identity);

        // 如果球仍然在触发器内，继续等待3秒生成下一个特效
        if (ballInside)
        {
            generationCoroutine = StartCoroutine(GenerateEffectAfterDelay());
        }
    }

    private void GenerateSecondEffect()
    {
        // Generate the second effectPrefab when a ball stays for 3 seconds
        Instantiate(effectPrefab, transform.position + Vector3.up, Quaternion.identity);

    }
}
