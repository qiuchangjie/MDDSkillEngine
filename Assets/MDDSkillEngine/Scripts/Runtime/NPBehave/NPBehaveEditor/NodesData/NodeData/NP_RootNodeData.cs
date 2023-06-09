﻿using MDDGameFramework;

namespace MDDSkillEngine
{
    public class NP_RootNodeData : NP_NodeDataBase
    {
        private Root m_Root;

        public override Decorator CreateDecoratorNode(object m_object,NP_Tree runtimeTree, Node node)
        {
            this.m_Root =Root.Create(node, m_object as IEntity,Game.NPBehave.GetClock());

            return this.m_Root;
        }

        public override Node NP_GetNode()
        {
            return this.m_Root;
        }
    }

}

