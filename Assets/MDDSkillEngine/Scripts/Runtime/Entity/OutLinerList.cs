using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class OutLinerList : MonoBehaviour
    {
        public List<Outline> outlines = new List<Outline>();

        public void SetOutLiner(bool isOpen)
        {
            for (int i = 0; i < outlines.Count; i++)
            {
                outlines[i].eraseRenderer = !isOpen;
            }
        }

        public void SetOutLinerColor(int color)
        {
            for (int i = 0; i < outlines.Count; i++)
            {
                outlines[i].color = color;
            }
        }
    }
}


