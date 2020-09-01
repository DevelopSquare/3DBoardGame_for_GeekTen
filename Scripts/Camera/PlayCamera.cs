/*
  Author      高橋泰斗
  LastUpdate  2020/04/09
  Since       2020/03/11
  Contents    PlayCameraのScript。SetUpCameraとほぼ同じ内容。
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera
{
    class PlayCamera : CameraBase
    {
        [SerializeField] private GameObject PlayCameraObj; //インスペクターで駒視点カメラを紐づける

        public override GameObject GetCameraObject()
        {
            return PlayCameraObj;
        }

        public override Vector3 GetCameraPosition()
        {
            CameraPosition = PlayCameraObj.transform.position;
            return CameraPosition;
        }

        public override Vector3 GetCameraAngle()
        {
            CameraAngle = PlayCameraObj.transform.eulerAngles;
            return CameraAngle;
        }

        public override void ActivateCamera(bool OnFlg)
        {
            //①カメラオブジェクトをアクティブ/非アクティブ 
            PlayCameraObj.SetActive(OnFlg);

            //②カメラオブジェクトのカメラコンポネントをアクティブ/非アクティブ
            //PlayCameraObj.GetComponent<UnityEngine.Camera>().enabled = OnFlg;
            //Debug.Log("PlayCamera"+PlayCameraObj.GetComponent<UnityEngine.Camera>().enabled);
        }
    }
}