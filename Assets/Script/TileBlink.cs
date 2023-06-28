using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBlink : Tile
{
    private List<TileBlink> blinkTiles; // 이동 가능한 순간이동 타일 리스트

    public void SetBlinkTiles(List<TileBlink> blinks)
    {
        blinkTiles = new List<TileBlink>();

        // 현재 맵에서 자기 자신을 제외한 모든 순간이동 타일을 등록
        for ( int i = 0; i < blinks.Count; ++i)
        {
            if ( blinks[i] != this)
            {
                blinkTiles.Add(blinks[i]);
            }
        }
    }

    public override void Collision(CollisionDirection direction)
    {
        if ( direction == CollisionDirection.Down)
        {
            // 현재 리스트에 있는 순간이동 타일 중 하나를 선택해서 해당 위치로 이동
            int index = Random.Range(0, blinkTiles.Count);
            movement2D.transform.position = blinkTiles[index].transform.position + Vector3.up;

            movement2D.JumpTo(movement2D.jumpForce);
        }
    }
}
