# Tron Endless Runner

## Table of Contents
* [Builds](#builds)
* [Project Overview](#project-overview)
* [Features and Highlights](#features-and-highlights)
* [Technical Details](#technical-details)
* [Tools and Technologies](#tools-and-technologies)
* [Gifs](#gifs)

------
## Builds
* [WebGL](https://alsandbox.github.io/Tron-Endless-Runner/) 
* [Windows](https://github.com/alsandbox/Tron-Endless-Runner/releases/tag/winbuild)

------
## Project Overview
For my first project, I decided to take the popular genre of endless runners and mix it with the aesthetics of Tron. In this game, player takes control of bike races. The objective is simple navigate the digital landscape, collect gems and extra lives, and overcome challenges as the velocity intensifies. This is the first project that I made on my own from scratch (not counting assets, of course).
![](https://github.com/alsandbox/Tron-Endless-Runner/blob/main/ReadmeGifs/gameplay.gif)

------
## Features and Highlights
#### Key Features:
* **Strategic Dodging**: You need to maneuver between obstacles, avoiding collisions and saving points and lives.
* **Gem Collection**: Pick up shiny gems along the way to increase your score and earn extra lives. 
* **Lives and Progression**: Obtain extra lives to extend your journey and achieve higher scores. 

#### Technical Insights:
* **Procedural Dynamic Object Recycling**. I took a bit from procedural generation and a bit from Object Pooling, to dynamically create relatively unique objects by overusing them in real time.
* **Faux Motion Floor Texture**. Instead of a moving bike and constant road creation, I scroll the texture on the floor in real time, synchronizing it with the movement of obstacles and gems.
* **Data Persistence between sessions**. I made sure that the player only sees the hints during their first session on the device, as well as being able to view their best scores on the main menu and at the game over screen, regardless of the session.

------
## Technical Details
#### Singleton Pattern Implementation.
I employed the Singleton design pattern to demonstrate my understanding of creating globally accessible and unique instances. This pattern is the most popular and controversial, as well as beginner-friendly. 

#### Data Persistence between sessions. 
I implemented two different approaches only to show my knowledge. In my case, I could get by with just PlayerPrefs, but in the case of a complex game I would use PlayerPrefs for simple non-sensitive data and Serializing for more complex non-sensitive data.
* **Serializing data & JSON**. I chose Serializing data and JSON to save the best score between sessions. 
* **PlayerPrefs**. I used PlayerPrefs to display helpful hints exclusively for first-time players, sparing those playing a second or more time from unnecessary information.

#### Scriptable Objects.
I used Scriptable Object to handle tags. It allows for enhanced code organization and simplifies debugging instead of relying on string-based tags directly in my scripts.

#### Adaptive UI Design with Canvas Scaler.
I made use of the Canvas Scaler component to make the game's UI responsive at various screen sizes and resolutions.

#### Resource Optimization Techniques.
* **Procedural Dynamic Object Recycling**. I implemented a Procedural Dynamic Object Recycling system to optimize performance and memory efficiency. In the game, obstacles are continuously spawned until the first obstacle touches a specific object in the scene. At that point, the spamming stops, and the recycled object's rotation angle is dynamically altered while its position resets to a random distance within a predefined range. This approach introduces variability in obstacle positions relative to each other, reducing the likelihood of collisions between obstacles and gems. By reusing objects and maintaining relative uniqueness, this system significantly reduces the load on the garbage collector, resulting in a smoother gameplay experience and improved overall performance.
* **Faux Motion Floor Texture.** By simple texture scrolling, I achieved the visual effect of a dynamically moving floor surface for performance optimization. You don't need to create a large track and store it in memory, or constantly create parts of the track with obstacles on it if you can create the illusion of the player moving forward on one small section of the quad.

------
## Tools and Technologies
#### Development Tools
* Unity game engine (version 2021.3.7f1)
* Visual Studio 2022

#### Graphics and Art
* **Background Assets**: [Tron inspired VR playground model](https://skfb.ly/6xEqo). I took the floor texture from it, as well as the bike and obstacle models.
* **Gems**: [Simple Gems Ultimate Animated Customizable Pack](https://assetstore.unity.com/packages/3d/props/simple-gems-ultimate-animated-customizable-pack-73764). I didn't use the author's animation but instead added a simple constant rotation around its axis.
* **Audio**
   - Main Background Sound: [8-Bit Arcade - Tron Legacy (End Titles) [8-Bit Computer Game Version]](https://open.spotify.com/track/2TidjDeKyGnj0va4xt42Vu?si=fb93294c463a4a4c)
   - All other sounds: [JDSherbert - Ultimate UI Sound Effect Pack [SFX] (Deprecated)](https://assetstore.unity.com/packages/audio/sound-fx/ultimate-ui-sound-effect-pack-sfx-228228)

------
## Gifs

#### I like the minimalism and inconspicuous details, such as the ability to navigate menus with both the mouse and keyboard.
![](https://github.com/alsandbox/Tron-Endless-Runner/blob/main/ReadmeGifs/menu-cursor.gif)
_____
![](https://github.com/alsandbox/Tron-Endless-Runner/blob/main/ReadmeGifs/menu-keyboard.gif)

#### Hints for a new player
![](https://github.com/alsandbox/Tron-Endless-Runner/blob/main/ReadmeGifs/hints.gif)

#### Pause menu
![](https://github.com/alsandbox/Tron-Endless-Runner/blob/main/ReadmeGifs/game-menu.gif)

#### The use of extra life
![](https://github.com/alsandbox/Tron-Endless-Runner/blob/main/ReadmeGifs/gameplay-using-life.gif)
