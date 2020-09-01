/*
  Author      田中木介
  LastUpdate  2020/05/28
  Since       2020/05/26
  Contents    GameSetUPのSoundを管理
  
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound;

namespace Sound
{
    public class GameSetUpSound : MonoBehaviour
    {
        private SoundManager sound;

        // Start is called before the first frame update
        void Start()
        {
            sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

            sound.PlayBGM("GameSetUp");

        }


        // Update is called once per frame
        void OnDisable()
        {
            sound.StopBGM("GameSetUp");
        }

        public void OnClickSound()
        {
            sound.PlaySE("normalBotton");
        }
    }
}


