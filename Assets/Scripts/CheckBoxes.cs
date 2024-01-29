using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoxes : MonoBehaviour
{
    public GameObject effectPrefab;
    private bool hasGeneratedFirstEffect = false; // �����������ڱ���Ƿ������˵�һ����Ч
    private bool ballInside = false; // ������Ƿ��ڴ�������
    private Coroutine generationCoroutine; // ���ڴ洢Э�̵�����


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            ballInside = true;
            if (generationCoroutine == null)
            {
                // ��ʼЭ��
                generationCoroutine = StartCoroutine(GenerateEffectAfterDelay());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            ballInside = false;
            // ������뿪�ˣ�ֹͣЭ��
            if (generationCoroutine != null)
            {
                StopCoroutine(generationCoroutine);
                generationCoroutine = null;
            }
        }
    }

    private IEnumerator GenerateEffectAfterDelay()
    {
        // �ȴ�3��
        yield return new WaitForSeconds(3f);

        // ������Ч
        Instantiate(effectPrefab, transform.position, Quaternion.identity);

        // �������Ȼ�ڴ������ڣ������ȴ�3��������һ����Ч
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
