using UnityEngine;
using System.Collections;

public class CameraControllScript : MonoBehaviour {

    public static CameraControllScript Instance;
    public Transform TargetLookat;

    public float Distance = 5;
    public float DistanceMin = 3;
    public float DistanceMax = 10;

    public float XMousSensitivity = 5f;
    public float YMousSensitivity = 5f;
    public float MouseWheelSensitivity = 5f;
    public float YMinLimit = -40f;
    public float YMaxLimit = 80f;

    public float DistanceSmooth = 0.05f;


    public float XSmooth = 0.05f;
    public float YSmooth = 0.1f;

    


    private float _mouseX = 0;
    private float _mouseY = 0;
    private float _startDistance = 0;
    private float _desiredDistance = 0;
    private float _velDistance = 0f;
    private Vector3 _desiredPosition = Vector3.zero;
    private float _velX = 0.0f;
    private float _velY = 0.0f;
    private float _velZ = 0.0f;
    private Vector3 _position = Vector3.zero;

    public int CameraNr = 0;

    void Awake()
    {
        Instance = this;
    }
	// Use this for initialization
	void Start () {
        Distance = Mathf.Clamp(Distance, DistanceMin, DistanceMax);
        _startDistance = Distance;
        Reset();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (TargetLookat == null)
        {
            return;
        }

        HandlePlayerInput();
        CalcDesiredPos();
        UpdatePos();
	}

    void LateUpdate()
    {
        
    }

    void CalcDesiredPos()
    {
        //eval distance with smoothcalc
        Distance = Mathf.SmoothDamp(Distance, _desiredDistance, ref _velDistance, DistanceSmooth);

        _desiredPosition = CalcPos(_mouseY, _mouseX, Distance);


    }

    Vector3 CalcPos(float rotX, float rotY, float distance)
    {
        Vector3 direction = new Vector3(0.0f, 0.0f, -distance);
        Quaternion rotation = Quaternion.Euler(rotX, rotY, 0.0f);
        return TargetLookat.position + rotation * direction;
    }

    void UpdatePos()
    {
        var positionX = Mathf.SmoothDamp(_position.x, _desiredPosition.x, ref _velX, XSmooth);
        var positionY = Mathf.SmoothDamp(_position.y, _desiredPosition.y, ref _velY, YSmooth);
        var positionZ = Mathf.SmoothDamp(_position.z, _desiredPosition.z, ref _velZ, XSmooth);
        _position = new Vector3(positionX, positionY, positionZ);
        transform.position = _position;
        transform.LookAt(TargetLookat);
    }

    public void HandlePlayerInput()
    {

        var deadzone = 0.1f;


        _mouseX += Input.GetAxis("Look Horizontal"+CameraNr)*XMousSensitivity;
        _mouseY -= Input.GetAxis("Look Vertical"+CameraNr) * YMousSensitivity;

        //TODO: Limit  Mouse Y Rotation

        _mouseY = HelperClass.ClampAngle(_mouseY, YMinLimit, YMaxLimit);




        if (Input.GetAxis("Mouse ScrollWheel")< -deadzone || Input.GetAxis("Mouse ScrollWheel") > deadzone)
        {
            _desiredDistance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity, DistanceMin, DistanceMax);
        }
    }

    void Reset()
    {
        _mouseX = 0;
        _mouseY = 10;
        Distance = _startDistance;
        _desiredDistance = Distance;
    }
}
