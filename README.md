# Thesis Project

- [Thesis Project](#thesis-project)
- [1. Introduction](#1-introduction)
- [1.1. Current Solutions and State of the Art](#11-current-solutions-and-state-of-the-art)
- [2. Technology](#2-technology)
  - [2.1. Hololens \> Spatial Mapping](#21-hololens--spatial-mapping)
  - [2.2. Hololens \> Spatial Audio](#22-hololens--spatial-audio)
  - [2.3. Hololens \> Voice Input](#23-hololens--voice-input)
  - [2.4. Unity \> Raycast](#24-unity--raycast)
- [3. Analysis Phase](#3-analysis-phase)
- [4. Development Phase](#4-development-phase)
  - [4.1. Voice Commands](#41-voice-commands)
- [5. How to use](#5-how-to-use)
- [6. Appendix](#6-appendix)

# 1. Introduction
The scope of this thesis project is to develop an app for Hololens 2, which will assist users who face vision impairment (partial or complete vision loss) t and would like to navigate in a specific area, unknown to them.

- Assistive Technology Device: any item, piece of equipment, or product system, whether acquired commercially off the shelf, modified, or customized, that is used to increase, maintain, or improve functional capabilities of individuals with disabilities. (Technology Related Assistance to Individuals with Disabilities Act of 1988)[^1.1].

[^1.1]: https://guides.library.illinois.edu/c.php?g=526852&p=3602299

# 1.1. Current Solutions and State of the Art
- Cane
  - Help the user to navigate (alongside the tactile paving) and locate obstacles in its route
- Service Dogs
  - Pick items up for their owners
  - Guide their owners
  - Remind medication intake
- Electronic Mobility Aids
  - Use of ultrasonic waves for the detection of obstacles (ex. [Ray Electronic Mobility Aid](https://www.maxiaids.com/ray-electronic-mobility-aid-for-the-blind), [UltraCane](http://ultracane.com/about_the_ultracane))
- [Guidesense](http://www.guidesense.com/en/)
  - Wearable: a belt around the torso of the user - 
  - Sensor module with high-frequency radar technology - Signal goes through clothing materials - Vibration and/or voice feedback
  - Mainly for outdoor use (alerts will be too frequent indoors)
  - Tested by 25 visually impaired people
- [Wayband](https://www.wear.works/wayband)
  - Wearable (armband)
  - Navigate user through a specific route - Provide haptic feedback to inform user for the correct direction
- [NextGuide](https://next-guide.com)
  - Smart cane
  - 1 camera - 160° FoV
  - Detection of obstacles - Use of haptic feedback (tectile poointer and vibration) - Different feedback based on the object detected - Optional audio cues - Guide to correct direction -  Read out text
- [biped](https://www.biped.ai)
  - Wearable: around the neck of the user - Weight: 900g - 3 cameras - 170° FoV - 15 meters distance - 6h battery
  - Bluetooth connection with headphones - Companion app
  - Detection of high and low-level obstacles - Objects detection (use of autonomous driving technology) - One-side detection (for users with hemispatial neglect) - Prediction of collision risk - Sound warning with spatial effect - Working during day and night - Basic GPS feature (uses smartphone's GPS)
  - Cost: 2950€ (one-time payment) / 129€ (monthly payment for 36 months)
  - The project started in 2020 - 5 collaborators, 250+ beta testers, 6 prototypes - It became available in September 2022
- [Unitree Go1](https://shop.unitree.com/products/unitreeyushutechnologydog-artificial-intelligence-companion-bionic-companion-intelligent-robot-go1-quadruped-robot-dog)
  - Weight: 12kg - Speed: 2.5-3.7 m/s
  - Super-sensing system - Intelligent concomitant system - Side-following system - Built-in AI
  - Cost: 2700$ - 3500$
  - [Video](https://www.linkedin.com/posts/unitreerobotics_robot-guide-dogs-activity-7033650939680948224-ImaT?utm_source=share&utm_medium=member_desktop)

# 2. Technology
The technology (devices, software), which was used during the development of the app is:
- Hololens 2
  - We decided to use an AR device in order to take advantage of its compact design along with the hardware and software which has already been implemented for it, making the development process easier.
- Unity
  - It was decided to proced with Unity instead of Javascript due to the vast range of documentation, tutorials and examples
- Mixed Reality Feature Tool
  - Mixed Reality Toolikit Foundations (version 2.7.3)
  - Mixed Reality Toolkit Standard Assets (version 2.7.3)
  - Mixed Reality OpenXR Plugin (version 1.4.0)
- Hololens Emulator

## 2.1. Hololens > Spatial Mapping

Spatial Mapping is a core feature of the HoloLens 2 that scans the user's surroundings and creates a real-time 3D mesh model. This mesh enables virtual objects to interact realistically with the environment, adhering to surfaces, occluding behind obstacles, and responding to physical constraints. In the project, this functionality was used to map the interior space and detect obstacles by analyzing surface geometry, enabling a realistic mixed-reality experience for visually impaired users.


## 2.2. Hololens > Spatial Audio

Spatial Audio simulates sound direction and distance using head-related transfer functions (HRTFs), allowing users to perceive where a sound is coming from in 3D space. The HoloLens 2 achieves this through built-in speakers on both sides of the headset. This project uses spatial audio to alert users about nearby obstacles, allowing them to identify the direction of a hazard without visual input.


## 2.3. Hololens > Voice Input

Voice input on HoloLens 2 allows users to interact with the system hands-free using spoken commands. It follows the "See It, Say It" model, where visible UI elements suggest available voice commands. The device uses a five-channel microphone array to capture input and supports natural language recognition. In this project, voice commands were implemented via Unity’s SpeechInputHandler, enabling users to trigger obstacle detection and navigation functions without relying on physical gestures — a key accessibility feature for users with visual impairments or mobility restrictions.


## 2.4. Unity > Raycast

Raycasting in Unity involves projecting an invisible line (ray) from a point in space (usually a camera or controller) and checking for collisions with game objects. It is used to detect and interact with the virtual environment. In this project, raycasting was used for obstacle detection and voice-triggered navigation, ensuring that the user could receive alerts based on the direction they were facing or pointing.

# 3. Development Phase

The application was developed in Unity using MRTK (Mixed Reality Toolkit) and C#. Spatial Mapping created a live mesh of the environment, crucial for detecting and avoiding obstacles. Spatial Audio was implemented with Microsoft Spatializer to deliver directional sound alerts. Voice commands allowed interaction with minimal physical input, improving accessibility. Raycasting was employed for obstacle detection and spatial interactions. The codebase was modular and responsive, and testing focused on interaction timing and real-world usability. Tools like Visual Studio and the HoloLens emulator aided in continuous integration and debugging.

## 3.1. Voice Commands
|Voice Command | Description |
| ------------ | ----------- |
| `Scan` | 9 rays are casted from multiple positions ([Raycasts Origin Point](#6-appendix)), as they are displayed in the image:<div margin="10px 0" align="center"><img width="25%" src="./Assets/raycast-origin-points.png"></div>The first row is placed at the height of user's gaze. The distance between each Origin Point with its adjacent ones is 0.5 units[^3.1].<br/>When a ray hits the mesh[^3.2] of the room, then an [alert box](#6-appendix) is placed at the hit point in order to alert the user for an obstacle. One alert box correspond to each raycast. If the ray doesn't hit the mesh within 5 units[^4.1], then its alert box is deactivated.<br>If the user uses the command again, the alert boxes are moved to the new hit points.<br><br>**<u>Note</u>**: If the user turns his gaze, the alert boxes stay in their positions. For the alert boxes to follow the movement of the user's head, use the command `Continuous Mode`. |
| `Stop` | All alert boxes are deactivated. |
| `Continuous Mode` | When this mode is activated, then the rays (which have been selected to be activated for this mode) are casted in each frame. The alert boxes are placed on the hit points. Since the rays are casted in each update, this means that the alert boxes follow the movement of user's head.<br>Using the same command, the mode is deactivated. |
| `Hands Mode` | When this mode is activated, then, if the user place his hand in front of him, at the end of the pointer, which extends from his arm and works as a raycast, an alert box is placed in the hit point between the pointer and the mesh[^4.2].<div margin="10px 0" align="center"><img width="25%" src="./Assets/hand-alert-box.png"></div>If no point of the mesh is hit by the pointer, the alert box is deactivated. If the user uses the same command, the mode is deactivated.<br><br>**<u>Note</u>**: If the user does the pinch gesture, the sound of the alert box is temporarily muted. If the user "release" the gesture, the sound is unmuted. <div margin="10px 0" align="center"><img width="25%" src="./Assets/hand-alert-box-pinch.png"></div> |


[^3.1]: Unity doesn't have a specific unit of measurement for distances, so the generic term **units** is used. We can accept that: $1 unit \approx 1 meter$.
[^3.2]: The mesh is retrieved from the spatial mapping.

# 4. Experiment

## 4.1. Experiment Design

Ten volunteers participated in a controlled experiment where they completed a navigation task twice: once using only a traditional cane, and once using the HoloLens 2 application. The test environment included obstacles of varying sizes and placements. Timing, error frequency, and user feedback were recorded for comparison between the two methods. Participants also completed the SUS (System Usability Scale) questionnaire.

## 4.2. Experiment Results

Results showed that participants took longer to complete the path using the HoloLens-based system compared to the cane, with a statistically significant average increase of ~131 seconds. However, the application significantly reduced navigation errors. SUS scores indicated positive user perception, especially regarding directional audio cues and obstacle warnings. Some issues such as delayed recognition and sensor sensitivity were also noted as areas for future improvement.

# 5. How to use
- Wear the Hololens 2 headset and launch the app
- On startup, a scan of the environment will be done (in the direction of user's gaze) in order to retrieve the mesh (spatial mapping). This scan is done in intervals of 3.5 seconds.
- The user can use any of the available [voice commands](#41-voice-commands).
  - It is suggested the user to use frequently the `Scan` command.

# 6. Appendix
- <a name="appendix-alert-box">**Alert Box**</a>: A GameObject, to which has been attached an Audio Source that plays a sound in order to alert the user. In the app, they are visualized as a blue or green cube.\
<div margin="10px 0" align="center">
  <img width="25%" src="./Assets/alert-boxes.png" />
</div>

- <a name="appendix-origin-point">**(Raycast) Origin Point**</a>: It is the point (coordinates) from which a ray is casted. In the app, it is visualized as a white box.\
<div margin="10px 0" align="center">
  <img width="25%" src="./Assets/raycast-origin-points.png" />
</div>
