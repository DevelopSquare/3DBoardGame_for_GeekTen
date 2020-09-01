/*
  Author      森友雅
  LastUpdate  2020/04/13
  Since       2020/03/16
  Contents    N GameSetUp
              設置可能マスを強調する(強調したいマスにアタッチして使う)
*/

using UnityEngine;

public class SquareEnphasis : MonoBehaviour
{
    public Material selectSquareColor;
    public Material resetSquareColor;


    public void OnTouch()
    {
        this.gameObject.GetComponent<Renderer>().material.color = selectSquareColor.color;
    } 
        

    public void NotOnTouch()
    {
        this.gameObject.GetComponent<Renderer>().material.color = resetSquareColor.color;
    }

    
}
