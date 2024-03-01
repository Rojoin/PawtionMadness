using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInputs
{
    public class InputController : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private Vector2ChannelSO OnMoveChannel;
        [SerializeField] private Vector2ChannelSO OnGridMoveChannel;
        //[SerializeField] private VoidChannelSO OnRollChannel;
        [SerializeField] private VoidChannelSO OnPauseChannel;
        [SerializeField] private VoidChannelSO OnInteractChannel;
        [SerializeField] private VoidChannelSO OnBackInteractChannel;
        [SerializeField] private VoidChannelSO OnCheatKillScreenEnemy;
        [SerializeField] private VoidChannelSO OnCheatWinGame;
        [SerializeField] private VoidChannelSO OnCheatLoseGame;
        [SerializeField] private VoidChannelSO OnCheatToggleConsole;
        [SerializeField] float offsetController = 0.70f;
        private Vector2 previousGridInput = Vector2.zero;
        private bool cheats;

        public void OnMove(InputAction.CallbackContext ctx)
        {
            OnMoveChannel.RaiseEvent(ctx.ReadValue<Vector2>());
        }

        public void OnGridMove(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                Vector2 ctxInput = ctx.ReadValue<Vector2>();

                OnGridMoveChannel.RaiseEvent(ctxInput);
            }

            if (ctx.canceled)
            {
                OnGridMoveChannel.RaiseEvent(Vector2.zero);
            }
        }
        public void OnStickGridMove(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                Vector2 ctxInput = ctx.ReadValue<Vector2>();

                if (ctxInput.x > offsetController)
                {
                    ctxInput.x = 1;
                }
                else if (ctxInput.x < -offsetController)
                {
                    ctxInput.x = -1;
                }
                else
                {
                    ctxInput.x = 0;
                }
                if (ctxInput.y > offsetController)
                {
                    ctxInput.y = 1;
                }
                else if (ctxInput.y < -offsetController)
                {
                    ctxInput.y = -1;
                }
                else
                {
                    ctxInput.y = 0;
                }

                var newValue = ctxInput;

                if (newValue != previousGridInput)
                {
                    OnGridMoveChannel.RaiseEvent(newValue);
                    previousGridInput = newValue;
                }
            }

            if (ctx.canceled)
            {
                OnGridMoveChannel.RaiseEvent(Vector2.zero);
            }
        }

        public void OnInteract(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                OnInteractChannel.RaiseEvent();
            }
        }

        public void OnBackInteract(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                OnBackInteractChannel.RaiseEvent();
            }
        }

        public void OnPauseMode(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                OnPauseChannel.RaiseEvent();
            }
        }

        public void OnCheatsSetState(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                cheats = !cheats;
                if (cheats)
                {
                    Debug.Log("Cheats Enable");
                }
                else
                {
                    Debug.Log("Cheats Disable");
                }
            }
        }

        public void OnCheatKillScreenEnemies(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if (cheats)
                {
                    OnCheatKillScreenEnemy.RaiseEvent();
                }
            }
        }

        public void OnCheatWinScreen(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if (cheats)
                {
                    OnCheatWinGame.RaiseEvent();
                }
            }
        }

        public void OnCheatLoseScreen(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if (cheats)
                {
                    OnCheatLoseGame.RaiseEvent();
                }
            }
        }

        public void OnShowConsole(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                OnCheatToggleConsole.RaiseEvent();
            }
        }
    }
}