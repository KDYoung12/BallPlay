using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBroke : Tile
{
    [SerializeField]
    private GameObject tileBrokeEffect;

    public override void Collision(CollisionDirection direction)
    {
        // Ÿ���� �μ����� ȿ���� ����ϴ� ��ƼŬ ������ ����
        Instantiate(tileBrokeEffect, transform.position, Quaternion.identity);

        // �÷��̾��� �Ʒ��ʰ� Ƽ���� �ε����� �÷��̾� ����
        if ( direction == CollisionDirection.Down)
        {
            movement2D.JumpTo(movement2D.jumpForce);
        }

        // Ÿ�� ����
        Destroy(gameObject);
    }
}
