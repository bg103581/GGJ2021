using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMaterialSetter : MonoBehaviour
{
    [SerializeField] private List<Material> m_Fur = new List<Material>();
    [SerializeField] private List<Material> m_Pattern = new List<Material>();
    [SerializeField] private List<Material> m_Collar = new List<Material>();
    [SerializeField] private SkinnedMeshRenderer m_meshRenderer;

    private List<Material> newMaterials = new List<Material>();
    public void SetMaterials(Fur fur, Pattern pattern, Collar collar) {
        newMaterials.Add(m_Fur[(int)fur]);
        newMaterials.Add(m_Pattern[(int)pattern]);
        newMaterials.Add(m_Collar[(int)collar]);

        m_meshRenderer.materials = newMaterials.ToArray();
    }
}
