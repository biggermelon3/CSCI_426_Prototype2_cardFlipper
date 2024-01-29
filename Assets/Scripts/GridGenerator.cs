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

    void Start()
    {
        GenerateGrid();
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