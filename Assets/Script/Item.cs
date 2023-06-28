using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� �Ӽ� (����)
public enum ItemType { Coin = 10 }

public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject itemEffectPrefabs; // ������ ȹ�� ����Ʈ ������

    public void Exit()
    {
        // �������� ����� �� ������ ȹ�� ����Ʈ ����
        Instantiate(itemEffectPrefabs, transform.position, Quaternion.identity);

        // ������ ������Ʈ ����
        Destroy(gameObject);
    }
}
