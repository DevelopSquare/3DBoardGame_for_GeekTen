/*
  Author      高橋泰斗
  LastUpdate  2020/04/09
  Since       2020/03/11
  Contents    CameraBaseのScript。
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera
{
    public abstract class CameraBase : MonoBehaviour, ICamera
    {
        protected Vector3 CameraPosition;
        protected Vector3 CameraAngle;

        //①抽象メソッド
        public abstract GameObject GetCameraObject();
        public abstract Vector3 GetCameraPosition();
        public abstract Vector3 GetCameraAngle();
        public abstract void ActivateCamera(bool OnFlg);

        //②意味のない値を返すメソッド
        //public GameObject GetCameraObject()
        //{
        //    return CameraObject;
        //}

        //public Vector3 GetCameraPosition()
        //{
        //    return CameraPosition;
        //}

        //public Vector3 GetCameraAngle()
        //{
        //    return CameraAngle;
        //}

        //public void IsActivateCamera(bool OnFlg)
        //{
        //}
    }
}