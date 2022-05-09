using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    [SerializeField] float speed = 250f;
    private Rigidbody2D _rb;
    private Controls _controls;
    private Collider2D _collider;
    private Camera _cam;

    void Awake()
    {
            if(_controls == null){
            try
            {
                _controls = new Controls();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;

        if(_rb == null){
            try
            {
                _rb = this.gameObject.GetComponent<Rigidbody2D>();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        if(_collider == null){
            try
            {
                _collider = this.gameObject.GetComponent<Collider2D>();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }

    void OnEnable()
    {
        _controls.Enable();
    }

    void OnDisable()
    {
        _controls.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        GetInput();
        RotateToMouse();
    }

    void FixedUpdate()
    {
        HandleMovement(GetInput());
        KeepPlayerOnScreen();
    }

    public Vector2 GetInput(){
        Vector2 _playerInput = _controls.PlayerControl.Movement.ReadValue<Vector2>();
        _playerInput.Normalize();

        return _playerInput;
    }

    void HandleMovement(Vector2 vec2){
        _rb.velocity = speed * Time.fixedDeltaTime * vec2;
    }

    void RotateToMouse(){
        Vector3 _mousePos = _cam.ScreenToWorldPoint(_controls.PlayerControl.Cursor.ReadValue<Vector2>());
        Vector2 _lookAtPos = _mousePos - transform.position;
        _lookAtPos.Normalize();
        transform.up = _lookAtPos;
    }

    void KeepPlayerOnScreen(){
        // Calculate the bounds of the camera & the bounds of the player collider.
        Vector2 _cameraBounds = _cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _cam.transform.position.z));
        Vector3 _viewPos = transform.position;
        _viewPos.x = Mathf.Clamp(_viewPos.x, _cameraBounds.x * -1 + _collider.bounds.extents.x, _cameraBounds.x - _collider.bounds.extents.x);
        _viewPos.y = Mathf.Clamp(_viewPos.y, _cameraBounds.y * -1 + _collider.bounds.extents.y, _cameraBounds.y - _collider.bounds.extents.y);

        // Apply the above and prevent the player from leaving the camera's view.
        transform.position = _viewPos;
    }
}