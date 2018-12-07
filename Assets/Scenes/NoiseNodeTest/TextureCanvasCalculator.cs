using System.Collections.Generic;
using System.Collections;
using NodeEditorFramework;
using NodeEditorFramework.Standard;
using UnityEditor;
using UnityEngine;

namespace Scenes.NoiseNodeTest {
    public class TextureCanvasCalculator : CanvasCalculator {
        private const float UpdateDelay = 0.5f;

        private Stack<Node> _nodesToUpdate = new Stack<Node>();

        private float _timer;

        private float _lastTick;

        public TextureCanvasCalculator(NodeCanvas canvas) : base(canvas) {
            _lastTick = Time.realtimeSinceStartup;
            EditorApplication.update += OnUpdate;
        }

        ~TextureCanvasCalculator() {
            EditorApplication.update -= OnUpdate;
        }

        private void OnUpdate() {
            var deltaTime = Time.realtimeSinceStartup - _lastTick;
            Tick(deltaTime);
            _lastTick = Time.realtimeSinceStartup;
        }

        public override void OnChange(Node node) {
            if (!_nodesToUpdate.Contains(node)) {
                _nodesToUpdate.Push(node);
            }

            _timer = UpdateDelay;
        }

        private void Tick(float deltaTime) {
            if (_timer > 0) {
                _timer -= deltaTime;
            }

            if (_nodesToUpdate.Count > 0 && _timer <= 0) {
                while (_nodesToUpdate.Count > 0) {
                    base.OnChange(_nodesToUpdate.Pop());
                }
                _timer = 0;
            }
        }
    }
}