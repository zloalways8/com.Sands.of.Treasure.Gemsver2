using Source.Level.Mono;
using UnityEngine;

namespace Source.Game
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GameUnility", order = 0)]
    public class GameSettingsScriptable : ScriptableObject
    {
        public Vector2Int LevelSize;
        public Vector2 CellPadding;
        public Vector2 LevelPosition;
        public int BorderCut;
        public bool ByItemScore;
        public bool TimerEnable;
        public Sprite CellSprite;
        public Vector2 CellSize => CellSprite.bounds.size;
        public Sprite[] EggSprites;
        public LevelScriptable[] LevelScriptables;


    }
}