using System;
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
            Debug.Log($"ToStringFast: {ComponentTypeExtensions.ToStringFast(ComponentType.SpriteRenderer)}");

            const string ASSERT = "Assert";

            if (ASSERT.AsSpan().TryParse(out LogType value, true, false))
            {
                Debug.Log($"TryParse: {value}");
            }

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
