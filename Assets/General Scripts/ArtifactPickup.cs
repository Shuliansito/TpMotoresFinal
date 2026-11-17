using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    public const int Total = 3;
    public static int Collected;

    [SerializeField] private float _speedBonus = 1f;
    [SerializeField] private float _jumpBonus = 0.5f;
    [SerializeField] private float _lungsBonus = 0.2f;

    private bool _picked;

    private void OnTriggerEnter(Collider other)
    {
        if (_picked) { return; }

        PlayerMovement player = other.GetComponentInParent<PlayerMovement>();
        JumpLogic _player = other.GetComponentInParent<JumpLogic>();

        if (player == null) { return; }

        _picked = true;
        ApplyPower(player,_player);
        Collected++;
        Destroy(gameObject);
    }

    private void ApplyPower(PlayerMovement player, JumpLogic jump)
    {
        string artifactName = gameObject.name;

        if (artifactName.StartsWith("Velocidad"))
        {
            player.AddSpeed(_speedBonus);
        }
        else if (artifactName.StartsWith("Salto"))
        {
            jump.AddJump(_jumpBonus);
        }
        else if (artifactName.StartsWith("Pulmones"))
        {
            player.ReduceStaminaDrain(_lungsBonus);
        }
    }
}