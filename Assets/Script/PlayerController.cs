using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Tilemap2D tilemap2D;
    private Movement2D movement2D;

    private float deathLimitY; // 플레이어가 사망하는 바닥 Y 위치

    public void Setup(Vector2Int position, int mapSizeY)
    {
        movement2D = GetComponent<Movement2D>();

        transform.position = new Vector3(position.x, position.y, 0);

        deathLimitY = -mapSizeY / 2;
    }

    private void Update()
    {
        if ( transform.position.y <= deathLimitY)
        {
            // Debug.Log("플레이어 사망");
            // 플레이어가 낭떠러지로 떨어지면 현재 씬 다시 로드
            SceneLoader.LoadScene();
        }

        UpdateMove();
        UpdateCollision(); 
    }

    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");

        // 플레이어 캐릭터의 몰리적 이동
        movement2D.MoveTo(x);
    }

    private void UpdateCollision()
    {
        // 플레이어의 위쪽 방향에 충돌이 감지되면
        if ( movement2D.IsCollision.up)
        {
            CollisionToTile(CollisionDirection.Up);
        }
        // 플레이어 아래쪽 방향에 충돌이 감지되면
        else if ( movement2D.IsCollision.down)
        {
            CollisionToTile(CollisionDirection.Down);
        }
    }

    private void CollisionToTile(CollisionDirection direction)
    {
        Tile tile = movement2D.HitTransform.GetComponent<Tile>();
        if ( tile != null)
        {
            // 플레이어에게 부딪힌 타일의 속성에 따라 충돌 처리
            tile.Collision(direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어에게 부딪힌 오브젝트의 태그가 Item이면
        if (collision.tag.Equals("Item"))
        {
            // 부딪힌 오브젝트 삭제
            // Destroy(collision.gameObject);

            // Tilemap2D의 GetCoin() 메소드 호출 (매개변수는 부딪힌 오브젝트 정보)
            tilemap2D.GetCoin(collision.gameObject);
        }
    }
}
