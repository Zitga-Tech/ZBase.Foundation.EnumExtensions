using UnityEngine;
using ZBase.Foundation.EnumExtensions;

namespace EnumExtensionsTests
{
    public partial class TestBehaviour : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log(ComponentTypeExtensions.ToStringFast(ComponentType.SpriteRenderer));
        }

        [EnumExtensions]
        public enum ComponentType
        {
            None,
            SpriteRenderer,
        }
    }
}
