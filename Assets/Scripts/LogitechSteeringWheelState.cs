using UnityEngine;
using System.Collections;
using System.Text;

public class LogitechSteeringWheelState : MonoBehaviour
{

    LogitechGSDK.LogiControllerPropertiesData properties;
    private string actualState;
    private string activeForces;
    private string propertiesEdit;
    private string buttonStatus;
    private string forcesLabel;
    string[] activeForceAndEffect;

    // Use this for initialization
    void Start()
    {
        activeForces = "";
        propertiesEdit = "";
        actualState = "";
        buttonStatus = "";
        activeForceAndEffect = new string[9];
        Debug.Log("SteeringInit:" + LogitechGSDK.LogiSteeringInitialize(false));
    }

    void OnApplicationQuit()
    {
        Debug.Log("SteeringShutdown:" + LogitechGSDK.LogiSteeringShutdown());
    }

    void OnGUI()
    {
        actualState = GUI.TextArea(new Rect(10, 10, 180, 200), actualState, 400);
        GUI.Label(new Rect(10, 400, 800, 400), forcesLabel);
    }

    // Update is called once per frame
    void Update()
    {
        //All the test functions are called on the first device plugged in(index = 0)
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {

   
            //CONTROLLER STATE
            actualState = "Steering wheel current state : \n\n";
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);
            actualState += "volante :" + rec.lX + "\n";
            actualState += "acelerador :" + rec.lY + "\n";
            actualState += "freno :" + rec.lRz + "\n";
            switch (rec.rgdwPOV[0])
            {
                case (0): actualState += "POV : UP\n"; break;
                case (4500): actualState += "POV : UP-RIGHT\n"; break;
                case (9000): actualState += "POV : RIGHT\n"; break;
                case (13500): actualState += "POV : DOWN-RIGHT\n"; break;
                case (18000): actualState += "POV : DOWN\n"; break;
                case (22500): actualState += "POV : DOWN-LEFT\n"; break;
                case (27000): actualState += "POV : LEFT\n"; break;
                case (31500): actualState += "POV : UP-LEFT\n"; break;
                default: actualState += "POV : CENTER\n"; break;
            }

        }
        else if (!LogitechGSDK.LogiIsConnected(0))
        {
            actualState = "PLEASE PLUG IN A STEERING WHEEL OR A FORCE FEEDBACK CONTROLLER";
        }
        else
        {
            actualState = "THIS WINDOW NEEDS TO BE IN FOREGROUND IN ORDER FOR THE SDK TO WORK PROPERLY";
        }
    }



}
