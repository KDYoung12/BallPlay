using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType { Left = -1, UpDown = 0, Right = 1 }

public class Movement2D : MonoBehaviour
{
    [Header("Raycast Collision")]
    [SerializeField]
    private LayerMask collisionLayer; // ������ �ε����� �浹 ���̾�

    [Header("Raycast")]
    [SerializeField]
    private int horizontalRayCount = 4; // x�� �������� �߻�Ǵ� ������ ����
    [SerializeField]
    private int verticalRayCount = 4; // y�� �������� �߻�Ǵ� ������ ����

    private float horizontalRaySpacing; // x�� ������ ���� ���� ����
    private float verticalRaySpacing; // y�� ������ ���� ���� ����

    [Header("Movement")]
    [SerializeField]
    private float moveSpeed; // �̵� �ӵ�
    
    public float jumpForce = 10; // ���� ��
    private float gravity = -20.0f; // �߷�

    private Vector3 velocity; // ������Ʈ �ӷ�
    private readonly float skinWidth = 0.015f; // ������Ʈ �������� �İ��� �ҷ��� ����

    private Collider2D collider2D; // ���� �߻� ��ġ ������ ���� �浹 ����
    private ColliderCorner colliderCorner; // ���� �߻縦 ���� �𼭸� ��
    private CollisionChecker collisionChecker; // 4���� �浹 ���� üũ

    public CollisionChecker IsCollision => collisionChecker;
    public Transform HitTransform { private set; get; } // �÷��̾�� �ε��� ������Ʈ ����

    public MoveType MoveType { private set; get; } // �̵� ��Ŀ� ���� ������ ����

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();

        MoveType = MoveType.UpDown; // �⺻������ ��/�Ʒ� �̵��� �Ѵ�
    }

    private void Update()
    {
        CalculateRaySpacing(); // ���� ������ �Ÿ� ����
        UpdateColliderCorner(); // �浹 ������ �ܰ� ��ġ ����
        collisionChecker.Reset(); // �浹 ���� �ʱ�ȭ (��/�Ʒ�/��/��)

        // �̵� ������Ʈ
        UpdateMovement();

        // õ���̳� �ٴڿ� ������ velocity.y ���� 0���� ����
        if ( collisionChecker.up || collisionChecker.down)
        {
            velocity.y = 0;
        }

        // JumpTo();
    }

    private void UpdateMovement()
    {
        // ��/�Ʒ� �̵��϶��� ������ ���� �߷��� ����
        if ( MoveType == MoveType.UpDown)
        {
            // �߷� ����
            velocity.y += gravity * Time.deltaTime;
        }
        // ���� or ������ �̵��� ���� ��/�� �̵�
        else
        {
            velocity.x = (int)MoveType * moveSpeed;
        }

        // ���� �����ӿ� ����� ���� �ӷ�
        Vector3 currentVelocity = velocity * Time.deltaTime;

        // �ӷ��� 0�� �ƴ� �� ������ �߻��� �̵� ���� ���� ����
        if ( currentVelocity.x != 0 )
        {
            RaycastHorizontal(ref currentVelocity);
        }
        if( currentVelocity.y != 0)
        {
            RaycastVertical(ref currentVelocity);
        }

        // ������Ʈ �̵�
        transform.position += currentVelocity;
    }

    public void MoveTo(float x)
    {
        // ���� or ������ �̵� ������ �� ��/�� ����Ű�� ������
        if ( x != 0 && MoveType != MoveType.UpDown)
        {
            // MoveType�� ��/�Ʒ� �̵����� ����
            MoveType = MoveType.UpDown;
        }

        // x�� ���� �ӷ��� x * moveSpeed�� ����
        velocity.x = x * moveSpeed;
    }

    public void JumpTo(float jumpForce = 0)
    {
        // �Ű������� 0�� �ƴ� ���� �����ϸ� �Ű����� ����ŭ velocity.y ����
        if ( jumpForce != 0 )
        {
            // y�� �ӷ��� jumpForce�� ������ ����
            velocity.y = jumpForce;
            return;
        }
    }

    public void SetupStraihtMove(MoveType moveType, Vector3 position)
    {
        // �÷��̾��� �̵� ���� ����
        MoveType = moveType;
        // ���� �÷��̾��� ��ġ�� ���� Ÿ���� ��ġ�� ����
        transform.position = position;
        // y�� �ӷ��� �ʱ�ȭ
        velocity.y = 0;
    }
    
    private void RaycastHorizontal(ref Vector3 velocity)
    {
        float direction = Mathf.Sign(velocity.x); // �̵� ���� (�� : 1, �� : -1)
        float distance = Mathf.Abs(velocity.x) + skinWidth; // ���� ����
        Vector2 rayPosition = Vector2.zero; // ������ �߻�Ǵ� ��ġ
        RaycastHit2D hit;

        for ( int i = 0; i < horizontalRayCount; ++i)
        {
            rayPosition = (direction == 1) ? colliderCorner.bottomRight : colliderCorner.bottomLeft;
            rayPosition += Vector2.up * (horizontalRaySpacing * i);

            hit = Physics2D.Raycast(rayPosition, Vector2.right * direction, distance, collisionLayer);

            if ( hit )
            {
                // x�� �ӷ��� ������ ������Ʈ ������ �Ÿ��� ���� (�Ÿ��� 0�̸� �ӷ� 0)
                velocity.x = (hit.distance - skinWidth) * direction;
                // ������ �߻�Ǵ� ������ �Ÿ� ����
                distance = hit.distance;

                // ���� �������, �ε��� ������ ������ true�� ����
                collisionChecker.left = direction == -1;
                collisionChecker.right = direction == 1;
            }

            // Debug : �߻�Ǵ� ������ Scene View���� Ȯ��
            Debug.DrawLine(rayPosition, rayPosition + Vector2.right * direction * distance, Color.yellow);
        }
    }

    private void RaycastVertical(ref Vector3 velocity)
    {
        float direction = Mathf.Sign(velocity.y); // �̵� ���� (�� : 1, �Ʒ� : -1)
        float distance = Mathf.Abs(velocity.y) + skinWidth; // ���� ����
        Vector2 rayPosition = Vector2.zero; // ������ �߻�Ǵ� ��ġ
        RaycastHit2D hit;

        for (int i = 0; i < verticalRayCount; ++i)
        {
            rayPosition = (direction == 1) ? colliderCorner.topLeft : colliderCorner.bottomLeft;
            rayPosition += Vector2.right * (verticalRaySpacing * i + velocity.x);

            hit = Physics2D.Raycast(rayPosition, Vector2.right * direction, distance, collisionLayer);

            if (hit)
            {
                // y�� �ӷ��� ������ ������Ʈ ������ �Ÿ��� ���� (�Ÿ��� 0�̸� �ӷ� 0)
                velocity.y = (hit.distance - skinWidth) * direction;
                // ������ �߻�Ǵ� ������ �Ÿ� ����
                distance = hit.distance;

                // ���� �������, �ε��� ������ ������ true�� ����
                collisionChecker.down = direction == -1;
                collisionChecker.up = direction == 1;

                // �ε��� ������Ʈ�� Transform ����
                HitTransform = hit.transform;
            }

            // Debug : �߻�Ǵ� ������ Scene View���� Ȯ��
            Debug.DrawLine(rayPosition, rayPosition + Vector2.up * direction * distance, Color.yellow);
        }
    }

    private void CalculateRaySpacing()
    {
        Bounds bounds = collider2D.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    private void UpdateColliderCorner()
    {
        // ���� ������Ʈ�� ��ġ �������� Collider�� ������ �޾ƿ�
        Bounds bounds = collider2D.bounds;
        bounds.Expand(skinWidth * -2);

        colliderCorner.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        colliderCorner.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        colliderCorner.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
    }

    private struct ColliderCorner
    {
        public Vector2 topLeft;
        public Vector2 bottomLeft;
        public Vector2 bottomRight;
    }

    public struct CollisionChecker
    {
        public bool up;
        public bool down;
        public bool left;
        public bool right;

        public void Reset()
        {
            up = false;
            down = false;
            left = false;
            right = false;
        }
    }

    private void OnDrawGizmos()
    {
        // �׷����� ������ ����
        Gizmos.color = Color.blue;
        // ��, �쿡 ǥ�õǴ� ���� �߻� ��ġ
        for ( int i = 0; i > horizontalRayCount; ++i)
        {
            Vector2 position = Vector2.up * horizontalRaySpacing * i;
            // �� ������ ������ �׸� (��ġ, ������)
            Gizmos.DrawSphere(colliderCorner.bottomRight + position, 0.1f);
            Gizmos.DrawSphere(colliderCorner.bottomLeft + position, 0.1f);
        }

        // ��, �Ʒ��� ǥ�õǴ� ���� �߻� ��ġ
        for ( int i = 0; i < verticalRayCount; ++i)
        {
            Vector2 position = Vector2.right * verticalRaySpacing * i;
            // �� ������ ������ �׸� (��ġ, ������)
            Gizmos.DrawSphere(colliderCorner.topLeft + position, 0.1f);
            Gizmos.DrawSphere(colliderCorner.bottomLeft + position, 0.1f);
        }
    }

}
