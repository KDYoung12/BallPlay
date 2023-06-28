using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStraight : Tile
{
    [SerializeField]
    private MoveType moveType; // 왼쪽 or 오른쪽 이동 방향
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public override void Collision(CollisionDirection direction)
    {
        // 플레이어의 위치는 현재 타일 위치에서 이동 방향으로 1만큼 이동한 위치
        Vector3 position = boxCollider2D.bounds.center + Vector3.right * (int)moveType;

        // 플레이어가 왼쪽 or 오른쪽 이동하도록 메소드 호출
        movement2D.SetupStraihtMove(moveType, position);
    }
}
