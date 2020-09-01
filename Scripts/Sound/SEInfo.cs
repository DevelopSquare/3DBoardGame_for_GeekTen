/*
  Author      田中木介
  LastUpdate  2020/03/16
  Since       2020/03/14
  Contents    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace Sound{

    public class SEInfo 
    {
        public Dictionary<string , AudioClip> SeDict= new Dictionary<string, AudioClip>();

        // Start is called before the first frame update

        public Dictionary<string, AudioClip> SeDictionary
        {
            get { return SeDict; }
        }

        public void AddSE(string resourceName, string seName)
        {
            //AudioClip clip;
            SeDict[seName] = Resources.Load("Sound/SE/" + resourceName) as AudioClip;
        }

    }


}



