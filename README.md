# TP_Escape_IT_Unity

TP_Escape_IT_Unity is a Unity-based augmented reality (AR) app designed to work in conjunction with the Escape IT web application. It allows players to experience interactive escape room challenges using AR technology. This repository contains the Unity project files and assets.

## Features

- Augmented reality escape room experience.
- Interactive puzzles and challenges.
- Integration with the Escape IT web app for hint delivery.

## Requirements

- Unity 2020.3 or higher
- ARCore (Android) or ARKit (iOS) support

## Getting Started

1. Clone the repository:

    git clone https://github.com/eryk-poradecki/TP_Escape_IT_Unity.git

2. Open the Unity project folder in Unity Editor.

3. Ensure that you have the required packages and dependencies installed.

4. Set up your development environment for the target platform (Android or iOS).

5. Build and deploy the app to your device.

6. Launch the app on your device and experience the augmented reality escape room challenges.

## Project Structure

The Unity project follows a standard structure, with key directories and files:

- **Assets**: Contains all the project assets, including scripts, scenes, models, textures, and audio files.
- **Packages**: Contains packages required by the project.
- **ProjectSettings**: Contains the project settings files.

Feel free to explore and modify the project files according to your requirements.

## Integration with Escape IT Web App

TP_Escape_IT_Unity is designed to work in conjunction with the Escape IT web application, which allows gamemasters to monitor multiple escape room games and send hints to players.

To set up the integration:

1. Clone the Escape IT web application repository:

    git clone https://github.com/eryk-poradecki/TP_Escape_IT.git

2. Follow the instructions in the [Escape IT Web App README.md](https://github.com/eryk-poradecki/TP_Escape_IT) to install and run the web application.

3. Ensure that both the Unity app and the Escape IT web app are running on the same network.

4. Configure the network settings in the Unity project to establish a connection with the Escape IT web app. Refer to the Unity documentation or project-specific instructions for details on configuring network connections.

Once the integration is set up, the Escape IT web app can communicate with the TP_Escape_IT_Unity app to deliver hints and track player progress.

## License

This project is licensed under the [MIT License](LICENSE).