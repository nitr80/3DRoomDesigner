# 🏠 3D Room Designer (Unity)

## Overview
A 3D room editor built in Unity that allows users to place, select, and manipulate furniture objects within a virtual environment.  
The project focuses on intuitive object interaction using custom-built translation gizmos and camera controls.

---

## Features
- Object selection and deselection system
- Custom translation gizmo (axis-based movement)
- Mouse and keyboard-based camera & object interaction
- Furniture spawning via UI
- Modular system for extending interaction tools

---

## Controls

### Camera
- **W / A / S / D** → Move camera
- **Mouse Scroll** → Move camera forward / backward
- **Alt + Left Click + Mouse Move** → Rotate camera

### Object Interaction
- **Left Click (on furniture)** → Select object  
- **Left Click (empty space)** → Deselect object  
- **Gizmo Arrows (drag)** → Move selected object  

### UI
- Select a furniture type from the top bar  
- Click the **"+" button** to spawn new furniture  
- Select a furniture object and modify its position, rotation, and scale via the transform UI

---

## Technical Highlights
- Screen-to-world space projection for precise object movement
- Custom gizmo system (instead of Unity built-in tools)
- Input handling for combined mouse + keyboard interaction
- Separation of interaction logic and object data

---

## Assets
- Furniture Models:  
  https://assetstore.unity.com/packages/3d/props/furniture/furniture-free-low-poly-3d-models-pack-260522  

- Icons:  
  https://www.flaticon.com/  

---

## Future Improvements
- Rotation gizmo
- Grid snapping system
- Save/load system (JSON-based)
- Improved gizmo accuracy and UX
- VR interaction support

---

## Demo
[Link](https://youtu.be/I0xqmodzyaQ)

---

## Author
Ege Öztürk  
Computer Science (Informatik) B.Sc. Student at TU Berlin