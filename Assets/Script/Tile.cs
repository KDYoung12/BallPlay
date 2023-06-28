using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Empty = 0, Base, Broke, Boom, Jump, StraightLeft, StraightRight, Blink, LastIndex }

// �÷��̾�� Ÿ���� �浹 ���� (�÷��̾� ����)
public enum CollisionDirection { Up = 0, Down }

public abstract class Tile : MonoBehaviour
{
    /*
    [SerializeField]
    private Sprite[] images; // Ÿ���� ����� �� �ִ� �̹��� �迭
    private SpriteRenderer spriteRenderer; // Ÿ�� �̹��� ������ ���� SpriteRenderer
    private TileType tileType; // ���� Ÿ���� �Ӽ�

    public void Setup(TileType tileType)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        TileType = tileType;
    }

    public TileType TileType
    {
        set
        {
            tileType = value;
            spriteRenderer.sprite = images[(int)tileType - 1];
        }
        get => tileType;
    }
    */

    protected Movement2D movement2D; // Ÿ�ϰ� �ε��� �÷��̾ �����ϱ� ���� Movement2D

    public virtual void Setup(Movement2D movement2D)
    {
        this.movement2D = movement2D;
    }

    public abstract void Collision(CollisionDirection direction);

}
