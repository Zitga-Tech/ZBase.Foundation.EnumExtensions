// Source:
// https://github.com/andrewlock/NetEscapades.EnumGenerators

using System;

namespace ZBase.Foundation.EnumExtensions
{
    public readonly struct EnumValueOption : IEquatable<EnumValueOption>
    {
        /// <summary>
        /// Custom name set by the <c>[Display(Name)]</c> attribute.
        /// </summary>
        public string DisplayName { get; }

        public bool IsDisplayNameTheFirstPresence { get; }

        public EnumValueOption(string displayName, bool isDisplayNameTheFirstPresence)
        {
            DisplayName = displayName;
            IsDisplayNameTheFirstPresence = isDisplayNameTheFirstPresence;
        }

        public bool Equals(EnumValueOption other)
            => DisplayName == other.DisplayName
            && IsDisplayNameTheFirstPresence == other.IsDisplayNameTheFirstPresence;
    }
}
