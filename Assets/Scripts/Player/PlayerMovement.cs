using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2ChannelSO OnMoveChannel;
    [SerializeField] private float speed;
    [FormerlySerializedAs("playerSize")] [SerializeField]
    private float radius = 0.7f;
    private Coroutine movement;
    [SerializeField] private float height;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        OnMoveChannel.Subscribe(Move);
    }

    private void OnDisable()
    {
        OnMoveChannel.Unsubscribe(Move);
    }

    private void Move(Vector2 dir)
    {
        Debug.Log(dir);
        if (movement != null)
        {
            StopCoroutine(movement);
        }

        movement = StartCoroutine(Movement(dir));
    }

    private IEnumerator Movement(Vector2 dir)
    {
        while (dir != Vector2.zero)
        {
            Vector3 moveDir = new Vector3(dir.x, 0, dir.y);
            float time = Time.deltaTime;
            
            bool canMove = CanMove(dir, time, ref moveDir);
            
            if (canMove)
            {
                transform.position += moveDir * time * speed;
            }

            yield return null;
        }
    }

    private bool CanMove(Vector2 dir, float time, ref Vector3 moveDir)
    {
        bool canMove = !IsColliding(moveDir, time);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(dir.x, 0, 0);
            canMove = !IsColliding(moveDirX, time);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirY = new Vector3(0, 0, dir.y);
                canMove = !IsColliding(moveDirY, time);
                if (canMove)
                {
                    moveDir = moveDirY;
                }
            }
        }

        return canMove;
    }

    private bool IsColliding(Vector3 moveDir, float time)
    {
        var position = transform.position;
        return Physics.CapsuleCast(position, position + Vector3.up * height, radius, moveDir, speed * time);
    }
}