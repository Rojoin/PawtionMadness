using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField]private Vector2ChannelSO OnMoveChannel;
    [SerializeField]private VoidChannelSO OnRollChannel;
    [SerializeField]private VoidChannelSO OnPauseChannel;
    [SerializeField]private BoolChannelSO OnFocusChannel;
    [SerializeField]private BoolChannelSO OnFireChannel;
    [Header("Cheats")]
    [SerializeField]private VoidChannelSO OnInfinityLaser;
    [SerializeField]private VoidChannelSO OnFlash;
    [SerializeField]private VoidChannelSO OnNextLevel;
    [SerializeField]private VoidChannelSO OnGodMode;
    [SerializeField]private VoidChannelSO OnNuke;


    public void OnMove(InputAction.CallbackContext ctx)
    {
        OnMoveChannel.RaiseEvent(ctx.ReadValue<Vector2>());
    }
    public void OnRollInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            OnRollChannel.RaiseEvent();
        }
    }
    public void OnFocusMode(InputAction.CallbackContext ctx)
    {
        OnFocusChannel.RaiseEvent(ctx.performed);
    } 
    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            OnFireChannel.RaiseEvent(true);
        }
        else if (ctx.canceled)
        {
            OnFireChannel.RaiseEvent(false);
        }
    }
    public void OnPauseMode(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            OnPauseChannel.RaiseEvent();
        }
    }
    public void OnCheatInfinityAmmo(InputAction.CallbackContext ctx)
    {
        OnInfinityLaser.RaiseEvent();
    } 
    public void OnCheatGodMode(InputAction.CallbackContext ctx)
    {
        OnGodMode.RaiseEvent();
    } 
    public void OnCheatFlash(InputAction.CallbackContext ctx)
    {
        OnFlash.RaiseEvent();
    } 
    public void OnCheatNuke(InputAction.CallbackContext ctx)
    {
        OnNuke.RaiseEvent();
    }
    public void OnCheatNextLevel(InputAction.CallbackContext ctx)
    {
        OnNextLevel.RaiseEvent();
    }
}