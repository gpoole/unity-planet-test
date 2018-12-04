using System;
using UnityEngine;

namespace Scenes.NoiseGeneratorTest {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class NoiseGeneratorModeAttribute : PropertyAttribute {

        public NoiseGenerator.NoiseType Type;

        public NoiseGeneratorModeAttribute(NoiseGenerator.NoiseType type) {
            Type = type;
        }
    }
}