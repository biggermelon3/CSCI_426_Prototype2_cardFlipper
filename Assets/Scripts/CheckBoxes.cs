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

            BallControl ballScript = other.GetComponent<BallControl>();
            if (ballScript != null)
            {
                ballScript.SetCurrentCheckBox(this);
            }

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

    public void HandleBallDestruction(GameObject ball)
    {
        if (ball && ball.CompareTag("Ball"))
        {
            ballsInsideCount--;
            Debug.Log("Ball destroyed in: " + gameObject.name + ", Remaining balls: " + ballsInsideCount);

            // 一旦所有球都离开，重置 triggerable 状态
            if (ballsInsideCount == 0)
            {
                triggerable = true;

                // 如果有正在进行的延迟生成效果协程，停止它
                if (generationCoroutine != null)
                {
                    StopCoroutine(generationCoroutine);
                    generationCoroutine = null;
                }
            }

            // 这里可以添加其他处理球体销毁的逻辑
            // 比如，如果你想在球体数量变化时做一些特别的处理
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

    public void HandleBallExiting()
    {
        ballsInsideCount--;
        // 这里可以添加其他处理球体即将离开的逻辑
    }

}