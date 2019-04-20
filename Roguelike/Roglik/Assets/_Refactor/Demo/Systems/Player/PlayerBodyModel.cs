using System.Collections.Generic;
using LDF.Structures;
using LDF.Systems;
using UnityEngine;

namespace LDF.Roglik
{
    public class PlayerBodyModel : MonoBehaviour, ILivingBodySystemInput
    {
        [field: SerializeField]
        public Progress Health { get; private set; } = new Progress(30,30);

        public bool IsAlive => Health.Current > 0;

        public float MaxHealth => Health.max;

        public float CurrentHealth
        {
            get => Health.Current;
            set => Health.Current = value;
        }

        public int SuperArmor { get; }

        public bool IsInvulnerable { get; }
        public IDictionary<string, IStatus> Statuses { get; set; } = new Dictionary<string, IStatus>();
    }
}