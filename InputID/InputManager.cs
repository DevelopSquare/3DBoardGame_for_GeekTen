/*
  Author      森友雅
  LastUpdate  2020/05/02
  Since       2020/03/14
  Contents    オブジェクトにアタッチして使用
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputID
{
    public class InputManager : MonoBehaviour, IInput
    {


        private InputEventFactory inputeventfactory;


        void Start()
        {
            inputeventfactory = GetComponent<InputEventFactory>();         
        }


        public Vector2 GetFlickListner()
        {
            Vector2 direction = inputeventfactory.GetFlick();
           
            return direction;
        }


        public GameObject GetTouchListner()
        {
            GameObject objectId = inputeventfactory.GetTouch();

            return objectId;
        }


        public void RockOn()
        {
            inputeventfactory.rock = true;
        }

        public void RockOff()
        {
            inputeventfactory.rock = false;
        }
    }


}