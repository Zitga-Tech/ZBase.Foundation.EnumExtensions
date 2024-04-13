using Unity.Burst;
using Unity.Jobs;
using Unity.Logging;
using UnityEngine;
using ZBase.Foundation.EnumExtensions;

namespace EnumExtensionsTests
{
    public partial class TestBehaviour : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log(ComponentTypeExtensions.ToStringFast(ComponentType.SpriteRenderer));

            var job = new MyJob();
            job.Schedule();
        }

        [EnumExtensions]
        public enum ComponentType
        {
            [Display("")]
            None,

            [Display("Sprite Renderer")]
            SpriteRenderer,
        }
    }

    [BurstCompile]
    public partial struct MyJob : IJob
    {
        [BurstCompile]
        public void Execute()
        {
            var name = MyFlags.A.ToFixedDisplayStringFast();
            Log.Info(name);
            Log.Info(MyFlagsExtensions.FixedNames.B);
        }
    }
}
