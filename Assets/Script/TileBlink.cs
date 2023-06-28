using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBlink : Tile
{
    private List<TileBlink> blinkTiles; // �̵� ������ �����̵� Ÿ�� ����Ʈ

    public void SetBlinkTiles(List<TileBlink> blinks)
    {
        blinkTiles = new List<TileBlink>();

        // ���� �ʿ��� �ڱ� �ڽ��� ������ ��� �����̵� Ÿ���� ���
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
            // ���� ����Ʈ�� �ִ� �����̵� Ÿ�� �� �ϳ��� �����ؼ� �ش� ��ġ�� �̵�
            int index = Random.Range(0, blinkTiles.Count);
            movement2D.transform.position = blinkTiles[index].transform.position + Vector3.up;

            movement2D.JumpTo(movement2D.jumpForce);
        }
    }
}
