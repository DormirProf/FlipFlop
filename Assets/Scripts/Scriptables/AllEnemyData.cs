using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Scriptables
{
    [CreateAssetMenu(menuName = "Data/AllEnemyData")]
    public class AllEnemyData : ScriptableObject
    {
        [Title("All enemy")]
        [Searchable] public List<Birds> Bird = new List<Birds>();
        public int BirdsLastSpawn = 0;

        [Serializable]
        public class Birds
        {
            public string Name;
            public float Speed;
            public float Scale;
            public ChoiceOfTheCollier ChoiceOfTheCollier;

            [AssetsOnly]
            public Sprite Sprite;
            public Sprite[] AllSprites;
        }
    
        public enum ChoiceOfTheCollier
        {
            Round = 0,
            square = 1
        }
    }
}

