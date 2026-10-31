using UnityEngine;

/// <summary>
/// Controls when the MiniMapCamera actually renders.
///
/// The GameObject stays active in the Editor. Only its Camera component is kept
/// disabled in the scene file, so the Editor does not render the minimap
/// RenderTexture while you edit the scene.
///
/// This script has no [ExecuteAlways] or [ExecuteInEditMode] attribute, so Awake()
/// runs only in Play Mode. When Play Mode starts, Awake() enables the camera so the
/// minimap works during gameplay. When Play Mode ends, Unity restores the serialized
/// (disabled) state, so the camera returns to its Edit Mode state automatically.
/// </summary>
[RequireComponent(typeof(Camera))]
public class MiniMapCameraRuntimeController : MonoBehaviour
{
    private Camera minimapCamera;

    private void Awake()
    {
        minimapCamera = GetComponent<Camera>();

        if (minimapCamera == null)
        {
            Debug.LogWarning("MiniMapCameraRuntimeController: no Camera found on " + name + ". Minimap will not render.");
            return;
        }

        // Runtime only: enable the minimap camera now that Play Mode has started.
        minimapCamera.enabled = true;
    }
}
