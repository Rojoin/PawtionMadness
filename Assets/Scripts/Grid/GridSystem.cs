using System;
using System.Collections.Generic;
using Turret;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


namespace Grid
{
    public class GridSystem : MonoBehaviour
    {
        [SerializeField] private int columns;
        [SerializeField] private int rows;
        [SerializeField] private int deltaX = 2;
        [SerializeField] private int deltaY = 4;
        [SerializeField] private UnityEngine.Grid grid;
        [FormerlySerializedAs("cube")] [SerializeField] private Tile tile;
        [SerializeField] private List<GameObject> tileList;
        private Tile[,] tileSet;


        private void Awake()
        {
            
        }

    

        public Tile GetTile(Vector2Int coord)
        {
            return tileSet[coord.x, coord.y];
        }

        public Vector2Int GetGridSize() => new Vector2Int(columns, rows);

        public void PlaceTurret(Tile tile, BaseTurret turret)
        {
            if (tile.IsAvailable())
            {
                tile.SetTurret(turret);
            }
            else
            {
                Debug.Log("There's already a Turret Here");
            }
        }
        [ContextMenu("Create MockUp of grid")]
        private void CreateGrid()
        {
            foreach (var tile in tileList)
            {
                DestroyImmediate(tile);
            }
            grid = GetComponent<UnityEngine.Grid>();
            tileSet = new Tile[columns, rows];
            tileList = new List<GameObject>();
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    var worldPos = grid.GetCellCenterWorld(new Vector3Int(i * deltaX, 0, (j - 1) * deltaY));
                    var newTile = Instantiate(this.tile, worldPos, Quaternion.identity, this.transform);
                    newTile.ChangeModel(Color.white);
                    tileList.Add(newTile.gameObject);
                    tileSet[i, j] = newTile;
                    tileSet[i, j].SetTurret(null);
                }
            }
        }
    }
}