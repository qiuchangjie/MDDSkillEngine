using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Werewolf.StatusIndicators.Components;

namespace MDDSkillEngine
{
    public class CharacterSelect : MonoBehaviour
    {
        public SplatManager Splats { get; set; }

        void Start()
        {
            Splats = GetComponent<SplatManager>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Splats.CancelSpellIndicator();
                Splats.CancelRangeIndicator();
                Splats.CancelStatusIndicator();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Splats.SelectSpellIndicator("Cone Basic");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Splats.SelectSpellIndicator("Point Basic");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Splats.SelectSpellIndicator("RangeBasic");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Splats.SelectSpellIndicator("Line");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Splats.SelectStatusIndicator("Status");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Splats.SelectStatusIndicator("Status Basic");
            }
        }
    }
}




