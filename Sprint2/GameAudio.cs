using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;


namespace Sprint0
{
    public class GameAudio
    {
        private Song bgm;
        private SoundEffect[] soundEffects;

        private static GameAudio instance = new GameAudio();

        public static GameAudio Instance
        {
            get
            {
                return instance;
            }
        }

        private GameAudio()
        {

        }


        public void LoadAllAudio(ContentManager content)
        {
            bgm = content.Load<Song>(@"Sound Effects\Underworld BGM");
            soundEffects = new SoundEffect[]
            {
                content.Load<SoundEffect>(@"Sound Effects\Pick Up Item"),
                content.Load<SoundEffect>(@"Sound Effects\Enemy Hurt"),
                content.Load<SoundEffect>(@"Sound Effects\Link Hurt"),
                content.Load<SoundEffect>(@"Sound Effects\Low Health"),
                content.Load<SoundEffect>(@"Sound Effects\Sword Swing"),
                content.Load<SoundEffect>(@"Sound Effects\Enemy Death"),
                content.Load<SoundEffect>(@"Sound Effects\Pick Up Better Item"),
                content.Load<SoundEffect>(@"Sound Effects\Item Spawning"),
                content.Load<SoundEffect>(@"Sound Effects\Collect Rupee")
            };
        }

        public void PlayBGM()
        {
            MediaPlayer.Play(bgm);
        }

        public void PickUpItem()
        {
            soundEffects[0].Play();
        }

        public void EnemyHit()
        {
            soundEffects[1].Play();
        }

        public void LinkHit()
        {
            soundEffects[2].Play();
        }

        public void LowHealth()
        {
            soundEffects[3].Play();
        }

        public void SwordSwing()
        {
            soundEffects[4].Play();
        }

        public void EnemyDead()
        {
            soundEffects[5].Play();
        }

        public void PickUpBetterItem()
        {
            soundEffects[6].Play();
        }

        public void ItemSpawn()
        {
            soundEffects[7].Play();
        }

        public void CollectRupee()
        {
            soundEffects[8].Play();
        }


    }
}
