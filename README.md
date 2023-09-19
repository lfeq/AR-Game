# Traffic Manager AR Game and Asset Bundle Server

This project combines a Unity-based traffic management AR game with a Node.js server for hosting car models via Asset Bundles. In the game, players manage traffic at an intersection, avoiding collisions and preventing long queues that frustrate drivers.

## Prerequisites

Before running this project, make sure you have the following software installed:

- Unity 2022.3 or a later version
- Node.js 16 or a later version
- MySQL 8 or a later version

## Setup

To set up the project, follow these steps:

1. Clone this repository to your local machine.
2. Open the Unity project.
3. Set up a MySQL database with two fields: `demon_name` and `file_path`.
4. Configure the database connection in the `config.js` file.
5. Start the MySQL server.
6. Run `npm install` to install the required dependencies.
7. Identify your local IP address and update line 73 in `index.js` accordingly.
8. Launch the Node.js server by running `node index.js`.
9. In Unity, navigate to Assets > BuildAssetBundles.
10. Open the `UploadAssetBundle.cs` file and update the IP address to your local IP obtained in step 7.
11. Save the file and run the `UploadAssetBundle.unity` scene.
12. Open the `LevelManager.cs` file and update the IP address to your local IP obtained in step 7.
13. Build and run the Unity game on your phone without stopping the server.

## How to Play

To enjoy the game, follow these steps:

1. Print the provided image on a piece of paper.
![Printed Image](PRINTIMAGE.webp)
2. Use the game build on your phone to scan the printed image with your camera.
3. If the image is recognized correctly, a small menu like this will appear.
![Start Screen](StartScreen.jpg)
4. Press "Play."
5. Click on the red spheres to make the cars move forward and prevent collisions.
6. Have fun!

## Code Explanation

The project's code is divided into two main parts: the Unity game code and the Node.js server code.

### Unity Game Code

The Unity game code manages the game's visuals and player input. Key scripts include:

- `CarController.cs`: Controls car movement.
- `LaneManager.cs`: Manages the traffic lanes within the game.
- `TrafficLightManager.cs`: Controls the in-game traffic lights.
- `MenuManager.cs`: Manages the game's menus.

### Node.js Server Code

The Node.js server hosts the asset bundle. Key files include:

- `index.js`: Initiates the server and handles game requests.
- `config.js`: Contains server configuration settings.

Feel free to explore and modify the code to suit your needs and enhance the game experience.
