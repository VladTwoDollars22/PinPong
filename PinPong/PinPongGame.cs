using System;
using System.IO;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PinPong
{
    public struct Player
    {
        public GameObject playerObj;
        public GameObject outObj;
        public int wins;

        private string playerTexture = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\textures\player.jpg");
        private string finishTexture = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\textures\out.jpg");

        public Player(Vector2f playerSpawnPoint, Vector2f outObjPosition)
        {
            playerObj = new(playerSpawnPoint, new Vector2f(0.1f, 0.1f), playerTexture);
            outObj = new(outObjPosition, new Vector2f(0.1f, 0.9f), finishTexture);
            wins = 0;
        }
    }

    public class PinPongGame
    {
        private Player _player1;
        private Player _player2;

        private RenderWindow window = new RenderWindow(new VideoMode(1600, 900), "Game window");

        private GameObject _border1;
        private GameObject _border2;

        private GameObject _projectile;

        private SFML.Graphics.Font _font;
        private SFML.Graphics.Text _text;

        public void GameProcess()
        {
            Initialisation();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                InputProcess();
                Logic();
                Render();
            }
        }

        private void Initialisation()
        {
            window.Closed += WindowClosed;

            _font = new SFML.Graphics.Font(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\fonts\font.ttf"));

            _projectile = new(new Vector2f(760, 450), new Vector2f(0.05f, 0.05f), @"..\..\..\textures\asteroid.png");
            _border1 = new(new Vector2f(0, 0), new Vector2f(5f, 0.1f), @"..\..\..\textures\barrier.jpg");
            _border2 = new(new Vector2f(0, 880), new Vector2f(5f, 0.1f), @"..\..\..\textures\barrier.jpg");

            _text = new SFML.Graphics.Text(GetTextFilling(), _font, 50)
            {
                Position = new Vector2f(750, 100),
                FillColor = Color.White
            };

            _player1 = new Player(new Vector2f(35, 450), new Vector2f(0, 0));
            _player2 = new Player(new Vector2f(1550, 450), new Vector2f(1580, 0));
        }

        private void Logic()
        {
            FacesLogic();
            MovingLogic();
        }

        private void MovingLogic()
        {
            _player1.playerObj.Move();
            _player2.playerObj.Move();
            _projectile.Move();
        }

        private void FacesLogic()
        {
            if (_projectile.isFasedWith(_player1.playerObj) || _projectile.isFasedWith(_player2.playerObj))
            {
                _projectile.Reflect(false, true);
            }

            if (_projectile.isFasedWith(_border1) || _projectile.isFasedWith(_border2))
            {
                _projectile.Reflect(true, false);
            }

            if (_projectile.isFasedWith(_player1.outObj))
            {
                _player2.wins++;
                ResetProjectile();
            }

            if (_projectile.isFasedWith(_player2.outObj))
            {
                _player1.wins++;
                ResetProjectile();
            }
        }

        static void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }

        private void Render()
        {
            window.Clear(Color.Black);

            DrawObjects();

            DrawText();

            window.Display();
        }

        private void DrawObjects()
        {
            window.Draw(_player1.playerObj.GetSprite());
            window.Draw(_player2.playerObj.GetSprite());

            window.Draw(_border1.GetSprite());
            window.Draw(_border2.GetSprite());

            window.Draw(_player1.outObj.GetSprite());
            window.Draw(_player2.outObj.GetSprite());

            window.Draw(_projectile.GetSprite());
        }

        private void DrawText()
        {
            _text.DisplayedString = GetTextFilling();
            window.Draw(_text);
        }

        private string GetTextFilling()
        {
            return _player1.wins.ToString() + ":" + _player2.wins.ToString();
        }

        private void InputProcess()
        {
            _player1.playerObj.SetVelocity(new Vector2f(0, 0));
            _player2.playerObj.SetVelocity(new Vector2f(0, 0));

            if (_projectile.GetVelocity() == new Vector2f(0, 0) && Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                _projectile.SetVelocity(GetRandomVelocity());
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                _player1.playerObj.SetVelocity(new Vector2f(0, -1));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                _player1.playerObj.SetVelocity(new Vector2f(0, 1));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                _player2.playerObj.SetVelocity(new Vector2f(0, -1));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                _player2.playerObj.SetVelocity(new Vector2f(0, 1));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.R))
            {
                ResetProjectile();
            }
        }

        private Vector2f GetRandomVelocity()
        {
            Random rand = new Random();

            float randomX = (float)(rand.NextDouble() * 1.0 - 0.5);
            float randomY = (float)(rand.NextDouble() * 1.0 - 0.5);

            return new Vector2f(randomX, randomY);
        }

        private void ResetProjectile()
        {
            _projectile.SetVelocity(new Vector2f(0, 0));
            _projectile.SetPosition(new Vector2f(800, 450));
        }
    }
}
