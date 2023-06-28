using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStraight : Tile
{
    [SerializeField]
    private MoveType moveType; // ���� or ������ �̵� ����
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public override void Collision(CollisionDirection direction)
    {
        // �÷��̾��� ��ġ�� ���� Ÿ�� ��ġ���� �̵� �������� 1��ŭ �̵��� ��ġ
        Vector3 position = boxCollider2D.bounds.center + Vector3.right * (int)moveType;

        // �÷��̾ ���� or ������ �̵��ϵ��� �޼ҵ� ȣ��
        movement2D.SetupStraihtMove(moveType, position);
    }
}
