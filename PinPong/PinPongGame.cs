using System.Reflection;
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

        private string playerTexture = "textures\\player.jpg";
        private string finishTexture = "textures\\out.jpg";

        public Player(Vector2f playerSpawnPoint,Vector2f outObjPosition)
        {
            playerObj = new(new SFML.System.Vector2f(0, 0), new SFML.System.Vector2f(0.1f, 0.1f), playerTexture);

            outObj = new(new SFML.System.Vector2f(100, 100), new SFML.System.Vector2f(0.1f, 0.1f), finishTexture);

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
        public void GameProcess()
        {
            Initialisation();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                InputProcess();
                Logic();
                Draw();
            }
        }
        private void Initialisation()
        {
            window.Closed += WindowClosed;

            _projectile = new(new SFML.System.Vector2f(10, 10), new SFML.System.Vector2f(0.1f, 0.1f), "textures\\asteroid.png");
            _border1 = new(new SFML.System.Vector2f(50, 50), new SFML.System.Vector2f(0.1f, 0.1f), "textures\\barrier.jpg");
            _border2 = new(new SFML.System.Vector2f(50, 25), new SFML.System.Vector2f(0.1f, 0.1f), "textures\\barrier.jpg");

            _player1 = new Player(new Vector2f(35 , 300),new Vector2f(0 , 0));
            _player2 = new Player(new Vector2f(35, 300), new Vector2f(0, 0));
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
            if(_projectile.isFasedWith(_player1.playerObj) || _projectile.isFasedWith(_player2.playerObj))
            {
                _projectile.Reflect(false, true);
            }
            
            if(_projectile.isFasedWith(_border1) || _projectile.isFasedWith(_border2))
            {
                _projectile.Reflect(true, false);
            }

            if (_projectile.isFasedWith(_player1.outObj))
            {
                _player2.wins++;
            }

            if (_projectile.isFasedWith(_player2.outObj))
            {
                _player1.wins++;
            }
        }

        static void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
        private void Draw()
        {
            window.Clear(Color.Black);

            window.Draw(_player1.playerObj.GetSprite());
            window.Draw(_player2.playerObj.GetSprite());

            window.Draw(_border1.GetSprite());
            window.Draw(_border2.GetSprite());

            window.Draw(_player1.outObj.GetSprite());
            window.Draw(_player2.outObj.GetSprite());

            window.Draw(_projectile.GetSprite());

            window.Display();
        }
        private void InputProcess()
        {
            _player1.playerObj.SetVelocity(new SFML.System.Vector2f(0, 0));
            _player2.playerObj.SetVelocity(new SFML.System.Vector2f(0, 0));

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                _player1.playerObj.SetVelocity(new SFML.System.Vector2f(0, -1));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                _player1.playerObj.SetVelocity(new SFML.System.Vector2f(0, 1));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                _player2.playerObj.SetVelocity(new SFML.System.Vector2f(0, 1));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                _player2.playerObj.SetVelocity(new SFML.System.Vector2f(0, -1));
            }
        }
    }
}