using Game.Enums;
using UnityEngine;

namespace Game
{
    public class Product : MonoBehaviour
    {
        [SerializeField] private ProductType _type;
        [SerializeField] private float _upOffset;

        public ProductType Type => _type;
        public float UpOffset => _upOffset;
    }
}
