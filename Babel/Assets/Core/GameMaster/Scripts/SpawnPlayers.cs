﻿using Assets.Characters.Player.Scripts;
using Assets.Characters.SideKick.Scripts;
using Assets.Core.Configuration;
using UnityEngine;

namespace Assets.Core.GameMaster.Scripts
{
    public class SpawnPlayers : MonoBehaviour
    {
        public GameObject CharactorPrefab;

        public Transform PlayerSpawnPoint;
        public Transform SidekickSpawnPoint;
        
        // Use this for initialization
        void Awake ()
        {
            var sidekick = (GameObject) Instantiate(CharactorPrefab, SidekickSpawnPoint.position, Quaternion.identity);
            var player = (GameObject) Instantiate(CharactorPrefab, PlayerSpawnPoint.position, Quaternion.identity);

            sidekick.tag = Constants.Tags.SideKick;
            sidekick.name = Constants.Tags.SideKick;
            sidekick.GetComponent<PlayerMovement>().enabled = false;

            player.GetComponent<SidekickControls>().enabled = false;
            player.name = Constants.Tags.Player;
        }

        // Update is called once per frame
        void Update () {
	
        }
    }
}