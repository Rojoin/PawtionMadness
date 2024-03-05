using System.Collections;
using UnityEngine;

namespace Grid
{
    public class GridController : MonoBehaviour
    {
        [Header("References in Scene")]
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private TurretManager _turretManager;
        [SerializeField] private float indicatorYOffset;
        [SerializeField] private GridIndicator gridIndicator;
        [Header("Channels")]
        [SerializeField] private Vector2ChannelSO movementChannel;
        [SerializeField] private VoidChannelSO interactChannel;
        [SerializeField] private VoidChannelSO backInputChannel;
        [SerializeField] private VoidChannelSO changeToPlayerChannel;
        [SerializeField] private VoidChannelSO onTurretPlaced;
        [Header("Data")]
        [SerializeField] private TimeController timeControllerData;
        [Header("Values")]
        [Range(0.1f, 1.0f)]
        [SerializeField] private float timeToHold = 0.5f;
        [SerializeField] private Vector2Int _defaultPos = new Vector2Int(0, 0);

        private Vector2 _input;
        private Vector2Int _cursorPos = new Vector2Int(0, 0);
        private Vector2Int _previousInput = new Vector2Int(0, 0);
        private GridSystem _grid;
        private Tile _currentTile;
        private bool _isGridNotMoving;
        private Coroutine _moveGrid;

        private void Awake()
        {
            _grid = GetComponent<GridSystem>();
            gridIndicator.gameObject.SetActive(false);
        }

        private void Start()
        {
            _cursorPos = _defaultPos;
            SelectCurrentTile();
        }

        private void OnEnable()
        {
            movementChannel.Subscribe(OnMove);
            interactChannel.Subscribe(OnInteract);
            backInputChannel.Subscribe(OnBackChannel);
            if (_playerStats.shouldGridControllerReset)
            {
                _cursorPos = _defaultPos;
            }
            
            SelectCurrentTile();
            if (playerInventory.GetTurret())
            {
                string value = playerInventory.GetTurret().id;
                gridIndicator.SetCurrentTurret(value);
            }

            Time.timeScale = timeControllerData.slowTimeScale;
        }

        private void OnDisable()
        {
            if (_moveGrid != null)
            {
                StopCoroutine(_moveGrid);
            }

            gridIndicator.gameObject.SetActive(false);
            movementChannel.Unsubscribe(OnMove);
            interactChannel.Unsubscribe(OnInteract);
            backInputChannel.Unsubscribe(OnBackChannel);
            Time.timeScale = timeControllerData.defaultTimeScale;
        }

        private void OnMove(Vector2 input)
        {
            _input = input;
            CheckPreviousInput(_input);
            Vector2Int limits = _grid.GetGridSize() - Vector2Int.one;
            _cursorPos.Clamp(new Vector2Int(0, 0), limits);
            SelectCurrentTile();

            if (_moveGrid != null)
            {
                StopCoroutine(_moveGrid);
            }

            _moveGrid = StartCoroutine(MoveGrid());
        }

        private IEnumerator MoveGrid()
        {
            Vector2 currentInput = _input;
            while (_input == currentInput)
            {
                yield return new WaitForSeconds(timeToHold);
                OnMove(currentInput);
            }

            yield break;
        }

        private void CheckPreviousInput(Vector2 input)
        {
            Vector2Int currentInput = new Vector2Int(Mathf.RoundToInt(input.x), Mathf.RoundToInt(input.y));
            Vector2Int toReturn = currentInput;
            if (currentInput.x == _previousInput.x)
            {
                toReturn.x = 0;
            }

            if (currentInput.y == _previousInput.y)
            {
                toReturn.y = 0;
            }

            _previousInput = toReturn;
            _cursorPos += toReturn;
        }


        private void SelectCurrentTile()
        {
            if (!gridIndicator.gameObject.activeSelf)
            {
                gridIndicator.gameObject.SetActive(true);
            }

            _currentTile = _grid.GetTile(_cursorPos);
            Vector3 position = _currentTile.transform.position;
            gridIndicator.transform.position = new Vector3(position.x, indicatorYOffset, position.z);
        }

        /// <summary>
        /// Places the Turret
        /// </summary>
        private void OnInteract()
        {
            if (_currentTile.IsAvailable() && playerInventory.hasPotion())
            {
                SetTurretOnTile(_currentTile, playerInventory.GetTurret());
                playerInventory.DestroyPickable();
                onTurretPlaced.RaiseEvent();
                gridIndicator.ActivateSmokeEffect();
                OnBackChannel();
            }
        }

        /// <summary>
        /// Set Turret on Tile
        /// </summary>
        /// <param name="currentTile"></param>
        /// <param name="baseTurretSo"></param>
        private void SetTurretOnTile(Tile currentTile, BaseTurretSO baseTurretSo)
        {
            currentTile.SetTurret(_turretManager.AddNewTurret(baseTurretSo, currentTile.GetTurretPosition(),
                currentTile.transform));
        }

        public void SpawnDefaultTurret(BaseTurretSO baseTurretSo)
        {
            Tile defaultTile = _grid.GetTile(Vector2Int.zero);
            defaultTile.SetTurret(_turretManager.AddNewTurret(baseTurretSo, defaultTile.GetTurretPosition(),
                defaultTile.transform));
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