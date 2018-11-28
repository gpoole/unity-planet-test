using UnityEngine;

namespace Scenes.BlitShaderTest {
    public class BrushControl : MonoBehaviour {

        [SerializeField]
        private Material _renderMaterial;

        [Header("Brush settings")]
        [SerializeField]
        private Color _brushColor;

        [SerializeField]
        [Range(0, 1)]
        private float _brushSize;

        [SerializeField]
        private Texture _brushTexture;

        private Texture2D _canvas;

        private RenderTexture _buffer;

        private float[] _cachedBrushPositionProperty = new float[2];

        private Vector2 _brushPosition;

        private Camera _camera;

        private void Start() {
            _canvas = new Texture2D(2048, 2048, TextureFormat.RGBA32, false);
            FillTexture(_canvas, Color.black);
            
            _buffer = new RenderTexture(2048, 2048, 0);
            _camera = Camera.main;

            GetComponentInChildren<Renderer>().material.SetTexture("_HeightMap", _canvas);
        }

        private void Update() {
            var drawing = false;
            if (Input.GetMouseButton(0)) {
                drawing = UpdateBrushPosition();
            }

            if (Input.GetKeyUp(KeyCode.Space)) {
                FillTexture(_canvas, Color.black);
            }
            
            _cachedBrushPositionProperty[0] = _brushPosition.x;
            _cachedBrushPositionProperty[1] = _brushPosition.y;
            _renderMaterial.SetFloatArray("_BrushPosition", _cachedBrushPositionProperty);
            _renderMaterial.SetInt("_BrushDrawing", drawing ? 1 : 0);
            _renderMaterial.SetFloat("_BrushSize", _brushSize);
            _renderMaterial.SetColor("_BrushColor", _brushColor);
            _renderMaterial.SetTexture("_BrushTex", _brushTexture);
            Graphics.Blit(_canvas, _buffer, _renderMaterial);
            Graphics.CopyTexture(_buffer, _canvas);
        }

        private bool UpdateBrushPosition() {
            Ray mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(mouseRay, out mouseHit) && mouseHit.transform == transform) {
                if (mouseHit.collider is MeshCollider) {
                    _brushPosition = mouseHit.textureCoord;
                    return true;
                }

                var sphereCollider = mouseHit.collider as SphereCollider;
                if (sphereCollider != null) {
                    var localHit = transform.InverseTransformPoint(mouseHit.point);
                    
                    var equator = new Vector3(localHit.x, 0, localHit.z);
                    var lat = Vector3.SignedAngle(equator, localHit, localHit.y > 0 ? Vector3.Cross(equator, localHit) : Vector3.Cross(localHit, equator));
                    var lon = Vector3.SignedAngle(Vector3.back, new Vector3(localHit.x, 0, localHit.z), Vector3.down);
                    _brushPosition.x = (lon + 180f) / 360f;
                    _brushPosition.y = (lat + 90f) / 180f;
                    return true;
                }
            }
            return false;
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