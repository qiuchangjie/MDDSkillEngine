using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;

namespace MDDGameFramework
{
    [BoxGroup("结点数据")]
    public abstract class NP_NodeDataBase 
    {

        /// <summary>
        /// 获取结点
        /// </summary>
        /// <returns></returns>
        public virtual Node NP_GetNode()
        {
            return null;
        }


        /// <summary>
        /// 创建组合结点
        /// </summary>
        /// <returns></returns>
        public virtual Composite CreateComposite(Node[] nodes)
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
        public virtual Decorator CreateDecoratorNode(object m_object, NP_Tree runtimeTree, Node node)
        {
            return null;
        }

        /// <summary>
        /// 创建任务节点
        /// </summary>
        /// <param name="unitId">行为树归属的Unit</param>
        /// <param name="runtimeTree">运行时归属的行为树</param>
        /// <returns></returns>
        public virtual Task CreateTask(object unit, NP_Tree runtimeTree)
        {
            return null;
        }
      
    }

    public enum NodeType
    {
        Composite,
        Decorator,
        Task,
    }

}

