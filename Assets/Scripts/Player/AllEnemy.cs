using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    class AllEnemy : MonoBehaviour
    {
        [Title("All enemy")]
        public int BirdsLastSpawn = 0;
        [Searchable] public List<Birds> Bird = new List<Birds>();

        [Serializable]
        public class Birds
        {
            public string Name;
            public float Speed;
            public float Scale;
            public ChoiceOfTheCollier ChoiceOfTheCollier;

            [AssetsOnly]
            public Sprite Sprite;
            public Sprite[] Frames;
        }
    
        public enum ChoiceOfTheCollier
        {
            Round = 0,
            square = 1
        }
    } 
}


