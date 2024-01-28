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
            // ��ȡ�����λ��
            Vector3 mousePosition = Input.mousePosition;

            // ����Ļ����ת��Ϊ��������
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

            // ���㷽������
            Vector3 direction = (worldMousePosition - transform.position).normalized;

            // ������������ĵ�ľ���
            float distance = Vector3.Distance(worldMousePosition, transform.position);

            // ��������
            float force = Mathf.Clamp(distance, 0f, 20f) * multiplier;

            // �������岢ʩ����
            SpawnNewObject(direction, force);
        }
    }

    void SpawnNewObject(Vector3 direction, float force)
    {
        // ��������
        GameObject newObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        Rigidbody rb = newObject.GetComponent<Rigidbody>();

        // ʩ���������Ĵ�С���ݾ��붯̬����
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}

