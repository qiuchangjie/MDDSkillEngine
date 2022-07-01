using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class SpawnHeroPoint : MonoBehaviour
    {
        [SerializeField]
        private string HeroName;

        private float Seconds = 0.5f;
        private float curSceonds = 0f;

        private void Start()
        {
            SpawnHero();
        }

        private void Update()
        {
            curSceonds += Time.deltaTime;
            if (curSceonds >= Seconds)
            {
                SpawnHero();
                curSceonds = 0f;
            }
        }

        private void SpawnHero()
        {
            Vector2 pos = Random.insideUnitCircle;
            int num = Random.Range(1,3);
            Game.Entity.ShowEntity(typeof(Hero103_TimeDemo), "Hero_103", new HeroData(Game.Entity.GenerateSerialId(), 0, null)
            {
                LocalScale = new Vector3(0.3f, 0.3f, 0.3f),
                Position =transform.position + new Vector3(0f, pos.x * num, pos.y * num),
            });
        }
    }
}

