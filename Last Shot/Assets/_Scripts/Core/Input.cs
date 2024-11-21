using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Input : MonoBehaviour
{
    public static Input instance;

    
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference aim;

    public GameAction Roll;
    public GameAction Shoot;

    private float inputConsumeTimer;

    public bool usingGamepad;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        
        Roll.Setup(this, 0.1f);
        Shoot.Setup(this, 0.01f);
    }

    private void Update()
    {
        inputConsumeTimer -= Time.deltaTime;

        if (inputConsumeTimer <= 0)
        {
            Roll.Consume();
        }
    }

    public void SetInputConsumeTimer(float time) => inputConsumeTimer = time;

    public static Vector2 MovementInput()
    {
        return instance.movement.action.ReadValue<Vector2>();
    }

    public static Vector2 Aim()
    {
        if (instance.usingGamepad)
        {
            return instance.aim.action.ReadValue<Vector2>();
        }
        else
        {
            var mousePos = Camera.main.ScreenToWorldPoint(instance.aim.action.ReadValue<Vector2>());
      
            return mousePos - instance.transform.position;
        }
    }


    private void OnControlsChanged(PlayerInput playerInput)
    {
        usingGamepad = playerInput.currentControlScheme.Equals("Gamepad");
    }
}
