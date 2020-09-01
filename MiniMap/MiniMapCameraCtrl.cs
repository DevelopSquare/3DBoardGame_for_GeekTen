/*
  Author      田中木介
  LastUpdate  2020/05/09
  LastUpdate  2020/08/30(Distanceの自動調節　by髙橋)
  Since       2020/05/09
  Contents    MiniMapCameraの制御
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputID;
namespace MiniMap {

    public class MiniMapCameraCtrl : MonoBehaviour
    {
        private GameObject target; // an object to follow
        private InputEventFactory input;

        public Vector3 offset; // offset form the target object

        [SerializeField] private float distance = 100.0f; // distance from following object
        [SerializeField] private float polarAngle = 45.0f; // angle with y-axis
        [SerializeField] private float azimuthalAngle = 45.0f; // angle with x-axis

        [SerializeField] private float minDistance = 1.0f;
        [SerializeField] private float maxDistance = 7.0f;
        [SerializeField] private float minPolarAngle = -160.0f;
        [SerializeField] private float maxPolarAngle = 160.0f;
        [SerializeField] private float mouseXSensitivity = 2.0f;
        [SerializeField] private float mouseYSensitivity = 2.0f;
        [SerializeField] public float scrollSensitivity = 2.0f;

        private bool isOnece = true;
        private Vector2 pivot;

        void Start()
        {
            target = GameObject.Find("MiniMapField");
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

        void ZoomCamera(float delta)
        {
            distance += delta;
        }

        void updatePos(Vector2 flick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pivot = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            if (Input.GetMouseButton(0))
            {
                float deltaX = flick.x;//Input.mousePosition.x - pivot.x;
                float deltaY = flick.y;//Input.mousePosition.y - pivot.y;
                pivot = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                transform.RotateAround(GetCenterPosition(target.transform) + offset, (transform.up * deltaX + transform.right * (-deltaY)).normalized, scrollSensitivity *0.1f* Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY));
            }

        }

        void updateAngle(float x, float y)
        {
            x = azimuthalAngle - x * mouseXSensitivity;
            azimuthalAngle = Mathf.Repeat(x, 360);

            y = polarAngle - y * mouseYSensitivity;
            polarAngle = Mathf.Repeat(y, 360);//Mathf.Clamp(y, minPolarAngle, maxPolarAngle);
        }

        //void updateDistance(float scroll)
        //{
        //    scroll = distance - scroll * scrollSensitivity;
        //    distance = Mathf.Clamp(scroll, minDistance, maxDistance);
        //}

        void updatePosition(Vector3 lookAtPos)
        {
            //スマホ画面に応じてカメラ・フィールド間距離を調整（髙橋）
            float screen_ratio = ((float)Screen.width / (float)Screen.height);

            if (screen_ratio < (9.0f / 20.0f))
            {
                distance = distance + 15;
            }
            else if(screen_ratio < (8.9f / 16.0f))
            {
                distance = distance +7;
            }
            

            var da = azimuthalAngle * Mathf.Deg2Rad;
            var dp = polarAngle * Mathf.Deg2Rad;
            transform.position = new Vector3(
                lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
                lookAtPos.y + distance * Mathf.Cos(dp),
                lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da));
        }

        //void updatePosition(Vector3 lookAtPos)
        //{
        //    var da = azimuthalAngle * Mathf.Deg2Rad;
        //    var dp = polarAngle * Mathf.Deg2Rad;
        //    transform.position = new Vector3(
        //        lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
        //        lookAtPos.y + distance * Mathf.Cos(dp),
        //        lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da));
        //}

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

        public  Vector3 GetCenterPosition( Transform target)
        {
            //非アクティブも含めて、targetとtargetの子全てのレンダラーとコライダーを取得
            var cols = target.GetComponentsInChildren<Collider>(true);
            var rens = target.GetComponentsInChildren<Renderer>(true);

            //コライダーとレンダラーが１つもなければ、target.positionがcenterになる
            if (cols.Length == 0 && rens.Length == 0)
                return target.position;

            bool isInit = false;

            Vector3 minPos = Vector3.zero;
            Vector3 maxPos = Vector3.zero;

            for (int i = 0; i < cols.Length; i++)
            {
                var bounds = cols[i].bounds;
                var center = bounds.center;
                var size = bounds.size / 2;

                //最初の１度だけ通って、minPosとmaxPosを初期化する
                if (!isInit)
                {
                    minPos.x = center.x - size.x;
                    minPos.y = center.y - size.y;
                    minPos.z = center.z - size.z;
                    maxPos.x = center.x + size.x;
                    maxPos.y = center.y + size.y;
                    maxPos.z = center.z + size.z;

                    isInit = true;
                    continue;
                }

                if (minPos.x > center.x - size.x) minPos.x = center.x - size.x;
                if (minPos.y > center.y - size.y) minPos.y = center.y - size.y;
                if (minPos.z > center.z - size.z) minPos.z = center.z - size.z;
                if (maxPos.x < center.x + size.x) maxPos.x = center.x + size.x;
                if (maxPos.y < center.y + size.y) maxPos.y = center.y + size.y;
                if (maxPos.z < center.z + size.z) maxPos.z = center.z + size.z;
            }
            for (int i = 0; i < rens.Length; i++)
            {
                var bounds = rens[i].bounds;
                var center = bounds.center;
                var size = bounds.size / 2;

                //コライダーが１つもなければ１度だけ通って、minPosとmaxPosを初期化する
                if (!isInit)
                {
                    minPos.x = center.x - size.x;
                    minPos.y = center.y - size.y;
                    minPos.z = center.z - size.z;
                    maxPos.x = center.x + size.x;
                    maxPos.y = center.y + size.y;
                    maxPos.z = center.z + size.z;

                    isInit = true;
                    continue;
                }

                if (minPos.x > center.x - size.x) minPos.x = center.x - size.x;
                if (minPos.y > center.y - size.y) minPos.y = center.y - size.y;
                if (minPos.z > center.z - size.z) minPos.z = center.z - size.z;
                if (maxPos.x < center.x + size.x) maxPos.x = center.x + size.x;
                if (maxPos.y < center.y + size.y) maxPos.y = center.y + size.y;
                if (maxPos.z < center.z + size.z) maxPos.z = center.z + size.z;
            }

            return (minPos + maxPos) / 2;
        }
    }

}

