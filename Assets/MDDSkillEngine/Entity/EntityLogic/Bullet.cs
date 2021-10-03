using UnityEngine;
using MDDGameFramework.Runtime;
using MDDGameFramework;
using Animancer;

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

            shared_Blackboard["isTrigger"] = null;

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

        private int i;

        private Root CreateBehaviourTree()
        {
            // Tell the behaviour tree to use the provided blackboard instead of creating a new one
            return new Root(blackboard,
                new Parallel(Parallel.Policy.ONE, Parallel.Policy.ONE,
                    new Parallel(Parallel.Policy.ONE, Parallel.Policy.ONE,
                        new BlackboardCondition("isTrigger", Operator.IS_NOT_EQUAL, null, Stops.IMMEDIATE_RESTART,
                             new Action(() =>
                             {
                                 Log.Error("isTrigger");
                                 shared_Blackboard.Get<Entity>("isTrigger").gameObject.GetComponent<PathFindingTest>().attackAction?.Invoke();
                                 shared_Blackboard["isTrigger"] = null;
                                 i++;
                                 if (i==5)
                                 GameEnter.Entity.HideEntity(this);
                             }))),
                    new BlackboardCondition("isTrigger", Operator.IS_EQUAL, null, Stops.IMMEDIATE_RESTART,
                        new Action(()=> 
                        {
                            Log.Error("move");
                            MoveTowards(SelectEntity.entities[i]);                            
                        })

                                           )

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

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);

            blackboard["isBoom"] = false;

            behaveTree.Stop();
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

            GameEnter.TextBar.ShowTextBar(e,999);

            if (e is Enemy)
            {
                shared_Blackboard["isTrigger"] = e;

               //((Enemy)e).findingTest.died.Invoke();

                GameEnter.Entity.ShowEffect(new EffectData(GameEnter.Entity.GenerateSerialId(), 50000) { name = "Hit", Position = this.transform.position });

                //behaveTree.Stop();
            }

            
        }
    }
}
