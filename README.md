# Photon RoomProperties Sync

- If you have any questions/comments, please visit [**Pico Developer Answers**](https://devanswers.pico-interactive.com/) and raise your question there.

## Environment：

- Unity 2019.4.31f1
- Unity XR Platform SDK (Legacy) v1.2.5

## Applicable devices:

- Neo 3 series
- Neo 2 series

## Description：

- This Demo shows how to use Photon's PUN v2 to synchronize the status of objects in the scene (via Custom Room Properties). 

- Players who join later will gain the properties of the room from server and apply the properties to local client as soon as they join the room.

- In the scene "MainScene", there are several fragile walls that disappear when hit by shells. When player A fires a shell and smashes a certain weakwall, we want both player B, who is already in the scene, and player C, who joins the room after the wall disappears, to see that weakwall is no longer in their scene. 

- Game controls: use the rocker of left controller to rotate the perspective, use the rocker of right controller to move forward, press the trigger button to fire shells.  
