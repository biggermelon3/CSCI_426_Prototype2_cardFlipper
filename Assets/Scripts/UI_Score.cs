using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    public TextMeshProUGUI countText; // 引用 TMP 文本组件
    public GameObject winMenu;

    private void Update()
    {
        // 更新 TMP 文本组件以显示计数
        countText.text = GridGenerator.secondEffectCount + " / " + GridGenerator.TargetSecondEffectCount;
        if(GridGenerator.secondEffectCount/GridGenerator.TargetSecondEffectCount == 1)
        {
            winMenu.SetActive(true);
        }

    }
}
