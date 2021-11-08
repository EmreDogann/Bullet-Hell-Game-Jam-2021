using UnityEngine;
using UnityEngine.UI;

public class CameraRT : MonoBehaviour {
    
    public RawImage rawImage;

    private Camera _camera;
    private Vector2 _resolution;

    private void Awake() {
        _camera = gameObject.GetComponent<Camera>();
        if (transform.parent.CompareTag("MainCamera")) {
            _camera.orthographicSize = transform.parent.GetComponent<Camera>().orthographicSize;
        }

        _resolution = new Vector2(Screen.width, Screen.height);
        _camera.targetTexture = new RenderTexture((int) _resolution.x, (int) _resolution.y, 24);
        rawImage.texture = _camera.targetTexture;
    }

    private void Update() {

        if (_resolution.x != Screen.width || _resolution.y != Screen.height) {
            _resolution.x = Screen.width;
            _resolution.y = Screen.height;

            _camera.targetTexture.Release();
            _camera.targetTexture = new RenderTexture((int) _resolution.x, (int) _resolution.y, 24);
            rawImage.texture = _camera.targetTexture;
        }
    }
}