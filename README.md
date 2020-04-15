# ModeMachine
**this library is currently WIP!**

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

ModeMachine's intended use in unity projects is with a .dll (to leverage the 'internal' keyword for encapsulation). At some point i'll provide the .dll on here but for now you have to build it yourself. Note that you'll need to change the build path in the .csproj file
