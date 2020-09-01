/*
  Author      森友雅
  LastUpdate  2020/08/30
  Since       2020/08/30
  Contents   ホログラムフィールドのreyのon、offを管理
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Field
{
    public class RayOnOff : MonoBehaviour
    {
        private bool isCalledOnce = false;
        private string tmpSceneName;


        void Start()
        {

        }

        void Update()
        {
            if (SceneManager.GetActiveScene().name != tmpSceneName)
            {
                isCalledOnce = false;
            }


            if (!isCalledOnce)
            {
                isCalledOnce = true;
                tmpSceneName = SceneManager.GetActiveScene().name;

                if (SceneManager.GetActiveScene().name == "PlayGame")
                {
                    this.transform.Find("ray").gameObject.SetActive(true);
                }
                else
                {
                    this.transform.Find("ray").gameObject.SetActive(false);
                }
            }
        }
    }
}