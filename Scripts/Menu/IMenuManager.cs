/*
  Author      田中木介
  LastUpdate  2020/0
  Since       2020/05/02
  Contents    MessageManagerのinterface
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menue
{
    interface IMenueManager 
    {
        GameObject SetMessage(GameObject mObj,Vector2 messagePosition);
    }
}



