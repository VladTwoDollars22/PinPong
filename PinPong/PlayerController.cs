
using SFML.Graphics;
using SFML.System;

namespace PinPong
{
    public class PlayerController
    {
        private Texture _texture;
        private Sprite _sprite;
        private Vector2f _spriteScale;
        private Vector2f _velocity;
        public PlayerController(Vector2f spawnPosition)
        {
            _texture = new Texture("abibs");
            _sprite = new Sprite(_texture);

            _spriteScale = new Vector2f(10, 10);

            _sprite.Position = spawnPosition;
            _sprite.Scale = _spriteScale;
        }
    }
}
