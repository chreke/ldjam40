using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    public float FinalUnitSize { get { return finalUnitSize; } }
    public int PixelsPerUnit { get { return pixelsPerUnit; } }
    public int VertUnitsOnScreen { get { return verticalUnitsOnScreen; } }

    [SerializeField]
    int pixelsPerUnit = 16;
    [SerializeField]
    int verticalUnitsOnScreen = 4;
    private float finalUnitSize;
    private new Camera camera;


    GameObject player;
    float smoothSpeed = 0.125f;

    void Awake()
    {
        camera = gameObject.GetComponent<Camera>();
        Assert.IsNotNull(camera);

        SetOrthographicSize();
    }

    void SetOrthographicSize()
    {
        ValidateUserInput();

        // get device's screen height and divide by the number of units 
        // that we want to fit on the screen vertically. this gets us
        // the basic size of a unit on the the current device's screen.
        var tempUnitSize = Screen.height / verticalUnitsOnScreen;

        // with a basic rough unit size in-hand, we now round it to the
        // nearest power of pixelsPerUnit (ex; 16px.) this will guarantee
        // our sprites are pixel perfect, as they can now be evenly divided
        // into our final device's screen height.
        finalUnitSize = GetNearestMultiple(tempUnitSize, pixelsPerUnit);

        // ultimately, we are using the standard pixel art formula for 
        // orthographic cameras, but approaching it from the view of:
        // how many standard Unity units do we want to fit on the screen?
        // formula: cameraSize = ScreenHeight / (DesiredSizeOfUnit * 2)
        camera.orthographicSize = Screen.height / (finalUnitSize * 2.0f);
    }

    int GetNearestMultiple(int value, int multiple)
    {
        int rem = value % multiple;
        int result = value - rem;
        if (rem > (multiple / 2))
            result += multiple;

        return result;
    }

    void ValidateUserInput()
    {
        if (pixelsPerUnit == 0)
        {
            pixelsPerUnit = 1;
            Debug.Log("Warning: Pixels-per-unit must be greater than zero. " +
                      "Resetting to minimum allowed.");
        }
        else if (verticalUnitsOnScreen == 0)
        {
            verticalUnitsOnScreen = 1;
            Debug.Log("Warning: Units-on-screen must be greater than zero." +
                      "Resetting to minimum allowed.");
        }
    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y + 1, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}