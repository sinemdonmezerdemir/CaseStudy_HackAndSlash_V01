using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class MoveCommandFactory
{
    public static MoveCommand CreatePlayerMoveCommand(Transform rb, Vector3 movement, float moveSpeed, float rotationSpeed)
    {
        return new MoveCommand(rb, movement, moveSpeed, rotationSpeed);
    }

    public static AIMoveCommand CreateAIMoveCommand( Transform rigidbody, Transform destination, float speed ,Transform looaAtTarget)
    {
        return new AIMoveCommand(rigidbody, destination,speed,looaAtTarget);
    }
}
