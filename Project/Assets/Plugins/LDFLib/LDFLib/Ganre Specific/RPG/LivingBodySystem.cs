using System;
using System.Collections.Generic;
using LDF.Structures;
using UnityEngine;

namespace LDF.Systems
{
    public interface ILivingBodySystemInput
    {
        bool IsAlive { get; }
        bool IsInvulnerable { get; }
        int SuperArmor { get; }
        float MaxHealth { get; }
        
        float CurrentHealth { get; set; }
        IDictionary<string, IStatus> Statuses { get; }
    }

    public class LivingBodySystem : SystemBehaviour<ILivingBodySystemInput>
    {
        public event Action OnDeath;
        public event Action OnBlockAttack;
        public event Action<float> OnDamageTaken;
        public event Action<float> OnHeal;

        private void Update()
        {
            UpdateStatusEffects();
        }

        public void TakeAttack(float damage, int superArmorPenetration = 0)
        {
            if (superArmorPenetration >= input.SuperArmor)
                TakeDamage(damage);
            else
                OnBlockAttack?.Invoke();
        }

        public void ApplyStatus(string statusId, IStatus status)
        {
            if (input.Statuses.ContainsKey(statusId))
            {
                input.Statuses[statusId].AddDuration(status.StatusDuration);
            }

            input.Statuses.Add(statusId, status);
        }
        
        public void Heal(int amount)
        {
            var clampedAmount = Mathf.Clamp(amount, 0, input.MaxHealth - input.CurrentHealth);
            input.CurrentHealth += clampedAmount;
            OnHeal?.Invoke(clampedAmount);
        }
        
        private void UpdateStatusEffects()
        {
            foreach (var status in input.Statuses.Values)
            {
                status.UpdateStatus(1);
            }
        }

        private void TakeDamage(float value)
        {
            if (input.IsInvulnerable || !input.IsAlive)
                return;

            input.CurrentHealth -= value;

            OnDamageTaken?.Invoke(value);

            if (!input.IsAlive)
                OnDeath?.Invoke();
        }
        
#if UNITY_EDITOR
        [ContextMenu("Take 10 damage")]
        public void TakeAttack_EditorOnly() => TakeAttack(10);
#endif
    }
    
}