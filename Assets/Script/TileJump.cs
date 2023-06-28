using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileJump : Tile
{
    [SerializeField]
    private float jumpForce;

    public override void Collision(CollisionDirection direction)
    {
        if ( direction == CollisionDirection.Down)
        {
            movement2D.JumpTo(jumpForce);
        }
    }
}
