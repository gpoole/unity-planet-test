using System;
using NodeEditorFramework;
using NodeEditorFramework.Standard;
using UnityEngine;

namespace Scenes.NoiseNodeTest {
    [NodeCanvasType("Texture canvas")]
    public class TextureNodeCanvas : CalculationCanvasType {
        public override string canvasName => "Texture";

        public Texture GetTexture(string textureName = null) {
            var namedTextureType = typeof(NamedTextureNode);
            foreach (var node in nodes) {
                if (!namedTextureType.IsInstanceOfType(node)) {
                    continue;
                }

                var namedTextureNode = node as NamedTextureNode;
                if ((String.IsNullOrEmpty(textureName) || namedTextureNode.TextureName == textureName) && namedTextureNode.Texture != null) {
                    return namedTextureNode.Texture;
                }
            }

            return null;
        }
    }
}