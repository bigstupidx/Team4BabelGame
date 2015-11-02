﻿using System.Collections;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Environment.Levers.LeverExample.Scripts
{
    public class LeverPulls : MonoBehaviour, IInteractable
    {
        private Color _oldColor;
        private bool _hasBeenPulled;
        public bool Timelimit = false;
        public int SecForLever = 5;
        public GameObject PlayerPos;

        public Vector3 InteractPosition(Vector3 ai)
        {
            return PlayerPos.transform.position;
        }

        void Start()
        {
            _oldColor = GetComponent<Renderer>().material.color;
        }

        public bool HasBeenActivated()
        {
            return _hasBeenPulled;
        }

        public GameObject Interact(GameObject pickup)
        {
            if(CanThisBeInteractedWith(pickup))
                StartCoroutine(ChangeColor());
            return pickup;
        }

        public bool CanThisBeInteractedWith(GameObject pickup)
        {
            return !_hasBeenPulled;
        }

        IEnumerator ChangeColor()
        {
            GetComponent<Renderer>().material.color = Color.red;
            _hasBeenPulled = true;
            if (Timelimit)
            {
                yield return new WaitForSeconds(SecForLever);
                GetComponent<Renderer>().material.color = _oldColor;
                _hasBeenPulled = false;
            }
        }
    }
}
