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
            EndMenu.winState = true;
        }
    }

    void GenerateGrid()
    {
        Vector3 cellSize = new Vector3(gridSize.x / columns, gridSize.y, gridSize.z / rows);
        Vector3 startPosition = -gridSize / 2 + cellSize / 2;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = startPosition + new Vector3(col * cellSize.x, 0, row * cellSize.z);
                GameObject gridCell = Instantiate(gridCellPrefab, position, Quaternion.identity, parentTransform);
                gridCell.transform.localScale = cellSize;
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