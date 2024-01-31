using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CheckBoxes : MonoBehaviour
{
    public GameObject effectPrefab;
    public GameObject secondEffectPrefab;
    public int ballsInsideCount = 0; // 跟踪碰撞体内球的数量
    private Coroutine generationCoroutine;
    public int firstTimeTrigger = 0;
    private ThrowAction throwAction;
    private int triggerableIncreasedCount = 0; // 新增计数器
    private GameObject secondEffectInstance; // 用于存储生成的第二个效果的实例
    public GameObject SkillEffect;


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
        if (other.CompareTag("Ball") | other.CompareTag("StrikeBall"))
        {
            ballsInsideCount++;
            //triggerable = false;

            GameObject _effectObject = Instantiate(effectPrefab, transform.position + Vector3.up * 0.6f, Quaternion.identity);
            Vector3 scale = transform.localScale;  // 获取当前对象的缩放值
            Vector3 newVector = new Vector3(scale.x, 0.1f, scale.x);
            _effectObject.transform.localScale = newVector;

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
        if ((other.CompareTag("Ball") | other.CompareTag("StrikeBall")) && ballsInsideCount > 0)
        {
            ballsInsideCount--;
            if (ballsInsideCount == 0)
            {
                //triggerable = true;

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
        if (ball && (ball.CompareTag("Ball") | ball.CompareTag("StrikeBall")) && ballsInsideCount > 0)
        {
            ballsInsideCount--;
            Debug.Log("Ball destroyed in: " + gameObject.name + ", Remaining balls: " + ballsInsideCount);

            // 一旦所有球都离开，重置 triggerable 状态
            if (ballsInsideCount == 0 && triggerableIncreasedCount > 0)
            {
                triggerable = true; // 只有在计数器大于0时减少
                triggerableIncreasedCount--; // 减少计数器
                if (generationCoroutine != null)
                {
                    StopCoroutine(generationCoroutine);
                    generationCoroutine = null;
                }
                // 当没有球时销毁第二个效果
                if (secondEffectInstance != null)
                {
                    Destroy(secondEffectInstance);
                    secondEffectInstance = null;
                }
            }
            
            // 这里可以添加其他处理球体销毁的逻辑
            // 比如，如果你想在球体数量变化时做一些特别的处理
        }
    }


    private IEnumerator GenerateSecondEffectAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);

        if (ballsInsideCount > 0)
        {


            secondEffectInstance = Instantiate(secondEffectPrefab, transform.position + Vector3.up * 0.6f, Quaternion.identity); ;

            Vector3 scale = transform.localScale;  // 获取当前对象的缩放值
            Vector3 newVector = new Vector3(scale.x, 0.1f, scale.x);
            secondEffectInstance.transform.localScale = newVector;
            
            
            if (firstTimeTrigger > 0)
            {
                //生成一个加球cd的技能 && 播放一个获得技能的动画
                GameObject skilleffect = Instantiate(SkillEffect, transform.position + Vector3.up * 0.6f, Quaternion.identity);
                skilleffect.transform.localScale = transform.localScale;

                throwAction.GainPowerup();
                firstTimeTrigger--;
            }
            triggerable = false; // 第二个效果触发后增加
            triggerableIncreasedCount++; // 增加计数器

        }

    }

    public void HandleBallExiting()
    {
        
        // 这里可以添加其他处理球体即将离开的逻辑
        if (ballsInsideCount == 0 && triggerableIncreasedCount > 0)
        {
            ballsInsideCount--;
        //    triggerable = true; // 只有在计数器大于0时减少
        //    triggerableIncreasedCount--; // 减少计数器

        }
    }

}