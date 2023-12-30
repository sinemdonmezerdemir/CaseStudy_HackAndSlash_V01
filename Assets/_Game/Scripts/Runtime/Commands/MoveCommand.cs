using UnityEngine;
using UnityEngine.UIElements;

public class MoveCommand : ICommand
{
    Transform _transform;
    Vector3 _movement;
    float _moveSpeed;
    float _rotationSpeed;

    public MoveCommand(Transform transform, Vector3 movement, float moveSpeed, float rotationSpeed)
    {
        _transform = transform;
        _movement = movement;
        _moveSpeed = moveSpeed;
        _rotationSpeed = rotationSpeed;
    }

    public void Execute()
    {
        Vector3 movePos = _transform.position + (_movement * _moveSpeed * Time.deltaTime);
        _transform.Translate(_movement * _moveSpeed * Time.deltaTime, Space.World);

        if (_movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_movement);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}
