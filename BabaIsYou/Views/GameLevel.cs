using BabaIsYou.Entities;
using BabaIsYou.Systems;
using BabaIsYou.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabaIsYou.Views
{
    public class GameLevel : GameView
    {
        private List<Entity> m_removeThese = new List<Entity>();
        private List<Entity> m_addThese = new List<Entity>();

        private Systems.Renderer m_sysRenderer;
        private Systems.Input m_sysKeyboardInput;
        private Systems.Movement m_sysMovement;
        private Systems.Rule m_sysRule;
        private Systems.AnimatedSprite m_sysAnimatedSprite;
        private Systems.Level m_sysLevel;
        public GameLevel(Game1 game, String levelName): base(game)
        {
            this.Init(levelName);
        }
        public GameLevel(Game1 game, String levelName, int width, int height) : base(game, width, height)
        {
            this.Init(levelName);
        }
        public GameLevel(Game1 game, String levelName, int width, int height, int posX, int posY) : base(game, width, height, posX, posY)
        {
            this.Init(levelName);
        }

        private void Init(String levelName)
        {
            m_sysLevel = this.game.levelReader.ReadLevel(levelName);
            this.m_addThese = this.m_sysLevel.Entities();
            this.m_sysLevel.ClearCurrentEntities();
            m_sysRenderer = new Systems.Renderer((int)dimensions.X, (int)dimensions.Y, m_sysLevel);
            m_sysKeyboardInput = new Systems.Input(this.game.inputMap.actionMap);
            m_sysMovement = new Systems.Movement(m_sysLevel);
            m_sysRule = new Systems.Rule(this.game, m_sysLevel);
            m_sysAnimatedSprite = new Systems.AnimatedSprite();

            AddAndRemoveEntities();

            m_sysLevel.Start();
        }

        private void AddAndRemoveEntities()
        {
            foreach (var entity in m_removeThese)
            {
                RemoveEntity(entity);
            }
            m_removeThese.Clear();

            foreach (var entity in m_addThese)
            {
                AddEntity(entity);
            }
            m_addThese.Clear();
        }

        private void AddEntity(Entity entity)
        {
            m_sysRenderer.Add(entity);
            m_sysKeyboardInput.Add(entity);
            m_sysMovement.Add(entity);
            m_sysRule.Add(entity);
            m_sysAnimatedSprite.Add(entity);
            m_sysLevel.Add(entity);
        }

        private void RemoveEntity(Entity entity)
        {
            m_sysRenderer.Remove(entity.Id);
            m_sysKeyboardInput.Remove(entity.Id);
            m_sysMovement.Remove(entity.Id);
            m_sysRule.Remove(entity.Id);
            m_sysAnimatedSprite.Remove(entity.Id);
            m_sysLevel.Remove(entity.Id);
        }

        public void CombineList(ref List<Entity> l1, List<Entity> l2)
        {
            l1 = l1.Concat(l2).ToList();
        }
        public override void Update(GameTime gameTime)
        {
            if (this.game.keyboard.JustPressed(Keys.Escape))
            {
                this.game.RemoveFrame();
            }

            m_sysKeyboardInput.Update(gameTime);
            List<Systems.Action> actions = m_sysKeyboardInput.Actions();
            foreach (var action in actions)
            {
                switch (action)
                {
                    case Systems.Action.Undo:
                        m_sysLevel.Undo();
                        break;
                    case Systems.Action.Reset:
                        m_sysLevel.Reset();
                        break;
                }
            }

            switch (m_sysMovement.levelState)
            {
                case LevelState.Win:
                    this.game.RemoveFrame();
                    break;
            }

            foreach (Systems.System sys in new List<Systems.System>{ m_sysLevel, m_sysRule, m_sysMovement })
            {
                CombineList(ref m_removeThese, sys.RemoveThese());
                CombineList(ref m_addThese, sys.AddThese());
                sys.ClearEntities();
            }

            if (m_addThese.Count != 0 || m_removeThese.Count != 0)
            {
                AddAndRemoveEntities();
                m_sysAnimatedSprite.ForceUpdateEntities(); // Move this to a better spot!
            }

            m_sysMovement.Update(gameTime);

            if (m_sysMovement.HasUpdated())
            {
                m_sysLevel.ClearTileSet();
                m_sysLevel.FillTileSet();
                m_sysRule.Update(gameTime);
            }

            m_sysAnimatedSprite.Update(gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //m_sysRenderer.Update();
            m_sysRenderer.Draw(spriteBatch);
        }

    }
}
