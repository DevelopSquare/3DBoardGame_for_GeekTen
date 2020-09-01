/*
  Author      Tanaka Kisuke
  LastUpdate  2020/06/9
  Since       2020/06/10
  Contents     Mlagentsのモデルの入出力を受け付ける
               ゲームオブジェクトにアッタッチしてDecisionRequesterをアッタッチする
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using System;

namespace CP
{
    public class CPAgent : Agent
    {
        private List<float> Actions = new List<float>();
        private List<float> Observations = new List<float>();

        private bool IsGame = false;


        public override void Initialize()
        {
            Debug.Log("Initialize Agent");
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            //Debug.Log("Collect Observation");
            //Debug.Log("Length og sensor : "+String.Join(",", sensor.GetObservationShape()));
            for (int i = 0; i < Observations.Count; i++)
            {
                
                sensor.AddObservation(Observations[i]);
            }
        }

        public override void OnActionReceived(float[] vectorAction)
        {
            //Debug.Log("Action Received : " + String.Join(",", vectorAction));
            var actions = new List<float>();
            for(int i = 0; i < vectorAction.Length; i++)
            {
                actions.Add(vectorAction[i]);
            }

            Actions = actions;

            if (IsGame)
            {
                IsGame = false;
                EndEpisode();
            }
        }

        public override void OnEpisodeBegin()
        {
            Debug.Log("Episode Begin");
        }

        public void SetObserVation(List<float> observations)
        {
            Observations = observations;
        }

        public List<float> GetActionVector()
        {

            return Actions;
        }

        public void GameSet()
        {
            IsGame = true;
        }
    }
}


