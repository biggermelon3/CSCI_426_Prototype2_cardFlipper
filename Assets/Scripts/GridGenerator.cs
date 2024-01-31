using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject gridCellPrefab;
    public Transform parentTransform; // ĸ��
    public Vector3 gridSize = new Vector3(20f, 1f, 30f);
    public int rows = 6;
    public int columns = 9;

    private List<Transform> gridCells = new List<Transform>();

    public static int secondEffectCount = 0; // ���ڸ������ɵڶ�����Ч�ĸ�������
    public static int TargetSecondEffectCount = 10; // ����Ŀ�����ɵڶ�����Ч�ĸ�������
    public int maxFirstTimeTriggers = 10;
    public int currentFirstTimeTriggers = 0;
    public List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        GenerateGrid();
        TargetSecondEffectCount = rows * columns;
    }

    private void Update()
    {
        // Check if the player has reached the target secondEffectCount
        if (secondEffectCount >= TargetSecondEffectCount)
        {
            Debug.Log("Player has passed the level!");
            // ��������Դ������صĲ���
        }
    }

    void GenerateGrid()
    {
        float spacing = 0.47f; // 每个单元格之间的间距
        Vector3 cellVisualSize = new Vector3(gridSize.x / columns, gridSize.y, gridSize.z / rows); // 物体的视觉大小
        Vector3 cellColliderSize = new Vector3((gridSize.x - (columns - 1) * spacing) / columns, gridSize.y, (gridSize.z - (rows - 1) * spacing) / rows); // Collider的大小
        Vector3 startPosition = -gridSize / 2 + cellVisualSize / 2;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = startPosition + new Vector3(col * cellVisualSize.x, 0, row * cellVisualSize.z);
                GameObject gridCell = Instantiate(gridCellPrefab, position, Quaternion.identity, parentTransform);
                gridCell.transform.localScale = cellVisualSize; // 设置物体的视觉大小

                // 计算并应用 Collider 的比例调整
                BoxCollider collider = gridCell.GetComponent<BoxCollider>();
                if (collider != null)
                {
                    Vector3 colliderScaleRatio = new Vector3(cellColliderSize.x / cellVisualSize.x, 1, cellColliderSize.z / cellVisualSize.z);
                    collider.size = colliderScaleRatio;
                }

                gridCells.Add(gridCell.transform);
                // 获取或添加 SpawnedObject 脚本并设置 firstTimeTrigger
                CheckBoxes spawnedScript = gridCell.GetComponent<CheckBoxes>();
                if (spawnedScript == null)
                {
                    spawnedScript = gridCell.AddComponent<CheckBoxes>();
                }

                // 随机决定是否设置 firstTimeTrigger 为 1
                if (currentFirstTimeTriggers < maxFirstTimeTriggers && Random.Range(0, 4) == 1)
                {
                    spawnedScript.firstTimeTrigger = 1;
                    currentFirstTimeTriggers++;
                    // 添加到列表
                    spawnedObjects.Add(gridCell);
                }
                else
                {
                    spawnedScript.firstTimeTrigger = 0;
                }

            }
            
        }
        // 如果已生成对象超过最大数量，确保只有五个 firstTimeTrigger 为 1
        if (spawnedObjects.Count > maxFirstTimeTriggers)
        {
            AdjustFirstTimeTriggers();
        }


    }

    // 调整 firstTimeTrigger 确保只有五个为 1
    private void AdjustFirstTimeTriggers()
    {
        while (currentFirstTimeTriggers > maxFirstTimeTriggers)
        {
            // 从列表中随机选择一个对象并更改其 firstTimeTrigger
            int indexToChange = Random.Range(0, spawnedObjects.Count);
            CheckBoxes spawnedScript = spawnedObjects[indexToChange].GetComponent<CheckBoxes>();
            if (spawnedScript.firstTimeTrigger == 1)
            {
                spawnedScript.firstTimeTrigger = 0;
                currentFirstTimeTriggers--;
            }
        }
    }

    public static void PlusOneFilledGridCount()
    {
        secondEffectCount++; // ���ӵڶ�����Ч���ӵ�����
    }
    public static void MinusOneFilledGridCount()
    {
        secondEffectCount--; // ���ӵڶ�����Ч���ӵ�����
    }
}