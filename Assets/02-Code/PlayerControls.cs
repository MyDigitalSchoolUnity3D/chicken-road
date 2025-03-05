using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Transform _playerTransform;
    private Transform _camera;

    public void MovePlayer(Vector3 direction, Quaternion rotation)
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
    }

    public void Start()
    {
        _playerTransform = GetComponent<Transform>();
        _camera = Camera.main.transform;

        // Position la caméra derrière le joueur
        _camera.position = new Vector3(
            _playerTransform.position.x,
            _camera.position.y + 2f,
            _playerTransform.position.z - 6f
        );
        _camera.rotation = Quaternion.Euler(20, 0, 0);
    }

    public void Update()
    {
        // Récupère les touches du clavier pour déterminer la direction du joueur
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.W))
        {
            // Déplace le joueur vers le haut
            MovePlayer(Vector3.forward, Quaternion.Euler(0, 0, 0));
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.A))
        {
            // Déplace le joueur vers la gauche
            MovePlayer(Vector3.left, Quaternion.Euler(0, -90, 0));
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            // Déplace le joueur vers la droite
            MovePlayer(Vector3.right, Quaternion.Euler(0, 90, 0));
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            // Déplace le joueur vers le bas
            MovePlayer(Vector3.back, Quaternion.Euler(0, 180, 0));
        }
    }
}
