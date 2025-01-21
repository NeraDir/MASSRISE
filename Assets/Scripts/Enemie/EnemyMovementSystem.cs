using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementSystem
{
    //private readonly EcsFilter<EnemyComponent, MovementComponent, PositionComponent> _filter = null;

    //public void Run()
    //{
    //    foreach (var i in _filter)
    //    {
    //        ref var movement = ref _filter.Get2(i);
    //        ref var position = ref _filter.Get3(i);

    //        var direction = movement.Target.position - position.Position;
    //        direction.y = 0; // Игнорируем высоту

    //        position.Position += direction.normalized * movement.Speed * Time.deltaTime;
    //    }
    //}
}
