using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

public class CarController : MonoBehaviour
{
    public enum ControlMode
    {
        Keyboard,
        SteeringWheel
    };

    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public GameObject wheelEffectObj;
        public ParticleSystem smokeParticle;
        public Axel axel;
    }

    LogitechGSDK.LogiControllerPropertiesData properties;

    public ControlMode control;

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass;

    public List<Wheel> wheels;

    float throttleInput;
    float steerInput;
    float breakInput;
    float moveInput;

    private Rigidbody carRb;

    private float MAX_VALUE_LOGITECH = 32767.0f; 

    // private CarLights carLights;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;
        Debug.Log("SteeringInit:" + LogitechGSDK.LogiSteeringInitialize(false));

        // carLights = GetComponent<CarLights>();
    }

    void OnApplicationQuit()
    {
        Debug.Log("SteeringShutdown:" + LogitechGSDK.LogiSteeringShutdown());
    }

    void Update()
    {
        GetInputs();
        AnimateWheels();
        // WheelEffects();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }


    void GetInputs()
    {
        
        //All the test functions are called on the first device plugged in(index = 0)
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0) && control == ControlMode.SteeringWheel){

            //CONTROLLER STATE
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            // normalizamos valores en el rango [-1, 1]
            throttleInput = (rec.lY * -1) / MAX_VALUE_LOGITECH ; // acelerador
            breakInput = (rec.lRz * -1) / MAX_VALUE_LOGITECH; // freno
            steerInput = rec.lX / MAX_VALUE_LOGITECH; // volante
            
            // si los pedales de aceleración y freno no estan pulsados
            if(throttleInput < 0) throttleInput = 0;
            if(breakInput < 0) breakInput= 0;

            // calculamos el movimiento del coche por los pedales del acelerador y del freno
            moveInput = throttleInput - breakInput;

            Debug.Log("steerInput -> "  + steerInput);
            Debug.Log("throtteInput -> "  + throttleInput);
            Debug.Log("breakInput -> "  + breakInput);
            Debug.Log("moveInput -> "  + moveInput);
        }
        else if(control == ControlMode.Keyboard){

            if (Input.GetKey(KeyCode.W)) throttleInput = maxAcceleration * Time.deltaTime; else throttleInput = 0;
            if (Input.GetKey(KeyCode.S)) breakInput = brakeAcceleration * Time.deltaTime; else breakInput = 0;
            steerInput = Input.GetAxis("Horizontal");

            // calculamos el movimiento del coche por las teclas del acelerador y del freno
            moveInput = throttleInput - breakInput;

            Debug.Log("steerInput -> "  + steerInput);
            Debug.Log("moveInput -> "  + moveInput);
        }
    }

    void Move()
    {
        foreach(var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration;
        }
    }

    void Steer()
    {
        foreach(var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || moveInput == 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration;
            }

            // carLights.isBackLightOn = true;
            // carLights.OperateBackLights();
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }

            // carLights.isBackLightOn = false;
            // carLights.OperateBackLights();
        }
    }

    void AnimateWheels()
    {
        foreach(var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }

    // void WheelEffects()
    // {
    //     foreach (var wheel in wheels)
    //     {
    //         //var dirtParticleMainSettings = wheel.smokeParticle.main;

    //         if (Input.GetKey(KeyCode.Space) && wheel.axel == Axel.Rear && wheel.wheelCollider.isGrounded == true && carRb.velocity.magnitude >= 10.0f)
    //         {
    //             wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
    //             wheel.smokeParticle.Emit(1);
    //         }
    //         else
    //         {
    //             wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
    //         }
    //     }
    // }
}