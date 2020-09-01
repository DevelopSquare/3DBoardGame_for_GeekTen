/*
  Author      森友雅
  LastUpdate  2020/05/28
  Since       2020/03/14
  Contents    N InputIDのインターフェイス
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputID
{
    interface IInput
    {
        Vector2 GetFlickListner();

        GameObject GetTouchListner();
    }

}