/*
  Author      高橋泰斗
  LastUpdate  2020/04/09
  Since       2020/03/11
  Contents    ICameraのScript。
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera
{
    interface ICamera
    {
        Vector3 GetCameraPosition();
        Vector3 GetCameraAngle();
        GameObject GetCameraObject();
        void ActivateCamera(bool OnFlg);
    }
}