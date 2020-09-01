/*
  Author      田中木介
  LastUpdate  2020/03/16
  Since       2020/03/14
  Contents    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public interface ISound 
    {
        void PlayBGM(string bgmName);

        void StopBGM(string bgmName);

        void PlaySE(string bgmId);
    }


}

