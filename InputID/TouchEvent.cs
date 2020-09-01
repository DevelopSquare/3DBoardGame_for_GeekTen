/*
  Author      森友雅
  LastUpdate  2020/03/24
  Since       2020/03/14
  Contents    入力情報に関するスクリプト
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputID
{
    public class TouchEvent : InputEvent
    {


        public override float GetTouchDeltaTime()
        {
            return 0;
        }


        public int GetTouchObjectId()
        {    
            return 0;
        }


    }
}