using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    //private readonly EcsFilter<HealthComponent, VisualComponent> _filter = null;

    //public void Run()
    //{
    //    foreach (var i in _filter)
    //    {
    //        ref var health = ref _filter.Get1(i);
    //        ref var visual = ref _filter.Get2(i);

    //        if (health.CurrentHealth <= 0)
    //        {
    //            // Обрабатываем смерть
    //            visual.SkinnedRenderer.material = visual.DamageMaterial;
    //            // Респаун или уничтожение
    //        }
    //        else
    //        {
    //            // Отображение урона
    //            visual.SkinnedRenderer.material = visual.DamageMaterial;
    //            // Через некоторое время возвращаем стандартный материал
    //        }
    //    }
    //}
}
