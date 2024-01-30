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
}
