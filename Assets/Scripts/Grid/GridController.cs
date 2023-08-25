using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Grid
{
    public class GridController : MonoBehaviour
    {
        private GridSystem grid;
        private Tile currentTile;
        private Vector2Int cursorPos = new Vector2Int(0, 0);
        private Vector2Int previousInput= new Vector2Int(0, 0);
        [SerializeField] private Vector2ChannelSO movementChannel;
        private Coroutine moveGrid;

        private void Awake()
        {
            grid = GetComponent<GridSystem>();
        }

        private void Start()
        {
            cursorPos = new Vector2Int(0, 0);
            SelectCurrentTile();
            movementChannel.Subscribe(OnMove);
        }

        private void OnDisable()
        {
            currentTile.SelectTile(false);
            movementChannel.Unsubscribe(OnMove);
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
            if  (currentInput.x == previousInput.x) 
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
            if (currentTile != null)
            {
                currentTile.SelectTile(false);
            }

            currentTile = grid.GetTile(cursorPos);
            currentTile.SelectTile(true);
        }
    }
}