using System.Collections.Generic;
using InventorySystem.Views;
using UnityEngine;

namespace InventorySystem.Managers
{
    public class SpriteManager : MonoBehaviour
    {
        public static SpriteManager Instance;
        private Dictionary<string, Sprite> _spriteCache;
        private void Awake()
        {
            if(Instance != null) Destroy(Instance);
            Instance = this;
        }
        public void LoadSpriteSheet()
        {
            if (_spriteCache == null)
                _spriteCache = new Dictionary<string, Sprite>();

            Sprite[] sprites = Resources.LoadAll<Sprite>("");
            foreach (var sprite in sprites)
            {
                if (!_spriteCache.ContainsKey(sprite.name))
                {
                    _spriteCache[sprite.name] = sprite;
                }
            }
        }
        public Sprite GetSprite(string spriteName)
        {
            if (_spriteCache != null && _spriteCache.TryGetValue(spriteName, out var sprite))
            {
                return sprite;
            }

            Debug.LogWarning($"Sprite not found in cache: {spriteName}");
            return null;
        }
    }
}