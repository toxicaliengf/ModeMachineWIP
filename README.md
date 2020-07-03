# ModeMachine
**this library is currently WIP!**

<a href = "https://www.dropbox.com/s/26usq6dni1qexnc/ModeMachine.dll?dl=0">Download DLL Here</a>
<a href = "https://www.dropbox.com/s/cvcqiigb2hyik37/ModeMachineSampleProject.zip?dl=0">Download sample project here (WIP)</a>

Current to-do list:
- [x] Basic architecture, push & remove modes
- [x] OnPush, OnRemove, OnStackChanged events
- [x] Stack querys: GetDepth
  - [x] Channels for filtering modes with GetDepth
- [x] Custom inspectors for debugging Mode and ModeStack
  - [ ] Needs to have functionality for configuring channels
- [ ] Support passing arguments to modes
- [ ] Write literally any documentation
- [ ] Template implementation (examples & templates for extending the system):
  - [ ] Dependency injection with modes
  - [ ] Transitions
  

**Notes**

Now with less features!

This repo currently contains only the (in-progress) source code for the 2020 version of mode machine. Contact me directly if you need access to an older version & I'll see what I can do!

ModeMachine's intended use in unity projects is with a .dll (to leverage the 'internal' keyword for encapsulation). If you want to change the source code, I recommend rebuilding the .dll rather than use the source code directly in Unity. Note that you'll need to change the build path in the .csproj file
