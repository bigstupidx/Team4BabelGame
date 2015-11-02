﻿using System;
using Assets.Characters.AiScripts;
using Assets.Characters.AiScripts.States;
using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.Characters.SideKick.Scripts
{
    public class SidekickControls : MonoBehaviour
    {
        #region Instatiate
        
        private GameObject _sideKick;
        private NavMeshAgent _sideKickAgent;
        private AiMovement _sideKickMovement;
        private GameObject _player;

        // Use this for initialization
        void Start()
        {
            _sideKick = GameObject.FindGameObjectWithTag(Constants.Tags.SideKick);
            _sideKickAgent = _sideKick.GetComponent<NavMeshAgent>();
            _sideKickMovement = _sideKick.GetComponent<AiMovement>();
            _player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
        }

        #endregion


        public void ExecuteAction(int i)
        {
            switch (i)
            {
                case 6:
                    _sideKickMovement.AssignNewState(new InteractWithNearestState(_sideKickAgent, Constants.Tags.Lever));
                    return;
                case 9:
                    _sideKickMovement.AssignNewState(new PickupItemState(_sideKickAgent, Constants.Tags.Stick));
                    return;
                case 10:
                    _sideKickMovement.AssignNewState(new PickupItemState(_sideKickAgent, Constants.Tags.Key));
                    return;
                case 11:
                    _sideKickMovement.AssignNewState(new PickupItemState(_sideKickAgent, Constants.Tags.Torch));
                    return;
                case 12:
                    _sideKickMovement.AssignNewState(new PickupItemState(_sideKickAgent, Constants.Tags.Bottle));
                    return;
                case 17:
                    _sideKickMovement.AssignNewState(new GoSomewhereAndWaitState(_sideKickAgent,
                        _player.transform.position));
                    return;
                case 18:
                    _sideKickMovement.AssignNewState(new FollowThisState(_sideKickAgent, _player.gameObject));
                    return;
                case 20:
                    _sideKickMovement.AssignNewState(new TradeState(_sideKickAgent, _player.GetComponent<NavMeshAgent>()));
                    return;
                case 21:
                    _sideKickMovement.AssignNewState(new InteractWithNearestState(_sideKickAgent, Constants.Tags.Keyhole));
                    return;
                default:
                    throw new Exception("That action is not implemented yet!");
            }   
            
        }
    }
}