using System;
using System.Collections;
using System.Collections.Generic;
using Health;
using Turret;
using UnityEngine;

namespace Grid
{
    public class GridController : MonoBehaviour
    {
        private GridSystem grid;
        private Tile currentTile;
        [SerializeField] private PlayerInventory playerInventory;
        private Vector2Int cursorPos = new Vector2Int(0, 0);
        private Vector2Int previousInput = new Vector2Int(0, 0);
        [SerializeField] private GameObject gridIndicator;
        [SerializeField] private Vector2ChannelSO movementChannel;
        [SerializeField] private VoidChannelSO interactChannel;
        [SerializeField] private VoidChannelSO backInputChannel;
        [SerializeField] private VoidChannelSO changeToPlayerChannel;
        private Coroutine moveGrid;

        private void Awake()
        {
            grid = GetComponent<GridSystem>();
            gridIndicator.SetActive(false);
        }

        private void Start()
        {
            cursorPos = new Vector2Int(0, 0);
            SelectCurrentTile();
        }

        private void OnEnable()
        {
            movementChannel.Subscribe(OnMove);
            interactChannel.Subscribe(OnInteract);
            backInputChannel.Subscribe(OnBackChannel);
        }

        private void OnDisable()
        {
            gridIndicator.SetActive(false);
            movementChannel.Unsubscribe(OnMove);
            interactChannel.Unsubscribe(OnInteract);
            backInputChannel.Unsubscribe(OnBackChannel);
        }

        private void OnMove(Vector2 input)
        {
            CheckPreviousInput(input);
            var limits = grid.GetGridSize() - Vector2Int.one;
            cursorPos.Clamp(new Vector2Int(0, 0), limits);
            SelectCurrentTile();
        }


        private void CheckPreviousInput(Vector2 input)
        {
            Vector2Int currentInput = new Vector2Int(Mathf.RoundToInt(input.x), Mathf.RoundToInt(input.y));
            Vector2Int toReturn = currentInput;
            if (currentInput.x == previousInput.x)
            {
                toReturn.x = 0;
            }

            if (currentInput.y == previousInput.y)
            {
                toReturn.y = 0;
            }

            previousInput = toReturn;
            cursorPos += toReturn;
        }


        private void SelectCurrentTile()
        {
            if (!gridIndicator.activeSelf)
            {
                gridIndicator.SetActive(true);
            }
            currentTile = grid.GetTile(cursorPos);
            gridIndicator.transform.position = currentTile.transform.position;
        }

        /// <summary>
        /// Places the Turret
        /// </summary>
        private void OnInteract()
        {
            if (currentTile.IsAvailable() && playerInventory.hasPotion())
            {
                currentTile.SetTurret(playerInventory.GetTurret());
                playerInventory.DestroyPickable();
                OnBackChannel();
            }
            else if (!currentTile.IsAvailable() && !playerInventory.hasPotion())
            {
                currentTile.DestroyTurret();
                OnBackChannel();
            }
        }

        /// <summary>
        /// Deactivates the GridController
        /// </summary>
        private void OnBackChannel()
        {
            changeToPlayerChannel.RaiseEvent();
        }
    }
}