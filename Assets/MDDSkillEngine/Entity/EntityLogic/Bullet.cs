using UnityEngine;
using MDDGameFramework.Runtime;
using MDDGameFramework;

namespace MDDSkillEngine
{
    /// <summary>
    /// 子弹类。
    /// </summary>
    public class Bullet : Entity
    {
        Blackboard blackboard;

        Blackboard shared_Blackboard;

        Root behaveTree;

        [SerializeField]
        private BulletData m_BulletData = null;
      
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            shared_Blackboard = UnityContext.GetSharedBlackboard("Bullet");

            blackboard = new Blackboard(shared_Blackboard, UnityContext.GetClock());

            blackboard["isBoom"] = false;

            behaveTree = CreateBehaviourTree();

            
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_BulletData = userData as BulletData;

           
            if (m_BulletData == null)
            {
                Log.Error("Bullet data is invalid.");
                return;
            }

            GameEnter.Entity.ShowEffect(new EffectData(GameEnter.Entity.GenerateSerialId(), 50000) { name = "Flash", Position = this.transform.position });

            behaveTree.Start();
        }

        private Root CreateBehaviourTree()
        {
            // Tell the behaviour tree to use the provided blackboard instead of creating a new one
            return new Root(blackboard,
                new Selector(
                    new BlackboardCondition("isBoom",Operator.IS_EQUAL,false,Stops.IMMEDIATE_RESTART,
                        new Action(()=> { MoveTowards(SelectEntity.selectEntity); })),

                    new Action(()=> 
                    {
                       
                    })
                    )

            
                );
        }

        private void UpdateBlackboards()
        {
            blackboard["GameObject"] = SelectEntity.selectEntity;
        }

        private void MoveTowards(Entity localPosition)
        {
            Log.Error("yidong  =-=-=-=-");

            this.transform.position = Vector3.MoveTowards(this.transform.position, localPosition.gameObject.transform.position, Time.deltaTime*5);

            //transform.position += localPosition.transform.position * 0.5f * Time.deltaTime;
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            //CachedTransform.Translate(Vector3.forward * m_BulletData.Speed * elapseSeconds, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            blackboard["isBoom"] = true;

            Log.Error("出发碰撞  炸了");

            Entity e = other.gameObject.GetComponent<Entity>();

            if (e is Enemy)
            {
                ((Enemy)e).findingTest.died.Invoke();

                GameEnter.Entity.ShowEffect(new EffectData(GameEnter.Entity.GenerateSerialId(), 50000) { name = "Hit", Position = this.transform.position });

                behaveTree.Stop();
            }

            GameEnter.Entity.HideEntity(this);
        }
    }
}
