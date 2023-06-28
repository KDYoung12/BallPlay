using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBroke : Tile
{
    [SerializeField]
    private GameObject tileBrokeEffect;

    public override void Collision(CollisionDirection direction)
    {
        // 타일이 부서지는 효과를 재생하는 파티클 프리펩 생성
        Instantiate(tileBrokeEffect, transform.position, Quaternion.identity);

        // 플레이어의 아래쪽과 티일이 부딪히면 플레이어 점프
        if ( direction == CollisionDirection.Down)
        {
            movement2D.JumpTo(movement2D.jumpForce);
        }

        // 타일 삭제
        Destroy(gameObject);
    }
}
