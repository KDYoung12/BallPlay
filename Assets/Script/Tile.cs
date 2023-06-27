using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Empty = 0, Base, Broke, Boom, Jump, StraightLeft, StraightRight, Blink, LastIndex }

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Sprite[] images; // 타일이 적용될 수 있는 이미지 배열
    private SpriteRenderer spriteRenderer; // 타일 이미지 변경을 위한 SpriteRenderer
    private TileType tileType; // 현재 타일의 속성

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
}
