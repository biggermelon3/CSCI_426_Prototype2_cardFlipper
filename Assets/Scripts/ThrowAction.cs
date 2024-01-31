using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ThrowAction : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float multiplier = 20f;

    public int maxBalls = 5;  // 最大球数
    private List<float> cooldownTimers;  // 存储每个球的冷却计时器
    public float cooldownDuration = 2.0f;  // 每个球的冷却时间
    public List<Image> ballUIElements;  // 存储对应球的CD UI元素

    AudioSource audioData;



    private void Start()
    {
        cooldownTimers = new List<float>() { 0 };
        // 初始时隐藏所有球的UI
        foreach (var uiElement in ballUIElements)
        {
            uiElement.gameObject.SetActive(false);
        }
        ballUIElements[0].gameObject.SetActive(true);

        audioData = GetComponent<AudioSource>();
    }

    public void UseSkill()
    {
        for (int i = 0; i < cooldownTimers.Count; i++)
        {
            if (cooldownTimers[i] <= 0)  // 检查是否有球不在冷却中
            {
                // 使用技能（扔球）
                ThrowBall(i);  // 实现球的扔出逻辑
                audioData.Play(0);
                cooldownTimers[i] = cooldownDuration;  // 重置冷却计时器
                break;  // 只扔一个球
            }
        }
    }

    private void ThrowBall(int ballIndex)
    {
        // 实现扔球的逻辑
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

    public void GainPowerup()
    {
        
        // 启用一个新的球的UI元素
        int activeBalls = Mathf.Min(cooldownTimers.Count, maxBalls);
        if (activeBalls < ballUIElements.Count)
        {
            ballUIElements[activeBalls].gameObject.SetActive(true);
            cooldownTimers.Add(0);
        }
    }


private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseSkill();
        }
        for (int i = 0; i < cooldownTimers.Count; i++)
        {
            if (cooldownTimers[i] > 0)
            {
                cooldownTimers[i] -= Time.deltaTime;
                // 假设每个球对象的子对象上有一个名为"CooldownImage"的Image组件
                Image cooldownImage = ballUIElements[i].transform.Find("Image").GetComponent<Image>();
                cooldownImage.fillAmount = cooldownTimers[i] / cooldownDuration;
            }
                
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GainPowerup();
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

