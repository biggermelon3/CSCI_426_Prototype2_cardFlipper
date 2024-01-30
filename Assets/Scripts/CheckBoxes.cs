using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CheckBoxes : MonoBehaviour
{
    public GameObject effectPrefab;
    public GameObject secondEffectPrefab;
    private int ballsInsideCount = 0; // 跟踪碰撞体内球的数量
    private Coroutine generationCoroutine;
    public int firstTimeTrigger = 0;
    private ThrowAction throwAction;

    private bool _triggerable = true;
    private bool triggerable
    {
        get => _triggerable;
        set
        {
            if (_triggerable != value)
            {
                _triggerable = value;
                if (_triggerable)
                {
                    GridGenerator.MinusOneFilledGridCount();
                }
                else
                {
                    GridGenerator.PlusOneFilledGridCount();
                }
            }
        }
    }

    private void Awake()
    {
        throwAction = FindObjectOfType<ThrowAction>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ballsInsideCount++;
            triggerable = false;

            GameObject _effectObject = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            _effectObject.transform.localScale = transform.localScale;

            if (generationCoroutine == null)
            {
                generationCoroutine = StartCoroutine(GenerateSecondEffectAfterDelay());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ballsInsideCount--;
            if (ballsInsideCount == 0)
            {
                triggerable = true;

                if (generationCoroutine != null)
                {
                    StopCoroutine(generationCoroutine);
                    generationCoroutine = null;
                }
            }
        }
    }

    private IEnumerator GenerateSecondEffectAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        if (ballsInsideCount > 0)
        {
            Instantiate(secondEffectPrefab, transform.position + Vector3.up, Quaternion.identity);
            if (firstTimeTrigger > 0)
            {
                //生成一个加球cd的技能 && 播放一个获得技能的动画
                throwAction.GainPowerup();
                firstTimeTrigger--;
            }

        }
    }
}