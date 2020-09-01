/*
  Author      高橋泰斗
  LastUpdate  2020/08/29
  Since       2020/08/24
  Contents    Title画面
               ・画面の更新
               ・各種スマホ画面への対応
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GameManager.TitleManager
{
    class Title : StateBase, ITitle
    {
        private GameObject center; //円運動の中心
        private GameObject field;  //円運動の対象
        float speed = 13; //円運動の速度

        private GameObject main_camera;  //メインカメラ


        void Start()
        {
            center = GameObject.Find("Center");
            field = GameObject.Find("Field");
            main_camera = GameObject.Find("Main Camera");


            CameraAutoDistanceAdjust();
        }


        void Update()
        {
            UpdateFieldRotation();
        }


        //スタートボタン
        public void StartGame()
        {
            SceneManager.LoadScene("GameSetUp");
        }


        //フィールドの回転演出
        void UpdateFieldRotation()
        {
            if (center != null && field != null)
            {
                //フィールドの円運動（見かけ上は自転）
                field.transform.RotateAround(center.transform.position, field.transform.up, speed * Time.deltaTime);
            }
        }


        //スマホ画面への対応
        void CameraAutoDistanceAdjust()
        {
            //画面縦横比に応じたカメラ・フィールド間距離調整

            Vector3 pos = main_camera.transform.position;

            float FarthestZ = pos.z + 5; //カメラ・フィールド間が最も遠い時
            float magnification = ((float)Screen.height / (float)Screen.width) - 7.0f / 9.0f; //9:16を基準とした倍率
            float new_z = pos.z * magnification; //新しいz座標

            if (new_z < FarthestZ)
            {
                pos.z = new_z;
                main_camera.transform.position = pos;
            }
        }
    }
}