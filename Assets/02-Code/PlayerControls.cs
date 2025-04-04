using System;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Transform _playerTransform;
    private Transform _camera;
    private ScoreText _scoreText;
    public static int score = 0;
    public bool disabledControls = false;

    public delegate void OnPlayerMovedForward(Transform playerTransform);
    public static event OnPlayerMovedForward playerMovedForwardEvent;

    void Awake()
    {
        TriggerCarCollision.playerKilledEvent += HandlePlayerDeath;
    }

    void OnDestroy()
    {
        TriggerCarCollision.playerKilledEvent -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        disabledControls = true;
    }

    private void MovePlayer(Vector3 direction, Quaternion rotation)
    {
        _playerTransform.position += direction;
        _playerTransform.rotation = rotation;

        if (_camera)
        {
            // La caméra suit le joueur
            _camera.position = new Vector3(
                _playerTransform.position.x,
                _camera.position.y,
                _playerTransform.position.z - 6f
            );
        }

        SetScore(_playerTransform.position.z);
    }

    private void Start()
    {
        _playerTransform = GetComponent<Transform>();
        _camera = Camera.main.transform;
        _scoreText = FindFirstObjectByType<ScoreText>();

        // Position la caméra derrière le joueur
        _camera.SetPositionAndRotation(new Vector3(
            _playerTransform.position.x,
            _camera.position.y + 2f,
            _playerTransform.position.z - 6f
        ), Quaternion.Euler(20, 0, 0));

        score = 0;
    }

    public void Update()
    {
        if (disabledControls) return;

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.W))
        {
            if (IsGoingToBumpATree(Vector3.forward)) return;
            MovePlayer(Vector3.forward, Quaternion.Euler(0, 0, 0));

            // Dispatch PlayerMovedForwardEvent
            playerMovedForwardEvent?.Invoke(_playerTransform);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.A))
        {
            if (IsGoingToBumpATree(Vector3.left)) return;
            MovePlayer(Vector3.left, Quaternion.Euler(0, -90, 0));
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            if (IsGoingToBumpATree(Vector3.right)) return;
            MovePlayer(Vector3.right, Quaternion.Euler(0, 90, 0));
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            if (IsGoingToBumpATree(Vector3.back)) return;
            MovePlayer(Vector3.back, Quaternion.Euler(0, 180, 0));
        }
    }

    // Vérifie si le joueur va percuter un arbre
    private bool IsGoingToBumpATree(Vector3 direction) => Physics.Raycast(transform.position, direction, 1f);

    private void SetScore(float z)
    {
        // Incrémente le score
        if (z * 10 > score)
        {
            score = (int)Math.Floor(z) * 10;
            _scoreText.SetScore(score);
        }
    }
}
