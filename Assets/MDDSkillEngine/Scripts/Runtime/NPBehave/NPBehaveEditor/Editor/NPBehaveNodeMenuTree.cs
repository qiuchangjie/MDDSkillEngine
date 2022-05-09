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
        [TabGroup("节点数据",true)]
        public OdinMenuTree tree;

        NPBehaveGraph ins;

        [InfoBox("这是这个Skill级的所有黑板数据\n键为string，值为Variable子类\n如果要添加新的黑板数据类型，请参照Variable文件夹下的定义")]
        [Title("黑板数据", TitleAlignment = TitleAlignments.Centered)]
        [LabelText("黑板内容")]
        [TabGroup("黑板数据")]
        [DictionaryDrawerSettings(KeyLabel = "键(string)", ValueLabel = "值(Variable)")]
        [OdinSerialize]
        public Dictionary<string, Variable> BBValues = new Dictionary<string, Variable>();

        [InfoBox("角色级公共黑板数据，如果为空则认为没有关联该技能没有关联公共黑板")]
        [Title("黑板数据", TitleAlignment = TitleAlignments.Centered)]
        [LabelText("黑板内容")]
        [BoxGroup]
        [DictionaryDrawerSettings(KeyLabel = "键(string)", ValueLabel = "值(Variable)")]
        public Dictionary<string, Variable> PublicBBValues = new Dictionary<string, Variable>();

        public void setins(NPBehaveGraph ins)
        {
            this.ins = ins;
            BBValues = ins.BBValues;
            
            //看是否有卡尔公共黑板关联
            KaelBlackboard kaelBlackboard = ins.PublicBB as KaelBlackboard;
            if (kaelBlackboard != null)
            {
                PublicBBValues = kaelBlackboard.BBValues;
            }
        }

        public static NPBehaveNodeMenuTree OpenWindow()
        {
            var window = GetWindow<NPBehaveNodeMenuTree>();
            window.titleContent = new GUIContent("辅助用菜单");
            return window;
        }

        public void Awake()
        {
            Init();
        }

      
        OdinMenuItem selectItem;
        bool isDraging;

        protected override void OnGUI()
        {
            base.OnGUI();

            foreach (var item in tree.EnumerateTree())
            {
                if (item.IsSelected)
                {
                    if (item != selectItem)
                    {
                        selectItem = item;
                    }                  
                }
            }


            Event e = Event.current;
            int cid = GUIUtility.GetControlID(FocusType.Passive);
            switch (e.GetTypeForControl(cid))
            {
                case EventType.MouseDown:
                    GUIUtility.hotControl = cid;

                    e.Use();
                    break;
                case EventType.MouseUp:
                    e.Use();
                    break;
                case EventType.MouseDrag:

                    if (selectItem == null)
                        return;

                    DragAndDrop.PrepareStartDrag();
                    DragAndDrop.SetGenericData("dragflag", selectItem.Value);
                    DragAndDrop.StartDrag("dragtitle"); 
                    
                    isDraging = true;
                    e.Use();
                    break;
                case EventType.DragUpdated:

                    e.Use();
                    break;
                case EventType.DragPerform:

                    e.Use();
                    break;
                case EventType.DragExited:

                    if (GUIUtility.hotControl == cid)
                        GUIUtility.hotControl = 0;
                    isDraging = false;

                    e.Use();
                    break;
            }          
        }


        private void Init()
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

            tree.Add("组合节点",null);

            tree.Add("描述节点",null);

            tree.Add("任务节点",null);

            for (int i = 0; i < list.Count; i++)
            {
                if (typeof(NP_CompositeNodeBase).IsAssignableFrom(list[i].GetType()))
                {
                    tree.Add("组合节点/" + list[i].Name, list[i]);
                    continue;
                }

                if (typeof(NP_DecoratorNodeBase).IsAssignableFrom(list[i].GetType()))
                {
                    tree.Add("描述节点/" + list[i].Name, list[i]);
                    continue;
                }

                if (typeof(NP_TaskNodeBase).IsAssignableFrom(list[i].GetType()))
                {
                    tree.Add("任务节点/" + list[i].Name, list[i]);
                    continue;
                }

                tree.Add(list[i].Name, list[i]);
            }

        }


    }


}

#endif
