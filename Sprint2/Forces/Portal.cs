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
        private Item item2;
        private Boolean PortalOn;
        private int ADJUST_FOR_LINK = 8; //Adjusts the portal to be spawned at the center of Link
        private int LEEWAY_USE_PORTAL = 10; //Leeway for Link to use the portal

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
            item2 = new Item();
            item.pixelPosition = Vector2.Zero;
        }

        public void ApplyForce(IGameObject gameObject)
        {
            //Creates a portal (represented as a Map) that Link can use to teleport back
            Vector2 position = ((Link)gameObject).Position;
            if (item.pixelPosition == Vector2.Zero)
            {
                item = item.CreateItem(ItemType.Map, 1, 1);
                item.pixelPosition.X = (position.X - ADJUST_FOR_LINK);
                item.pixelPosition.Y = (position.Y - ADJUST_FOR_LINK);
            } else
            {
                item2 = item.CreateItem(ItemType.Map, 1, 1);
                item2.pixelPosition.X = (position.X - ADJUST_FOR_LINK);
                item2.pixelPosition.Y = (position.Y - ADJUST_FOR_LINK);
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
            if (item2.pixelPosition != Vector2.Zero)
            {
                item2.itemSprite.Draw(spriteBatch, item2.pixelPosition);
            }
        }

        public void UsePortal(IGameObject gameObject)
        {
            Vector2 position = ((Link)gameObject).Position;
            if (Math.Abs(position.X - item.pixelPosition.X - ADJUST_FOR_LINK) < LEEWAY_USE_PORTAL &&
                Math.Abs(position.Y - item.pixelPosition.Y - ADJUST_FOR_LINK) < LEEWAY_USE_PORTAL)
            {
                ((Link)gameObject).Position = item2.pixelPosition;
                DestroyPortals();
            }
            if (Math.Abs(position.X - item2.pixelPosition.X - ADJUST_FOR_LINK) < LEEWAY_USE_PORTAL &&
                Math.Abs(position.Y - item2.pixelPosition.Y - ADJUST_FOR_LINK) < LEEWAY_USE_PORTAL)
            {
                ((Link)gameObject).Position = item.pixelPosition;
                DestroyPortals();
            }
        }

        public void DestroyPortals()
        {            
            //Destroys the portals
            item.pixelPosition = Vector2.Zero;
            item2.pixelPosition = Vector2.Zero;
        }

        public Boolean ReadyToUse()
        {
            if (item2.pixelPosition != Vector2.Zero)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}