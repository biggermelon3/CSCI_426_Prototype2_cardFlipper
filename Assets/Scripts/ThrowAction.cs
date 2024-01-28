using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAction : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float multiplier = 20f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标点击位置
            Vector3 mousePosition = Input.mousePosition;

            // 将屏幕坐标转换为世界坐标
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

            // 计算方向向量
            Vector3 direction = (worldMousePosition - transform.position).normalized;

            // 计算鼠标与中心点的距离
            float distance = Vector3.Distance(worldMousePosition, transform.position);

            // 计算力量
            float force = Mathf.Clamp(distance, 0f, 20f) * multiplier;

            // 生成物体并施加力
            SpawnNewObject(direction, force);
        }
    }

    void SpawnNewObject(Vector3 direction, float force)
    {
        // 创建物体
        GameObject newObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        Rigidbody rb = newObject.GetComponent<Rigidbody>();

        // 施加力，力的大小根据距离动态调整
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}

