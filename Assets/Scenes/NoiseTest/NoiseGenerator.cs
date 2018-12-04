using System;
using LibNoise;
using LibNoise.Generator;
using UnityEngine;

namespace Scenes.NoiseGeneratorTest {
    public class NoiseGenerator : NoiseNode {
        [Flags]
        public enum NoiseType {
            Billow                = 1 << 0,
            Checker               = 1 << 1,
            Const                 = 1 << 2,
            Cylinders             = 1 << 3,
            Perlin                = 1 << 4,
            RidgedMultifractal    = 1 << 5,
            Spheres               = 1 << 6,
            Voronoi               = 1 << 7
        }

        [SerializeField]
        private NoiseType _type;
        
        [SerializeField]
        [NoiseGeneratorMode(NoiseType.Billow | NoiseType.Cylinders | NoiseType.Perlin | NoiseType.RidgedMultifractal | NoiseType.Spheres | NoiseType.Voronoi)]
        private double _frequency;

        [SerializeField]
        [NoiseGeneratorMode(NoiseType.Voronoi)]
        private double _dispacement;
        
        [SerializeField]
        [NoiseGeneratorMode(NoiseType.Billow | NoiseType.Perlin | NoiseType.RidgedMultifractal)]
        private double _lacunarity;

        [SerializeField]
        [NoiseGeneratorMode(NoiseType.Billow | NoiseType.Perlin)]
        private double _persistence;

        [SerializeField]
        [NoiseGeneratorMode(NoiseType.Billow | NoiseType.Perlin | NoiseType.RidgedMultifractal)]
        private int _octaves;

        [SerializeField]
        [NoiseGeneratorMode(NoiseType.Billow | NoiseType.Perlin | NoiseType.RidgedMultifractal | NoiseType.Voronoi)]
        private int _seed;

        [SerializeField]
        [NoiseGeneratorMode(NoiseType.Voronoi)]
        private bool _distance;

        [SerializeField]
        [NoiseGeneratorMode(NoiseType.Billow | NoiseType.Perlin | NoiseType.RidgedMultifractal)]
        private QualityMode _quality;

        private ModuleBase _generator;

        public override ModuleBase GetResult() {
            return _generator;
        }

        private void Start() {
            _generator = ConstructGenerator();
        }
        
        private void OnValidate() {
            _generator = ConstructGenerator();
        }

        private ModuleBase ConstructGenerator() {
            switch (_type) {
                case NoiseType.Billow:
                    return new Billow(_frequency, _lacunarity, _persistence, _octaves, _seed, _quality);
                case NoiseType.Checker:
                    return new Checker();
                case NoiseType.Const:
                    return new Const();
                case NoiseType.Cylinders:
                    return new Cylinders(_frequency);
                case NoiseType.Perlin:
                    return new Perlin(_frequency, _lacunarity, _persistence, _octaves, _seed, _quality);
                case NoiseType.RidgedMultifractal:
                    return new RidgedMultifractal(_frequency, _lacunarity, _octaves, _seed, _quality);
                case NoiseType.Spheres:
                    return new Spheres(_frequency);
                case NoiseType.Voronoi:
                    return new Voronoi(_frequency, _dispacement, _seed, _distance);
                default:
                    throw new Exception($"Unknown noise type: {_type}");
            }
        }
    }
}