/*
  Author      高橋泰斗
  LastUpdate  2020/04/09
  Since       2020/03/11
  Contents    SetUpCameraとPlayCameraのテストで使用したScript。
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test01
{
    public class Test : MonoBehaviour
    {
        GameObject Obj1;
        GameObject Obj2;
        GameObject Obj3;

        void Start()
        {
            Debug.Log("Camera Test Start");
            Obj1 = GameObject.Find("SetUpCameraManager").transform.Find("SetUpCamera").gameObject;
            Obj2 = GameObject.Find("Pawn").transform.Find("PlayCamera").gameObject;
            Obj3 = GameObject.Find("King").transform.Find("PlayCamera").gameObject;
        }

        void Update()
        {
            if (Input.GetKeyDown("1"))
            {
                Camera.SetUpCamera sCamera1 = Obj1.GetComponent<Camera.SetUpCamera>();
                Camera.PlayCamera pCamera2 = Obj2.GetComponent<Camera.PlayCamera>();
                Camera.PlayCamera pCamera3 = Obj3.GetComponent<Camera.PlayCamera>();


                Debug.Log("<SetUpCamera> GameObject: " + sCamera1.GetCameraObject() + "Position:" + sCamera1.GetCameraPosition() + " Angle:" + sCamera1.GetCameraAngle());

                sCamera1.ActivateCamera(true);
                pCamera2.ActivateCamera(false);
                pCamera3.ActivateCamera(false);

            }

            if (Input.GetKeyDown("2"))
            {
                Camera.SetUpCamera sCamera1 = Obj1.GetComponent<Camera.SetUpCamera>();
                Camera.PlayCamera pCamera2 = Obj2.GetComponent<Camera.PlayCamera>();
                Camera.PlayCamera pCamera3 = Obj3.GetComponent<Camera.PlayCamera>();

                Debug.Log("<Pawn PlayCamera> GameObject: " + pCamera2.GetCameraObject()  + "Position:" + pCamera2.GetCameraPosition() + " Angle:" + pCamera2.GetCameraAngle());

                sCamera1.ActivateCamera(false);
                pCamera2.ActivateCamera(true);
                pCamera3.ActivateCamera(false);
            }

            if (Input.GetKeyDown("3"))
            {
                Camera.SetUpCamera sCamera1 = Obj1.GetComponent<Camera.SetUpCamera>();
                Camera.PlayCamera pCamera2 = Obj2.GetComponent<Camera.PlayCamera>();
                Camera.PlayCamera pCamera3 = Obj3.GetComponent<Camera.PlayCamera>();

                Debug.Log("<King PlayCamera> GameObject: " + pCamera2.GetCameraObject() + "Position:" + pCamera2.GetCameraPosition() + " Angle:" + pCamera2.GetCameraAngle());

                sCamera1.ActivateCamera(false);
                pCamera2.ActivateCamera(false);
                pCamera3.ActivateCamera(true);
            }
        }
    }

}

