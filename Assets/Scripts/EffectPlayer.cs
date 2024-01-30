using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    public Material effectMaterial; // 特效使用的材质
    public float effectDuration = 2.0f; // 特效持续时间
    public string debugSaySomething;

    private void Start()
    {
        // 如果需要，这里可以初始化材质的属性

        // 开始播放特效
        StartCoroutine(PlayEffect());
    }

    private IEnumerator PlayEffect()
    {
        Debug.Log(debugSaySomething);
        // 假设特效是通过改变材质的某个属性实现的
        // 这里添加特效播放的逻辑

        // 等待特效播放完毕
        yield return new WaitForSeconds(effectDuration);

        // 销毁物体
        Destroy(gameObject);
    }
}
