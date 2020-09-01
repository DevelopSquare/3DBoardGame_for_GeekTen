/*
  Author      高橋泰斗
  LastUpdate  2020/04/09
  Since       2020/03/11
  Contents    SetUpCameraのScript。PlayCameraとほぼ同じ内容。
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera
{
    class SetUpCamera : CameraBase
    {
        [SerializeField] private GameObject SetUpCameraObj; //インスペクターでセットアップカメラを紐づける

        public override GameObject GetCameraObject()
        {
            return SetUpCameraObj;
        }

        public override Vector3 GetCameraPosition()
        {
            CameraPosition = SetUpCameraObj.transform.position;
            return CameraPosition;
        }

        public override Vector3 GetCameraAngle()
        {
            CameraAngle = SetUpCameraObj.transform.eulerAngles;
            return CameraAngle;
        }

        public override void ActivateCamera(bool OnFlg)
        {
            //①カメラオブジェクトをアクティブ/非アクティブ
            SetUpCameraObj.SetActive(OnFlg);

            //②カメラコンポネントをアクティブ/非アクティブ
            //SetUpCameraObj.GetComponent<UnityEngine.Camera>().enabled = OnFlg;
            //Debug.Log("SetUpCamera:" + SetUpCameraObj.GetComponent<UnityEngine.Camera>().enabled);


        }
    }
}