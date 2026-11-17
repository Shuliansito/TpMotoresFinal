using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    [SerializeField] private float _speedBonus = 1f;
    [SerializeField] private float _jumpBonus = 1.5f;
    [SerializeField] private float _regenBonus = 2f;
    [SerializeField] private float _energyBonus = 2f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponentInParent<PlayerMovement>();
        JumpLogic playerJump = other.GetComponentInParent<JumpLogic>();

        if (player == null) { return; }
        string artifactName = gameObject.name;

        if (artifactName.StartsWith("Velocidad"))
        {
            ApplyPower(player);
        }
        if (artifactName.StartsWith("Salto"))
        {
            JumpBoost(playerJump);
        }
        else if (artifactName.StartsWith("Energia"))
        {
            player.ReduceStaminaDrain(_energyBonus);
        }
        Destroy(gameObject);
    }

    private void ApplyPower(PlayerMovement player)
    {
        player.AddSpeed(_speedBonus);
    }
    private void JumpBoost(JumpLogic player)
    {
        player.AddJump(_jumpBonus);
    }
}