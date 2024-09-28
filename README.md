# 184UI
UI package for VRC provided by 184

<img src="https://github.com/user-attachments/assets/75613e5d-9ec8-4930-9ad5-283a0f7f7312" width="50%" />

# GameObject Menu
- An item `UI (184)` is added to the GameObject menu.
- Can generate custom UI objects optimized for VRChat.
  - For example, `Navigation`.

# Components
## Layout Baker
- This component removes layout components attached to the same Game Object at build time.
- This reduces heavy layout processing.
## Smart Canvas Collider
- This component optimally positions the Collider for the UiShape attached to the Canvas at build time.
- This prevents the cursor for the UI from appearing in unwanted areas.
