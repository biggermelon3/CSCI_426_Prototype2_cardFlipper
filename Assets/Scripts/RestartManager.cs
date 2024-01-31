using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    void Update()
    {
        // 检测是否按下了 "R" 键
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 获取当前场景的名称并重新载入该场景
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
