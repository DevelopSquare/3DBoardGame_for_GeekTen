/*
  Author      Tanaka Kisuke
  LastUpdate  2020/06/9
  Since       2020/06/10
  Contents     CPAgentsのinterface
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CP
{
    interface ICPAgent 
    {
        /*
        TODO :モデルに入力を行う
        Arg :　List<float>で入力 学習時と同様に行う
        return : none
        */
        void SetObserVation(List<float> observations);


        /*
        TODO :モデルからの入力を取得する
        Arg :　none
        return : List<float>でモデルの出力を受け取る　学習時と同様に受け取ること
        */
        List<float> GetActionVector();


        /*
        TODO : Episodeを終わらせる　やるかどうかは任意でやる
        Arg :　none
        return : none
        */
        void GameSet();
    }
}


