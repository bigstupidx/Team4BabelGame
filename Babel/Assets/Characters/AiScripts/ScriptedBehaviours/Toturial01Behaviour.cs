﻿using Assets.Characters.SideKick.Scripts;




{
    public class Toturial01Behaviour : MonoBehaviour






        // Use this for initialization
        void Start ()
        {
            var colider = gameObject.AddComponent<SphereCollider>();
            colider.radius = 5;




	
        // Update is called once per frame
        void Update () {
            _agent.speed = movementSpeed;
            
                // TODO: waving animation
                // TODO: Say symbol: 8
                Debug.Log("Say symbol: 8");

                state = State.Second;
                // TODO: Say symbol: 8
                Debug.Log("Say symbol: 8");
        
            if(other.tag != Constants.Tags.Player || !waitingForPlayer) return;
                case State.First:
                    // TODO: waving animation
                    CreateNewSymbol.SymbolID = 8;
                    GameObject.FindGameObjectWithTag(Constants.Tags.GameUI).GetComponent<UIControl>().SignCreationEnter();
                    _agent.destination = new Vector3(17,1,17);
                    break;
                case State.Second:
                    _agent.destination = new Vector3(35, 1, -8);
                    break;
                case State.Thrid:
                    if (Vector3.Distance(_agent.transform.position, new Vector3(35, 1, -8)) > 2) return;
                    Debug.Log("Say symbol: 8");
                    // TODO: waving animation
                    // TODO: Say symbol: 8
                    break;
            }
        }
        
            First, Second, Thrid
    }
}
