using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainGame.CollisionHandling;
using MainGame.States;
using MainGame.Sprites;
using Microsoft.Xna.Framework;
using Zelda.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace MainGame.Forces
{

    //Creates a portal that Link uses to teleport back
    public class Portal : IForces
    {
        private static Portal instance = new Portal();

        private Item item;
        private Boolean PortalOn;

        public static Portal Instance
        {
            get
            {
                return instance;
            }
        }

        private Portal()
        {
            PortalOn = true;
            item = new Item();
            item.pixelPosition = Vector2.Zero;
        }

        public void ApplyForce(IGameObject gameObject)
        {
            //Creates a portal (represented as a Map) that Link can use to teleport back
            Vector2 position = ((Link)gameObject).Position;
            if (item.pixelPosition == Vector2.Zero)
            {
                item = item.CreateItem(ItemType.Map, 1, 1);
                item.pixelPosition = position;
            } else
            {
                ((Link)gameObject).Position = item.pixelPosition;
                item.pixelPosition = Vector2.Zero;
            }
        }

        public void ToggleForce(IGameObject gameObject)
        {
            PortalOn = !PortalOn;
        }

        public void DrawPortal(SpriteBatch spriteBatch)
        {
            //Vector2.Zero is used to check if the portal needs to be drawn
            if (item.pixelPosition != Vector2.Zero)
            {
                item.itemSprite.Draw(spriteBatch, item.pixelPosition);
            }
        }
    }
}