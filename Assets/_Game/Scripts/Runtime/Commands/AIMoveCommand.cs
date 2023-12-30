using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AIMoveCommand : ICommand
{
    Transform _destination;
    float _speed;
    Transform _transform;
    public AIMoveCommand(Transform transform, Transform destination, float speed, Transform lookAtTarget = null)
    {
        _transform = transform;
        _destination = destination;
        _speed = speed;
    }

    public void Execute()
    {
        Vector3 targetCorner = new Vector3(_destination.position.x, 0, _destination.position.z);
        Vector3 direction = (targetCorner - new Vector3(_transform.position.x, 0, _transform.position.z)).normalized;
        Vector3 movePos =_transform.position + (direction * _speed * Time.deltaTime);
        Vector3 lookPos = direction;

        _transform.transform.Translate(direction * _speed * Time.deltaTime, Space.World);


        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookPos);

            _transform.transform.rotation = Quaternion.Slerp(_transform.transform.rotation, targetRotation, Time.deltaTime * 10);
        }
    }
}
