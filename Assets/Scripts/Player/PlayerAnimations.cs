using System;
using Player;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerInventory _playerInventory;
    [SerializeField] private VoidChannelSO OnCristalInteraction;
    [SerializeField] private VoidChannelSO OnCristalBackInteraction;
    private bool isPlayerInvoking = false;
    private PlayerMovement _playerMovement;
    [SerializeField] private Animator _animator;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsHolding = Animator.StringToHash("isHolding");
    private static readonly int Invoking = Animator.StringToHash("isInvoking");

    private void Awake()
    {
        _playerInventory = GetComponent<PlayerInventory>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.OnMovement.AddListener(AnimatorMoveBool);
        _playerInventory.OnItemPickUp.AddListener(AnimatorHoldingBool);
        OnCristalInteraction.Subscribe(AnimatorTriggerInvoking);
        OnCristalBackInteraction.Subscribe(AnimatorTriggerInvoking);
    }

    private void OnDestroy()
    {
        _playerMovement.OnMovement.RemoveListener(AnimatorMoveBool);
        _playerInventory.OnItemPickUp.RemoveListener(AnimatorHoldingBool);
        OnCristalInteraction.Unsubscribe(AnimatorTriggerInvoking);
        OnCristalBackInteraction.Unsubscribe(AnimatorTriggerInvoking);
    }

    void AnimatorMoveBool(Vector2 dir)
    {
        _animator.SetBool(IsMoving, dir != Vector2.zero);
    }
    void AnimatorHoldingBool(bool state)
    {
        _animator.SetBool(IsHolding, state); 
    } 
    void AnimatorTriggerInvoking()
    {
        isPlayerInvoking = !isPlayerInvoking;
        _animator.SetBool(Invoking,isPlayerInvoking); 
    }
}