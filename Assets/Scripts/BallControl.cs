using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public CheckBoxes currentCheckBox;

    private void OnDestroy()
    {
        if (currentCheckBox != null)
        {
            currentCheckBox.HandleBallDestruction(gameObject);
        }
    }
    public void SetCurrentCheckBox(CheckBoxes newCheckBox)
    {
        if (currentCheckBox != null)
        {
            currentCheckBox.HandleBallExiting(); // 通知旧的 CheckBoxes 实例球体即将离开
        }
        currentCheckBox = newCheckBox;
    }
    void Start()
    {
        // 设置初始标签为StrikeBall
        gameObject.tag = "StrikeBall";
    }

    void OnCollisionEnter(Collision collision)
    {
        // 检查球是否撞击到地面
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 改变标签为Ball
            gameObject.tag = "Ball";
        }
    }
}
