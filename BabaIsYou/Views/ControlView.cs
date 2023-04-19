using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using BabaIsYou;
using BabaIsYou.UI;
using BabaIsYou.Systems;
using BabaIsYou.Controls;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;

namespace BabaIsYou.Views
{
    public class ControlView : GameView
    {
        public List<TextDisplay> textDisplays = new List<TextDisplay>();
        private Systems.Action? _currentAction = null;
        private TextDisplay _currentTextDisplay;
        private Keys _currentKey;

        private Dictionary<TextDisplay, Systems.Action> _actionUI = new();

        public ControlView(Game1 game) : base(game)
        {
            this.textDisplays.Add(new TextDisplay(this.game, "-- Controls --", new Vector2(this.dimensions.X / 2, 100), Color.White));
            int i = 0;
            foreach (Systems.Action action in Enum.GetValues(typeof(Systems.Action)))
            {
                TextDisplay t = new TextDisplay(this.game, $"{action} : {this.game.inputMap.actionMap[action]}", new Vector2(this.dimensions.X / 2, 200 + (50 * i)), "arial", Color.White, Color.Orange);
                this.textDisplays.Add(t);
                _actionUI[t] = action;
                i++;
            }
            for (i = 0; i < this.textDisplays.Count; i++)
            {
                this.textDisplays[i].Center();
            }
        }
        protected override void LoadContent()
        {
            //this._background = this.game.Content.Load<Texture2D>("Images/sunset");
        }

        public override void Update(GameTime gameTime)
        {
            if (_currentAction != null)
            {
                Keys[] keys = this.game.keyboard.GetPressedKeys();
                if (keys.Length != 0) {
                    FinalizeAction(keys.First());
                }
            }

            if (this.game.keyboard.JustPressed(Keys.Escape))
            {
                this.game.RemoveFrame();
            }
            
            if (_currentAction == null)
            {
                if (this.game.keyboard.JustLeftMouseDown())
                {
                    for (int i = 1; i < this.textDisplays.Count; i++)
                    {
                        if (this.game.keyboard.IsOver(this.textDisplays[i].bounds))
                        {
                            //Debug.Print($"{_actionUI[this.textDisplays[i]]}");
                            StartAction(this.textDisplays[i]);
                        }
                    }
                }
            }
        }

        private void StartAction(TextDisplay textDisplay)
        {
            _currentAction = _actionUI[textDisplay];
            _currentTextDisplay = textDisplay;
            _currentTextDisplay.SetHoverColor(Color.Red);
            _currentTextDisplay.SetColor(Color.Red);
        }
        private void FinalizeAction(Keys key)
        {
            _currentKey = key;
            this.game.inputMap.UpdateInputMapping((Systems.Action)_currentAction, _currentKey);

            _currentTextDisplay.SetString($"{_currentAction} : {_currentKey}");
            _currentTextDisplay.SetHoverColor(Color.Orange);
            _currentTextDisplay.SetColor(Color.White);
            _currentTextDisplay = null;
            _currentAction = null;
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(this._background, new Rectangle(0, 0, (int)this.dimensions.X, (int)this.dimensions.Y), Color.White);
            for (int i = 0; i < this.textDisplays.Count; i++)
            {
                this.textDisplays[i].Draw(spriteBatch);
            }
        }
    }
}
