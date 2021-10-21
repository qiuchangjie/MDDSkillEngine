﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using NPBehave;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace NPBehave.node
{
	public abstract class NP_NodeBase : XNode.Node
	{

		public int Id;


        public List<int> linkedID = new List<int>();

        public virtual string Name => GetType().Name;

        /// <summary>
        /// 获取结点
        /// </summary>
        /// <returns></returns>
        public abstract MDDGameFramework.Node NP_GetNode();


        /// <summary>
        /// 创建组合结点
        /// </summary>
        /// <returns></returns>
        public virtual Composite CreateCompositeNode(NP_Tree owner_Tree,MDDGameFramework.Node[] nodes)
        {
            return null;
        }

        /// <summary>
        /// 创建装饰结点
        /// </summary>
        /// <param name="unitId">行为树归属的Unit</param>
        /// <param name="runtimeTree">运行时归属的行为树</param>
        /// <param name="node">所装饰的结点</param>
        /// <returns></returns>
        public virtual Decorator CreateDecoratorNode(NP_Tree owner_Tree, MDDGameFramework.Node node)
        {
            return null;
        }

        /// <summary>
        /// 创建任务节点
        /// </summary>
        /// <param name="unitId">行为树归属的Unit</param>
        /// <param name="runtimeTree">运行时归属的行为树</param>
        /// <returns></returns>
        public virtual Task CreateTask(NP_Tree owner_Tree)
        {
            return null;
        }
    }
}
