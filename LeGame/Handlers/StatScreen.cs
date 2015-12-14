﻿namespace LeGame.Handlers
{
    using System;

    using LeGame.Engine;
    using LeGame.Models.Characters;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class StatScreen
    {
        private SpriteFont font;

        public void DrawHealth(Character character, ContentManager content, SpriteBatch spriteBatch)
        {
            int hp = character.CurrentHealth;
            this.font = content.Load<SpriteFont>(@"Fonts/SpriteFont");
            string healthBar = String.Format("Health points: {0}", hp);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, healthBar, new Vector2(650, 10), Color.Red);
            spriteBatch.End();
        }

        public void EndScreen(ContentManager content, SpriteBatch spriteBatch)
        {
            this.font = content.Load<SpriteFont>(@"Fonts/DeathFont");
            Vector2 middle = new Vector2(GlobalVariables.WindowWidth / 2, GlobalVariables.WindowHeight / 2);
            spriteBatch.Begin();
            spriteBatch.DrawString(this.font, "GG NOOB", middle, Color.Red);
            spriteBatch.End();
        }

    }
}

