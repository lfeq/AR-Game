using UnityEngine;

public class TrafficLightManager : MonoBehaviour {
    [SerializeField] private LaneManager laneManager;
    [SerializeField] private Color redLightColor = Color.red;
    [SerializeField] private Color yellowLightColor = Color.yellow;
    [SerializeField] private Color greenLightColor = Color.green;

    private Renderer m_renderer;
    private Material m_material;
    private bool m_isRedLight;

    private void Start() {
        m_renderer = GetComponent<Renderer>();
        m_material = m_renderer.material;
        m_material.color = redLightColor;
        m_isRedLight = true;
    }

    private void OnMouseDown() {
        ChangeLight();
    }

    private void ChangeLight() {
        if (m_isRedLight) {
            m_material.color = greenLightColor;
            m_isRedLight = false;
        } else {
            m_material.color = redLightColor;
            m_isRedLight = true;
        }
        laneManager.ChangeTrafficLight(m_isRedLight);
    }
}