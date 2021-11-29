#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Sirenix.Serialization;
using MDDGameFramework;
using System.IO;
using System;
using Sirenix.OdinInspector;
using System.Linq;
using Sirenix.Utilities;

namespace MDDSkillEngine
{
    public class NPBehaveNodeMenuTree : OdinEditorWindow
    {

        public OdinMenuTree tree;

        public static NPBehaveNodeMenuTree OpenWindow()
        {
            var window = GetWindow<NPBehaveNodeMenuTree>();

            return window;
        }


        public void Awake()
        {
            tree = new OdinMenuTree(supportsMultiSelect: true);

            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;

            List<Type> types = new List<Type>();

            Utility.Assembly.GetTypesByFather(types, typeof(NP_NodeBase));

            List<NP_NodeBase> list = new List<NP_NodeBase>();

            for (int i = 0; i < types.Count; i++)
            {
                list.Add(Activator.CreateInstance(types[i]) as NP_NodeBase);
            }

            for (int i = 0; i < list.Count; i++)
            {
                tree.Add(list[i].Name,list[i]);
            }

            foreach (var item in tree.EnumerateTree())
            {
                AddDragHandles(item);
            }

        }

        protected override void OnGUI()
        {
            base.OnGUI();
            DragAndDropUtilities.DrawDropZone(new Rect(100, 100, 100, 100), null, null, 1);
        }

        private void AddDragHandles(OdinMenuItem menuItem)
        {
            menuItem.OnDrawItem += x => DragAndDropUtilities.DragZone(menuItem.Rect, menuItem.Value, true, true);
        }
    }

    public class NodeTable
    {
        [TableList(IsReadOnly = true, AlwaysExpanded = true), ShowInInspector]
        public readonly List<NP_NodeBaseWarper> list = new List<NP_NodeBaseWarper>();

        public NodeTable(List<NP_NodeBase> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                NP_NodeBaseWarper baseWarper = new NP_NodeBaseWarper();
                baseWarper.nP = list[i];
                this.list.Add(baseWarper);
            }
        }

        public class NP_NodeBaseWarper
        {
            public NP_NodeBase nP;
        }
    }
}

#endif
