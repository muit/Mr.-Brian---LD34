using UnityEngine;
using System.Collections;
using Crab;
using Crab.Components;


namespace Crab.Controllers
{
    [RequireComponent(typeof(CMovement))]
    public class PlayerController : EntityController
    {
        private CMovement movement;
        public Animator greenButton;
        public Animator blueButton;
        void Awake()
        {
            me = GetComponent<Entity>();
            movement = me.Movement;
        }

        void Update()
        {
            //Green Button
            if (Input.GetKeyDown(KeyCode.Q))
            {
                greenButton.SetBool("pressed", true);
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                greenButton.SetBool("pressed", false);
            }

            //Blue Button
            if (Input.GetKeyDown(KeyCode.E))
            {
                blueButton.SetBool("pressed", true);
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                blueButton.SetBool("pressed", false);
            }
        }
    }
}