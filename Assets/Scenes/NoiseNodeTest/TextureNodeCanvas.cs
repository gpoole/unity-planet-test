using System;
using NodeEditorFramework;
using NodeEditorFramework.Standard;
using UnityEditor;
using UnityEngine;

namespace Scenes.NoiseNodeTest {
    [NodeCanvasType("Texture canvas")]
    public class TextureNodeCanvas : NodeCanvas {
        public override string canvasName => "Texture";

        protected override void OnCreate() {
            Traversal = new TextureCanvasCalculator(this);
        }

        protected override void ValidateSelf() {
            if (Traversal == null)
                Traversal = new TextureCanvasCalculator(this);
        }

        public Texture GetTexture(string textureName = null) {
            var namedTextureType = typeof(NamedTextureNode);
            foreach (var node in nodes) {
                if (!namedTextureType.IsInstanceOfType(node)) {
                    continue;
                }

                var namedTextureNode = node as NamedTextureNode;
                if ((String.IsNullOrEmpty(textureName) || namedTextureNode.TextureName == textureName) &&
                    namedTextureNode.Texture != null) {
                    return namedTextureNode.Texture;
                }
            }

            return null;
        }
    }
}