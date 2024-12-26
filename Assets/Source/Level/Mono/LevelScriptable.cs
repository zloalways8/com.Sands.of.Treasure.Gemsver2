using UnityEngine;

namespace Source.Level.Mono
{
    [CreateAssetMenu(fileName = "Level", menuName = "Create Level", order = 0)]
    public class LevelScriptable : ScriptableObject
    {
        public float Duration;
        public int MaxScore;
        public int MinScore;
        public string Name;
    }
}