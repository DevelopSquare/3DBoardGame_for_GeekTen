﻿/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Player;
using Unity.MLAgents.Sensors;
using Piece;
using Field;

    public class PieceAgent : Agent
　　　{
        // Start is called before the first frame update
        void Start()
        {

        }

        //Start Episode
        public Transform Target;
        public override void OnEpisodeBegin()
        {
            
            if (this.transform.localPosition.y < 0)
            {
                // If the Agent fell, zero its momentum
                this.rBody.angularVelocity = Vector3.zero;
                this.rBody.velocity = Vector3.zero;
                this.transform.localPosition = new Vector3(0, 0.5f, 0);
                
            }

            // Move the target to a new spot
            Target.localPosition = new Vector3(Random.value * 8 - 4,
                                               0.5f,
                                               Random.value * 8 - 4);
        }

        //Start Observation
        public override void CollectObservations(VectorSensor sensor)
        {
            
            // Target and Agent positions
            sensor.AddObservation(Target.localPosition);
            sensor.AddObservation(this.transform.localPosition);

            // Agent velocity
            sensor.AddObservation(rBody.velocity.x);
            sensor.AddObservation(rBody.velocity.z);
            
        }



        // make acition and get reward
        public override void OnActionReceived(float[] vectorAction)
        {
            
            // Actions, size = 2
            Vector3 controlSignal = Vector3.zero;
            controlSignal.x = vectorAction[0];
            controlSignal.z = vectorAction[1];
            rBody.AddForce(controlSignal * speed);

            // Rewards
            float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

            // Reached target
            if (distanceToTarget < 1.42f)
            {
                SetReward(1.0f);
                EndEpisode();
            }

            // Fell off platform
            if (this.transform.localPosition.y < 0)
            {
                EndEpisode();
            }
            
        }

    }
    */

