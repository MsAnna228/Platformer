using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector3 _moveDirection;
    private CharacterController _controller;
    private float _horizontalInput;
    private bool _canDoubleJump = false;

    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    [SerializeField]
    private float _currentPlayerLives;
    private float _maxPlayerLives = 3;
    [SerializeField]
    private Vector3 _respawnPos = new Vector3(-8, 10, 0);

    private float _yVelocity;
    public int _playerCoins;
    private UIManager _uiManager;
    private Vector3 _velocity;
    private bool _canWallJump;
    private Vector3 _wallNormal;
    private float _pushPower = 2.0f;
    



    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Controller is NULL");       
        }
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("Uimanager is NULL");
        }
        _playerCoins = 0;
        _currentPlayerLives = _maxPlayerLives;
        _uiManager.UpdateLives(_currentPlayerLives, _maxPlayerLives);
    }

    void Update()
    {       
        float _horizontalInput = Input.GetAxis("Horizontal");    

        if (_controller.isGrounded)
        {
            _moveDirection = new Vector3(_horizontalInput, 0, 0);
            _velocity = _moveDirection * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            _yVelocity -= _gravity;            
            if (_canWallJump && Input.GetKeyDown(KeyCode.Space))
            {
                _velocity = _wallNormal * _speed;
                _yVelocity += _jumpHeight;
                _canWallJump = false;
            }                                 
            if (_canDoubleJump == true && Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity += _jumpHeight;
                _canDoubleJump = false;
            }
        }
        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);

        if (transform.position.y < -6)
        {
            transform.position = _respawnPos;
            Damage();
        }
    }

    public void AddCoins(int coin)
    {
        _playerCoins += coin;
        _uiManager.UpdateScoreDisplay(_playerCoins);
    }

    void Damage()
    {
        _currentPlayerLives--;
        _uiManager.UpdateLives(_currentPlayerLives, _maxPlayerLives);
        if (_currentPlayerLives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "MovingBox")
        {
            Rigidbody box = hit.collider.GetComponent<Rigidbody>();
            if (box != null)
            {
                Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, 0);
                box.velocity = pushDirection * _pushPower;
            }
     
        }
 

        if (hit.transform.tag == "Wall")
        {
            Debug.DrawRay(hit.point, hit.normal, Color.blue);
            _wallNormal = hit.normal;
            _canWallJump = true;
        }
    }
}
