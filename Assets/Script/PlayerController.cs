using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Tilemap2D tilemap2D;
    private Movement2D movement2D;

    private float deathLimitY; // �÷��̾ ����ϴ� �ٴ� Y ��ġ

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
            // Debug.Log("�÷��̾� ���");
            // �÷��̾ ���������� �������� ���� �� �ٽ� �ε�
            SceneLoader.LoadScene();
        }

        UpdateMove();
        UpdateCollision(); 
    }

    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");

        // �÷��̾� ĳ������ ������ �̵�
        movement2D.MoveTo(x);
    }

    private void UpdateCollision()
    {
        // �÷��̾��� ���� ���⿡ �浹�� �����Ǹ�
        if ( movement2D.IsCollision.up)
        {
            CollisionToTile(CollisionDirection.Up);
        }
        // �÷��̾� �Ʒ��� ���⿡ �浹�� �����Ǹ�
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
            // �÷��̾�� �ε��� Ÿ���� �Ӽ��� ���� �浹 ó��
            tile.Collision(direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �ε��� ������Ʈ�� �±װ� Item�̸�
        if (collision.tag.Equals("Item"))
        {
            // �ε��� ������Ʈ ����
            // Destroy(collision.gameObject);

            // Tilemap2D�� GetCoin() �޼ҵ� ȣ�� (�Ű������� �ε��� ������Ʈ ����)
            tilemap2D.GetCoin(collision.gameObject);
        }
    }
}
