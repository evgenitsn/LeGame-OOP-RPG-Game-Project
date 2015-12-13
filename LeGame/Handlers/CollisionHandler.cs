﻿namespace LeGame.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LeGame.Interfaces;
    using LeGame.Models;
    using LeGame.Models.Characters;
    using LeGame.Models.Characters.Enemies;
    using LeGame.Models.Items.Projectiles;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    internal static class CollisionHandler
    {
        public static void PlayerReaction(Character character, Keys key)
        {
            List<IGameObject> collisionItems = character.Level.Assets.Concat(character.Level.Enemies).ToList();
            var collider = Collide(character, collisionItems);

            if (!collider.Equals(-1))
            {
                 Vector2 temp = new Vector2(character.Position.X, character.Position.Y);
               
                // movement reactions
                if (collider is ICollidable && !(collider is IKillable))
                {
                    if (key == Keys.D)
                    {
                        temp.X -= character.Speed;
                        character.Position = temp;
                    }

                    if (key == Keys.W)
                    {
                        temp.Y += character.Speed;
                        character.Position = temp;
                    }

                    if (key == Keys.S)
                    {
                        temp.Y -= character.Speed;
                        character.Position = temp;
                    }

                    if (key == Keys.A)
                    {
                        temp.X += character.Speed;
                        character.Position = temp;
                    }
                }
                
                if (collider is IPickable)
                {
                    GameObject item = (GameObject)collider;
                    Console.Beep(8000, 50); 

                    // legit cool gold-pickup sound 
                    character.Level.Assets.Remove(item);
                }
            }
        }

        public static void ProjectileReaction(Projectile projectile, ILevel level)
        {
            List<IGameObject> collisionItems = level.Assets.Concat(level.Enemies).ToList();
            object collider = Collide(projectile, collisionItems);

            if (!collider.Equals(-1))
            {
                level.Projectiles.Remove(projectile);

                if (collider is Enemy)
                {
                    var enemy = (Enemy)collider;
                    enemy.CurrentHealth -= projectile.Damage;

                    if (enemy.CurrentHealth < 0 && !enemy.Type.Contains("Effect"))
                    {
                        // TODO: possition change is kinda hacky, maybe figure out a better way by fixing GfxHandler.
                        enemy.Position = new Vector2(enemy.Position.X + 16, enemy.Position.Y + 16);
                        enemy.Type = "Effects/FleshExplosionEffect";
                        enemy.CanCollide = false;
                    }
                }
            }
        }

        public static object Collide(IGameObject collider, IEnumerable<IGameObject> collisionItems)
        {
            foreach (var item in collisionItems)
            {
                Rectangle obj = GfxHandler.GetBBox(item);

                if (((item is ICollidable && ((ICollidable)item).CanCollide) || item is IPickable)
                    && GfxHandler.GetBBox(collider).Intersects(obj))
                {
                    return item;
                }
            }

            return -1;
        }

        public static void AICollide(IGameObject collider, Character character)
        {
            Rectangle colliderBBox = GfxHandler.GetBBox(collider);
            Rectangle charBBox = GfxHandler.GetBBox(character);
            if (colliderBBox.Intersects(charBBox))
            {
                if (character.CooldownTimer >= 5)
                {
                    character.TakeDamage();
                    Console.Beep(3000, 49);
                    character.CooldownTimer = 0;
                    if (character.CurrentHealth < 0)
                    {
                       // guess =)
                    }
                }
            }
        }
    }
}
