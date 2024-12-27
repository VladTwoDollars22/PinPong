using PinPong;
using SFML.Graphics;
using SFML.Window;

RenderWindow window = new RenderWindow(new VideoMode(1600, 900), "Game window");
window.Closed += WindowClosed;

while (window.IsOpen)
{
    window.DispatchEvents();


    window.Clear(Color.Green);


    window.Display();
}


static void WindowClosed(object sender, EventArgs e)
{
    RenderWindow w = (RenderWindow)sender;
    w.Close();
}