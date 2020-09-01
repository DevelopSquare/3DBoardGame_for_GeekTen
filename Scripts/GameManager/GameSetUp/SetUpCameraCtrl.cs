/*
  Author      森友雅
  LastUpdate  2020/07/11
  Since       2020/07/04
  Contents    N GameSetUp
              SeuUpのMainCameの制御を行う
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputID;
using System.Diagnostics;

namespace MiniMap {

    public class SetUpCameraCtrl : MonoBehaviour
    {
        private GameObject target; // an object to follow
        private InputEventFactory input;

        public Vector3 offset; // offset form the target object

        [SerializeField] private float polarAngle = 45.0f; // angle with y-axis
        [SerializeField] private float azimuthalAngle = 45.0f; // angle with x-axis

        [SerializeField] private float minDistance = 1.0f;
        [SerializeField] private float maxDistance = 7.0f;
        [SerializeField] private float minPolarAngle = -160.0f;
        [SerializeField] private float maxPolarAngle = 160.0f;
        [SerializeField] private float mouseXSensitivity = 2.0f;
        [SerializeField] private float mouseYSensitivity = 2.0f;
        [SerializeField] public float scrollSensitivity = 5f;

        private bool isOnece = true;
        private Vector2 pivot;

        void Start()
        {
            target = GameObject.Find("10-12");
            var inputManager= GameObject.Find("InputManager");
            input = inputManager.GetComponent<InputEventFactory>();
        }

        void LateUpdate()
        {
            if (target != null)
            {
                if (isOnece)
                {
                    Init();
                    isOnece = false;
                }
                var flick = input.GetFlick();
                updatePos(flick);

            }


        }

       


        public void SetSensibility(float sence)
        {
            mouseXSensitivity = sence;
            mouseYSensitivity = sence;
            scrollSensitivity = sence;
        }

        private void Init()
        {
            var flick = input.GetFlick().normalized;

            //updateAngle(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            updateAngle(flick.x, flick.y * -1f);


            //updateDistance(Input.GetAxis("Mouse ScrollWheel"));

            if (target == null) return;

            var lookAtPos = target.transform.position + offset;

            //updatePos(flick, lookAtPos);


            updatePosition(lookAtPos);
            transform.LookAt(lookAtPos);

            ControlPosture();
        }


        void updatePos(Vector2 flick)
        {

            if (Input.GetMouseButton(0))
            {
                float deltaX = flick.x;
                float deltaY = flick.y;


                if (deltaX > 0)
                {
                    transform.RotateAround(target.transform.position, new Vector3(0, 1, 0), scrollSensitivity * 0.1f * Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY));
                }
                if (deltaX < 0)
                {
                    transform.RotateAround(target.transform.position, new Vector3(0, -1, 0), scrollSensitivity * 0.1f * Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY));
                }
                else if(deltaX == 0)
                {

                }
            }

        }

        void updateAngle(float x, float y)
        {
            x = azimuthalAngle - x * mouseXSensitivity;
            azimuthalAngle = Mathf.Repeat(x, 360);

            y = polarAngle - y * mouseYSensitivity;
            polarAngle = Mathf.Repeat(y, 360);//Mathf.Clamp(y, minPolarAngle, maxPolarAngle);
        }



        void updatePosition(Vector3 lookAtPos)
        {
            float magnification = ((float)Screen.width / (float)Screen.height);
            var da = azimuthalAngle * Mathf.Deg2Rad;
            var dp = polarAngle * Mathf.Deg2Rad;

            if (magnification < (9.0f / 20.0f))
            {
                transform.position = new Vector3(
                    26.0f,
                    98.5f,
                   -21.7f);
            }
            else
            {
                transform.position = new Vector3(
                    9.12f,
                    79.14f,
                   -17.01f);
            }
        }

        private void ControlPosture()
        {
            if (polarAngle>180)
            {
            // x軸を軸にして毎秒2度、回転させるQuaternionを作成（変数をrotとする）
            Quaternion rot = Quaternion.AngleAxis(180, Vector3.forward);
            // 現在の自信の回転の情報を取得する。
            Quaternion q = this.transform.rotation;
            // 合成して、自身に設定
            this.transform.rotation = q * rot;
            }
            
        }

    }

}

