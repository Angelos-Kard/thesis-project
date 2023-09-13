# Thesis Project

- [Thesis Project](#thesis-project)
- [1. Introduction](#1-introduction)
- [2. Technology](#2-technology)
  - [2.1. Hololens \> Spatial Mapping](#21-hololens--spatial-mapping)
  - [2.2. Hololens \> Spatial Audio](#22-hololens--spatial-audio)
  - [2.3. Hololens \> Voice Input](#23-hololens--voice-input)
  - [2.4. Unity \> Raycast](#24-unity--raycast)
- [3. Analysis Phase](#3-analysis-phase)
- [4. Development Phase](#4-development-phase)
- [5. How to use](#5-how-to-use)

# 1. Introduction
The scope of this thesis project is to develop an app for Hololens 2, which will assist users who face vision impairment (partial or complete vision loss) t and would like to navigate in a specific area, unknown to them.

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
...

## 2.2. Hololens > Spatial Audio
...

## 2.3. Hololens > Voice Input
...

## 2.4. Unity > Raycast
...

# 3. Analysis Phase
...

# 4. Development Phase
...

# 5. How to use
- Wear the Hololens 2 headset and launch the app
- On startup, a scan of the environment will be done (in the direction of user's gaze) in order to retrieve the mesh (spatial mapping). This scan is done in intervals of 3.5 seconds.
- The user can use any voice command. It is suggested the user to usually use the [`Scan`](TODO: Add link) command.
- When the user says `Scan`, then 9 rays are casted from multiple positions[^1].
  - If a ray doesn't hit any object[^2] within the range of 3m from the user, this means that no obstacles is in the way of the user in this particular direction
  - If a ray hits an object, then an alert sound will start to be played, originating from the direction of the hit point ([alert box](TODO: Add link))


[^1]: Mentioned earlier - TODO: add links
[^2]: With the phrase "a ray hits an object", we mean that the ray hits the mesh of an real life object, which was retrieved through the spatial mapping