using UnityEngine;

namespace Scenes.BlitShaderTest {
    public class BrushControl : MonoBehaviour {
        private enum DrawMode {
            None,
            Painting,
            Erasing,
        }

        [SerializeField]
        private Material _renderMaterial;

        [Header("Brush settings")]
        [SerializeField]
        private float _brushAlpha;

        [SerializeField]
        [Range(0, 5)]
        private float _brushSize;

        [SerializeField]
        private Texture _brushTexture;

        [SerializeField]
        private string _textureProperty;

        [SerializeField]
        private bool _allowDragging;

        private Texture2D _canvas;

        private bool _dragging;

        private RenderTexture _buffer;

        private Vector2 _brushPosition;

        private Camera _camera;

        private DrawMode _drawMode;

        private void Start() {
            _canvas = new Texture2D(2048, 2048, TextureFormat.RGBA32, false);
            FillTexture(_canvas, new Color(0.5f, 0, 0, 1));
            
            _buffer = new RenderTexture(2048, 2048, 0);
            _camera = Camera.main;
            _brushPosition = Vector2.zero;

            GetComponentInChildren<Renderer>().material.SetTexture(string.IsNullOrEmpty(_textureProperty) ? "_MainTex" : _textureProperty, _canvas);
        }

        private void Update() {
            _brushSize = Mathf.Clamp(_brushSize + Input.mouseScrollDelta.y * 0.1f, 0, 5);

            if (_drawMode != DrawMode.None && _allowDragging) {
                _brushPosition = GetBrushPosition();
            }

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
                _drawMode = Input.GetMouseButtonDown(0) ? DrawMode.Painting : DrawMode.Erasing;
                _brushPosition = GetBrushPosition();
            }

            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || _brushPosition == Vector2.zero) {
                _drawMode = DrawMode.None;
                _brushPosition = Vector2.zero;
            }

            if (Input.GetKeyUp(KeyCode.Space)) {
                FillTexture(_canvas, Color.black);
            }

            switch (_drawMode) {
                case DrawMode.Painting:
                    _renderMaterial.SetInt("_BrushDirection", 1);
                    break;
                case DrawMode.Erasing:
                    _renderMaterial.SetInt("_BrushDirection", -1);
                    break;
                case DrawMode.None:
                    _renderMaterial.SetInt("_BrushDirection", 0);
                    break;
            }
            
            _renderMaterial.SetFloat("_BrushAlpha", _brushAlpha);
            _renderMaterial.SetTexture("_BrushTex", _brushTexture);
            _renderMaterial.SetTextureOffset("_BrushTex", _brushPosition);
            _renderMaterial.SetTextureScale("_BrushTex", new Vector2(5 - _brushSize, 5 - _brushSize));
            Graphics.Blit(_canvas, _buffer, _renderMaterial);
            Graphics.CopyTexture(_buffer, _canvas);
        }

        private Vector2 GetBrushPosition() {
            Ray mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(mouseRay, out mouseHit) && mouseHit.transform == transform) {
                if (mouseHit.collider is MeshCollider) {
                    return mouseHit.textureCoord;
                }

                var sphereCollider = mouseHit.collider as SphereCollider;
                if (sphereCollider != null) {
                    var localHit = transform.InverseTransformPoint(mouseHit.point);
                    
                    var equator = new Vector3(localHit.x, 0, localHit.z);
                    var lat = Vector3.SignedAngle(equator, localHit, localHit.y > 0 ? Vector3.Cross(equator, localHit) : Vector3.Cross(localHit, equator));
                    var lon = Vector3.SignedAngle(Vector3.back, new Vector3(localHit.x, 0, localHit.z), Vector3.down);
                    return new Vector2((lon + 180f) / 360f, (lat + 90f) / 180f);
                }
            }
            return Vector2.zero;
        }

        private void FillTexture(Texture2D texture, Color color) {
            Color[] fill = new Color[_canvas.width * _canvas.height];
            for (var i = 0; i < fill.Length; i++) {
                fill[i] = color;
            }
            texture.SetPixels(fill);
            texture.Apply();
        }
    }
}