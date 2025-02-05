using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// For all link states
public interface ILinkState
{
    void Enter();
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    void Exit();
}