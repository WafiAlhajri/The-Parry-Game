using UnityEngine;
using UnityEngine.InputSystem;

public class ParryControls : MonoBehaviour
{
    public Rigidbody2D RB2D;
    public Camera MC;
    Vector2 rotationinput;
    PlayerInput PI;
    public GameObject ParryCollider;
    [SerializeField] bool canParry = true;
    void Start()
    {
        PI = GetComponent<PlayerInput>();
        RB2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (PI.currentControlScheme == "Keyboard&Mouse")
        {
            Vector3 worldpos = MC.ScreenToWorldPoint(new Vector3(rotationinput.x, rotationinput.y));
            Vector3 rotationdirection = (worldpos - transform.position).normalized;
            rotationdirection.z = 0;
            float angle = Mathf.Atan2(rotationdirection.y, rotationdirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            RB2D.SetRotation(targetRotation);
        }
        if (PI.currentControlScheme == "Gamepad")
        {
            float angle = Mathf.Atan2(rotationinput.y, rotationinput.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            RB2D.SetRotation(targetRotation);
        }
    }
    public void OnRotate(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            rotationinput = context.ReadValue<Vector2>();
        }
    }
    public void OnParry()
    {
        if (canParry)
        {
            canParry = false;
            ParryCollider.SetActive(true);
            Invoke(nameof(StopParry), 0.1f);
            Invoke(nameof(ResetParry), 1f);
        }
    }
    void StopParry()
    {
        ParryCollider.SetActive(false);
    }
    public void ResetParry()
    {
        ParryCollider.SetActive(false);
        canParry = true;
        CancelInvoke();
    }
}
