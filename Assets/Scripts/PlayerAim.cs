using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Transform rotateObj;

    private InputSystem_Actions inputActions;
    private Camera mainCam;
    private Vector2 mousePosScreen;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        mainCam = Camera.main;        //Main Cam reference
    }

    private void Update()
    {
        float distFromCam = 10f;
        mousePosScreen = Mouse.current.position.ReadValue();
        Vector3 mousePosWorld = mainCam.ScreenToWorldPoint(new Vector3(mousePosScreen.x, mousePosScreen.y, distFromCam));   //Convert to world co-ordinates

        Vector2 lookDir = (Vector2)mousePosWorld - (Vector2)rotateObj.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;        //Angle calc and convert from radian to degree

        rotateObj.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
