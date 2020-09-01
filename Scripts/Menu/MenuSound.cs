using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound;

namespace Menu
{
    public class MenuSound : MonoBehaviour
    {
        private SoundManager sound;
        // Start is called before the first frame update
        void Start()
        {
            sound = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        }

        public void OnClickSound()
        {
            sound.PlaySE("normalBotton");
        }
    }
}


