using UnityEngine;

public interface IDamageable
{
    void Damage(int pValue, GameObject pSender, Vector3 pPoint, Vector3 pDirection, Color pColor);
    void Damage(int pValue, GameObject pSender, Vector3 pPoint, Vector3 pDirection);
    // void SetStatus(StatusEffects.status pStatus, float pSeconds);
    GameObject GetGameObject();
    IDamageable GetParentDamageable();
}