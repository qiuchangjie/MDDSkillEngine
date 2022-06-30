using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class SpawnHeroPoint : MonoBehaviour
    {
        [SerializeField]
        private string HeroName;

        private void Start()
        {
            SpawnHero();
        }

        private void SpawnHero()
        {
            Game.Entity.ShowEntity(typeof(Hero103), "Hero_103", new HeroData(Game.Entity.GenerateSerialId(), 0, null)
            {
                Position = transform.position,
            });
        }
    }
}

