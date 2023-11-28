using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Vector2ChannelSO OnMoveChannel;
        [SerializeField] public UnityEvent<Vector2> OnMovement;
        [SerializeField] private float speed;
        [SerializeField] private float radius = 0.7f;
        [SerializeField] private float height;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private bool activateGizmos;
        private Coroutine movement;


        private void OnEnable()
        {
            OnMoveChannel.Subscribe(Move);
        }

        private void OnDisable()
        {
            OnMoveChannel.Unsubscribe(Move);
            StopCoroutine(movement);
        }

        private void Move(Vector2 dir)
        {
            OnMovement.Invoke(dir);
            if (movement != null)
            {
                StopCoroutine(movement);
            }

            movement = StartCoroutine(Movement(dir));
        }

        /// <summary>
        /// Movement Corroutine
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private IEnumerator Movement(Vector2 dir)
        {
            while (dir != Vector2.zero)
            {
                Vector3 moveDir = new Vector3(dir.x, 0, dir.y);
                float time = Time.deltaTime;
                Rotation(moveDir);
                bool canMove = CanMove(dir, time, ref moveDir);
                if (canMove)
                {
                    transform.position += moveDir * (time * speed);
                }

                yield return null;
            }
        }

        /// <summary>
        /// Checks if the player is able to move towards the input direction.
        /// Returns true if the player can move at least in one direction. 
        /// </summary>
        /// <param name="dir">InputDirection.</param>
        /// <param name="time">Delta Time.</param>
        /// <param name="moveDir">Direction to be return. It changes depending on the available direction.</param>
        /// <returns></returns>
        private bool CanMove(Vector2 dir, float time, ref Vector3 moveDir)
        {
            moveDir = new Vector3(dir.x, 0, dir.y);
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

        /// <summary>
        /// Returns true if the player is collinding.
        /// </summary>
        /// <param name="moveDir">Direction of the player</param>
        /// <param name="time">Delta Time</param>
        /// <returns></returns>
        private bool IsColliding(Vector3 moveDir, float time)
        {
            var position = transform.position;
            return Physics.CapsuleCast(position, position + Vector3.up * height, radius, moveDir, speed * time);
        }

        private void Rotation(Vector3 moveDir)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        }

        private void OnDrawGizmosSelected()
        {
            if (!activateGizmos) return;
            DrawWireCapsule(transform.position, transform.rotation, radius, height, Color.red);
        }

        private static void DrawWireCapsule(Vector3 _pos, Quaternion _rot, float _radius, float _height,
            Color _color = default(Color))
        {
#if UNITY_EDITOR

            if (_color != default(Color))
                Handles.color = _color;
            Matrix4x4 angleMatrix = Matrix4x4.TRS(_pos, _rot, Handles.matrix.lossyScale);
            using (new Handles.DrawingScope(angleMatrix))
            {
                var pointOffset = (_height - (_radius * 2)) / 2;

                //draw sideways
                Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.left, Vector3.back, -180, _radius);
                Handles.DrawLine(new Vector3(0, pointOffset, -_radius), new Vector3(0, -pointOffset, -_radius));
                Handles.DrawLine(new Vector3(0, pointOffset, _radius), new Vector3(0, -pointOffset, _radius));
                Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.left, Vector3.back, 180, _radius);
                //draw frontways
                Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.back, Vector3.left, 180, _radius);
                Handles.DrawLine(new Vector3(-_radius, pointOffset, 0), new Vector3(-_radius, -pointOffset, 0));
                Handles.DrawLine(new Vector3(_radius, pointOffset, 0), new Vector3(_radius, -pointOffset, 0));
                Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.back, Vector3.left, -180, _radius);
                //draw center
                Handles.DrawWireDisc(Vector3.up * pointOffset, Vector3.up, _radius);
                Handles.DrawWireDisc(Vector3.down * pointOffset, Vector3.up, _radius);
            }
#endif
        }
    }
}