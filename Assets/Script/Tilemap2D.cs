using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap2D : MonoBehaviour
{
    [Header("Common")]
    [SerializeField]
    private StageController stageController;
    [SerializeField]
    private StageUI stageUI;

    [Header("Tile")]
    [SerializeField]
    //private GameObject tilePrefab;
    private GameObject[] tilePrefabs; // Ÿ���� �Ӽ��� ���� ������ ���� ���� / �迭�� ����
    [SerializeField]
    private Movement2D movement2D; // �÷��̾�� Ÿ���� �ε����� �� �÷��̾� ��� ���� Movement2D

    [Header("Item")]
    [SerializeField]
    private GameObject itemPrefab;

    private int maxCoinCount = 0; // ���� ���������� �����ϴ� �ִ� ���� ����
    private int currentCoinCount = 0; // ���� ���������� �����ϴ� ���� ���� ����

    private List<TileBlink> blinkTiles; // ���� ���������� �����ϴ� �����̵� Ÿ�� ����Ʈ

    public void GenerateTilemap(MapData mapData)
    {
        blinkTiles = new List<TileBlink>();

        int width = mapData.mapSize.x;
        int height = mapData.mapSize.y;

        for ( int y = 0; y < height; ++y)
        {
            for ( int x = 0; x < width; ++x)
            {
                // ���� ���·� ��ġ�� Ÿ�ϵ��� ���� ��ܺ��� ���������� ��ȣ�� �ο�
                // 0, 1, 2, 3, 4, 5,
                // 6, 7, ...
                int index = y * width + x;

                // Ÿ���� �Ӽ��� "Empty"�̸� �ƹ��͵� �������� �ʰ� ����д�
                if ( mapData.mapData[index] == (int)TileType.Empty)
                {
                    continue;
                }

                // �����Ǵ� Ÿ�ϸ��� �߾��� (0, 0, 0)�� ��ġ
                Vector3 position = new Vector3(-(width * 0.5f - 0.5f) + x, (height * 0.5f - 0.5f) - y);

                // ���� index�� �� ������ TileType.Empty(0)���� ũ��, TileType.LastIndex(8)���� ������
                if ( mapData.mapData[index] > (int)TileType.Empty && mapData.mapData[index] < (int)TileType.LastIndex)
                {
                    // Ÿ�� ����
                    SpawnTile((TileType)mapData.mapData[index], position);
                }
                // ���� index�� �� ������ ItemType.Coin(10)�̸�
                else if ( mapData.mapData[index] == (int)ItemType.Coin)
                {
                    // ������ ����
                    SpawnItem(position);
                }
            }
        }

        currentCoinCount = maxCoinCount;

        // ���� ������ ������ �ٲ� ������ UI ��� ���� ����
        stageUI.UpdateCoinCount(currentCoinCount, maxCoinCount);

        // �����̵� Ÿ���� �ٸ� �����̵� Ÿ�Ϸ� �̵��� �� �ֱ� ������
        // �ʿ� ��ġ�Ǿ� �ִ� ��� �����̵� Ÿ���� ������ ������ �־���Ѵ�
        foreach ( TileBlink tile in blinkTiles)
        {
            tile.SetBlinkTiles(blinkTiles);
        }
    }

    private void SpawnTile(TileType tileType, Vector3 position)
    {
        //GameObject clone = Instantiate(tilePrefabs, position, Quaternion.identity);

        GameObject clone = Instantiate(tilePrefabs[(int)tileType - 1], position, Quaternion.identity);

        clone.name = "Tile"; // Tile ������Ʈ�� �̸��� "Tile"�� ����
        clone.transform.SetParent(transform); // Tilemap2D ������Ʈ�� Tile ������Ʈ�� �θ�� ����

        Tile tile = clone.GetComponent<Tile>(); // ��� ������ Ÿ��(clone) ������Ʈ�� Tile.Setup() �޼ҵ� ȣ��
        // tile.Setup(tileType);
        tile.Setup(movement2D);

        if ( tileType == TileType.Blink)
        {
            // ���� �ʿ� �����ϴ� �����̵� Ÿ�ϸ� ���� ����Ʈ�� ����
            blinkTiles.Add(clone.GetComponent<TileBlink>());
        }
    }

    private void SpawnItem(Vector3 position)
    {
        GameObject clone = Instantiate(itemPrefab, position, Quaternion.identity);

        clone.name = "Item"; // Item ������Ʈ�� �̸��� "Item"���� ����
        clone.transform.SetParent(transform); // Tilemap2D ������Ʈ�� Item ������Ʈ�� �θ�� ����

        // ���� �������� ���� �ۿ� ���� ������ ������ �������� ���� = ���� ����
        maxCoinCount++;
    }

    public void GetCoin(GameObject coin)
    {
        currentCoinCount--; // ���� ���� ���� -1

        // ���� ������ ������ �ٲ� ������ UI ��� ���� ����
        stageUI.UpdateCoinCount(currentCoinCount, maxCoinCount);

        // ���� �������� ����� �� ȣ���ϴ� Item.Exit() �޼ҵ� ȣ��
        coin.GetComponent<Item>().Exit();

        // ���� ���������� ���� ������ 0�̸�
        if ( currentCoinCount == 0)
        {
            // ���� Ŭ����
            stageController.GameClear();
        }
    }
}
