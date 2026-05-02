using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _player;

    [Header("Configuración General")]
    [SerializeField] private float _walkSpeed = 2.0f;
    [SerializeField] private float _runSpeed = 6.0f;
    [SerializeField] private float _mouseSensitivity = 2.0f;

    [Header("Física Normal (Caminar)")]
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 1.5f;

    [Header("Modo Vuelo (Dron)")]
    [SerializeField] private float _flySpeedVertical = 5.0f;
    [SerializeField] private float _maxHeight = 50.0f;

    [Header("Detección de Suelo (para MeshCollider)")]
    [SerializeField] private float _groundCheckDistance = 0.25f;
    [SerializeField] private LayerMask _groundLayer = ~0; // <- por defecto "TODO" (no se queda en Nothing)

    private bool _isFlying = false;
    private Vector3 _velocity;

    private float _xRotation = 0f;
    private Camera _playerCamera;

    private void Awake()
    {
        _player = GetComponent<CharacterController>();
        _playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            ToggleFlying();

        HandleMouseLook();
        HandleMovement();
    }

    private void ToggleFlying()
    {
        _isFlying = !_isFlying;
        _velocity = Vector3.zero;
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * 100f * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);
        _playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (move.magnitude > 1f) move.Normalize();

        float speed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed;
        Vector3 horizontalMove = move * speed;

        if (_isFlying)
            HandleFlying(horizontalMove);
        else
            HandleGrounded(horizontalMove);
    }

    // ✈️ VUELO (Shift acelera también)
    private void HandleFlying(Vector3 horizontalMove)
    {
        float verticalSpeed = Input.GetKey(KeyCode.LeftShift) ? _flySpeedVertical * 2f : _flySpeedVertical;

        float vertical = 0f;
        if (Input.GetKey(KeyCode.Z)) vertical = verticalSpeed;
        else if (Input.GetKey(KeyCode.X)) vertical = -verticalSpeed;

        float newY = Mathf.Clamp(transform.position.y + vertical * Time.deltaTime, 0f, _maxHeight);
        float deltaY = newY - transform.position.y;

        Vector3 finalMove = horizontalMove;
        finalMove.y = deltaY / Time.deltaTime;

        _player.Move(finalMove * Time.deltaTime);
    }

    // 🚶 CAMINAR + SALTO
    private void HandleGrounded(Vector3 horizontalMove)
    {
        bool grounded = IsGroundedStable();

        // Mantener pegado al suelo
        if (grounded && _velocity.y < 0f)
            _velocity.y = -5f;

        // Salto (Space por defecto en "Jump")
        if (Input.GetButtonDown("Jump") && grounded)
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

        _velocity.y += _gravity * Time.deltaTime;

        Vector3 finalMove = horizontalMove;
        finalMove.y = _velocity.y;

        _player.Move(finalMove * Time.deltaTime);
    }

    // ✅ Ground check robusto para MeshCollider
    private bool IsGroundedStable()
    {
        // Centro del capsule del CharacterController en mundo
        Vector3 centerWorld = transform.TransformPoint(_player.center);

        // Punto cerca de la base del capsule
        float bottomOffset = (_player.height * 0.5f) - _player.radius;
        Vector3 origin = centerWorld + Vector3.down * bottomOffset;

        float radius = _player.radius * 0.95f;
        float castDist = _groundCheckDistance;

        // SphereCast hacia abajo para no fallar entre triángulos del mesh
        bool hit = Physics.SphereCast(
            origin + Vector3.up * 0.05f,   // un pelín arriba para evitar empezar dentro del suelo
            radius,
            Vector3.down,
            out _,
            castDist + 0.05f,
            _groundLayer,
            QueryTriggerInteraction.Ignore
        );

        // Extra: si Unity dice grounded, también lo aceptamos
        return hit || _player.isGrounded;
    }
}
