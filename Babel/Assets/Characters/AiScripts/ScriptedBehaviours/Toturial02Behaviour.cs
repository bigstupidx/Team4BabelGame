﻿using System;






{
    public class Toturial02Behaviour : MonoBehaviour



        private Transform waypoint01;
        private Transform waypoint02;



        // Use this for initialization
        void Start ()
        {
            var colider = gameObject.AddComponent<SphereCollider>();
            colider.radius = 5;





            var waypoints = GameObject.FindGameObjectWithTag(Constants.Tags.GameMaster).transform.FindChild("Waypoints");
            waypoint01 = waypoints.FindChild("waypoint");
            waypoint02 = waypoints.FindChild("waypoint (1)");



        // Update is called once per frame
        void Update () {
            _agent.speed = _movementSpeed;
            
                // TODO: waving animation
                bubble.ActivateSidekickSignBubble(new List<int>(new[] { 8 }));
                _state = State.Second;
                // TODO: waving animation
        
            if(other.tag != Constants.Tags.Player || !_waitingForPlayer) return;
                case State.First:
                    // TODO: waving animation
                    CreateNewSymbol.SymbolID = 8;
                    GameObject.FindGameObjectWithTag(Constants.Tags.GameUI).GetComponent<UIControl>().SignCreationEnter();

                    break;
                case State.Second:
                    _agent.destination = waypoint02.position;
                    break;
            }
        }
        
            First, Second, Thrid
    }
}
