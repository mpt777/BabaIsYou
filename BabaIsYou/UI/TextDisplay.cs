﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabaIsYou.Components;
using BabaIsYou.Entities;
using BabaIsYou;

namespace Breakout.UI
{
    public class TextDisplay
    {
        private Game1 _game;
        private Vector2 _position;
        private string _text;
        private string _fontName;
        private Vector2 _offset;
        private Color _color;
        private SpriteFont _font;
        public Rectangle bounds;
        public TextDisplay(Game1 game, String str, Vector2 position)
        {
            Init(game, str, position, "arial", Color.White);
        }
        public TextDisplay(Game1 game, String str, Vector2 position, Color color)
        {
            Init(game, str, position, "arial", color);
        }
        public TextDisplay(Game1 game, String str, Vector2 position, String fontName)
        {
            Init(game, str, position, fontName, Color.White);
        }
        public TextDisplay(Game1 game, String str, Vector2 position, String fontName, Color color)
        {
            Init(game, str, position, fontName, color);
        }
        private void Init(Game1 game, String str, Vector2 position, String fontName, Color color)
        {
            this._game = game;
            _position = position;
            this._fontName = fontName;
            this._color = color;
            this.SetString(str);
            this.LoadContent();
        }
        public void LoadContent()
        {
            _font = this._game.Content.Load<SpriteFont>(this._fontName);
        }
        private Vector2 OffsetPosition()
        {
            return this._offset + this._position;
        }
        public void Center()
        {
            Vector2 measure = this._font.MeasureString(this._text);
            this._offset.X = -measure.X / 2;
            this._offset.Y = -measure.Y / 2;
            this.SetString(this._text);
        }
        public void SetString(string text)
        {
            _text = text;
            Vector2 measure = this._font.MeasureString(this._text);
            Vector2 pos = this.OffsetPosition();
            this.bounds = new Rectangle((int)pos.X, (int)pos.Y, (int)measure.X, (int)measure.Y);
        }
        public String GetString()
        {
            return this._text;
        }
        public void SetBounds(Rectangle rect)
        {
            this.bounds = rect;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_text != null)
            {
                spriteBatch.DrawString(_font, _text, this.OffsetPosition(), _color);
            }
        }
    }
}
