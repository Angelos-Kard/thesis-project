# Objects
- `MixedRealityToolkit`: contains the Toolkit itself
- `MixedRealityPlayspace`: ensures that the headset/controllers and other required systems are managed correctly in the scene
  - `Main Camera`: child to the MixedRealityPlayspace object
***
- For an object to be grabbable, it must have 3 components:
  - Collider (automatically attached by Unity)
  - Object Manipulator (Script) component
    - Constraint Manager (Script): dependency (it is added automatically with Object Manipulator)
  - NearInteractionGrabbable (Script) component

# Components
- [`Object Manipulator`](https://learn.microsoft.com/el-gr/windows/mixed-reality/mrtk-unity/mrtk2/features/ux-building-blocks/object-manipulator?view=mrtkunity-2022-05): Add to objects to make them interactable (movable, scalable, and rotatable) using one or two hands
  - To make the object respond to near articulated hand input, add the `NearInteractionGrabbable` script as well
- Collections: useful for organizing and arranging objects
  - Grid Object Collection
  - Scatter Object Collection
  - Tile Grid Object Collection
- `ToolTipSpawner`: Create tooltip
  - Focus Enabled: Tooltip appears when the user looks at the object
- `EyeTrackingTarget`: Add eye tracking to object
- `SpeechInputHandler`: Handles voice commands

# Interaction Models
- Hands and motion controllers model
  - Direct manipulation with hands (`ObjectManipulator`, `NearInteractionGrabbable`)
  - Point and commit with hands
  - Motion controllers
- Hands-free model
  - voice input
  - gaze and dwell
- Gaze and commit
  - Commit methods include voice commands, a button press, or a hand gesture.
  - There are two types of gaze input: head, eye-gaze

# Solvers
Solvers calculate an object's position and orientation by using predefined algorithms. Solvers provide a wide range of behavior to attach objects to other objects or systems.
- Packages &rarr; Mixed Reality Toolkit Foundation &rarr; SDK &rarr; Features &rarr; Utilities &rarr; Solvers
- Directional Indicator solver (Component: DirectionalIndicator): Pinpoints where the target is
  - If tracked target type is Head, then by defining Directional Target, you selct the object that will be tracked
- Tap to place solver: Tap to move a target object

# Menu
- Packages &rarr; Mixed Reality Toolkit Foundation &rarr; SDK &rarr; Features &rarr; UX &rarr; Prefabs &rarr; Menus

# Tooltip
- Packages &rarr; Mixed Reality Toolkit Foundation &rarr; SDK &rarr; Features &rarr; UX &rarr; Prefabs &rarr; Tooltip

# Voice Commands:
- MixedRealityToolkit
  - MixedRealityToolkit > Input > Speech > Clone `DefaultMixedRealitySpeechCommandsProfile` > Speech Commands > Add a New Speech Command

# Spatial Anchors
- Anchor: A point in space that you want the devices to track
  - For each anchor, Anchor Manager creates GameObjects.
  - Device performs more work to update the anchor's position and orientation

# Spatial Audio
- [https://github.com/microsoft/spatialaudio-unity](https://github.com/microsoft/spatialaudio-unity)
- [Spatial Sound Best Practices](https://learn.microsoft.com/en-us/windows/mixed-reality/design/spatial-sound-design)
- [Spatial Audio Tutorial](https://learn.microsoft.com/en-us/training/modules/spatial-audio-tutorials-mrtk/)
- Spatial sound on the HoloLens is enabled using an audio spatializer plugin
  - Edit > Audio > Spatializer > Enable the Microsoft HRTF (head-related transfer function) extension
    - HoloLens 2 includes dedicated hardware that can be utilized to avoid burdening the application processor, thus "offloading" the processing of HRTF-based algorithms
  - Microsoft Spatial Sound only supports sampling rates of 48 kHz currently
    - You should also set your System Sample Rate to 48000 to prevent an HRTF failure in the rare case that your system output device isn't set to 48000 already

# Spatial Awareness
- FOI (Field of Interest of depth sensors for spatial mapping) is larger than the FOV
- Room Data (SR Mesh)
- Scene understanding
- Spatial Observers
- Best practises:
  - Make scanning of the room part of your app in a fun way
  - Check boundaries of the room
  - Show mesh only if it's necessary
- Camera used for spatial mapping can only see 3.1m in front of the user
- Raycasting is affected by holes in the mesh and hallucination (mesh of objects that aren't there)
- Set up scanning experiences:
  - Scanning may not be needed: the app needs only what is in front of the user
  - Spatial coordinate system
  - Scan part of or the whole room
  - Take an initial snapshot of the environment (if no checks for changes in the environment are needed)

# Build & Deploy Application
- File &rarr; Build Settings &rarr; "Add Open Scenes" &rarr; "Build" &rarr; Select Folder