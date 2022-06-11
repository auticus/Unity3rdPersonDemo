Downloaded character from www.mixamo.com 
Character was saved in format fbx for unity
Moved that fbx file over to a models directory in unity.

Mixamo materials will be missing.  So you have to click on the model.  Then in the properties, make sure the Materials button is selected
(it will be Model | Rig | Animation | Materials)

Make sure Material Creation Mode is Standard (Legacy)
Location >> change from Use Embedded materials to Use External Materials (Legacy)
Hit Apply

Normal map jackery may show up >> Click "Fix Now"

ALSO good idea to duplicate the animations inside the little box and delete the boxed package animation to just keep the animation itself.

STEP 2 - go to Mixamo and grab Animation >> Locomotion
There are several packs to choose from (Fuse characters - Mocap animation captures)
Mixamo can also be used to rig your own models.

Select animation >> Pose - no character as we will be applying it to multiple characters
When this is brought in to unity, it contains a lot of other things that we don't need.  So we extract the animation itself out and thats it.
(so the box icon has a right arrow next to it, click that and duplicate the animation inside, then you can just delete the animation box)
IMPORTANT - in any animation that we want to repeat over and over (like idle) go into the animation and check LOOP TIME.

STEP 3 - Add Animator Component to the Player Prefab (needs a controller)

STEP 4 - Add AnimatorController in Animations folder.  Create Animate Controller (context).  Then drag that into the controller field on the player you did in Step 3.

STEP 5 - double click the controller for player.  To stop them T posing, drag idle in next to Entry so thats the first thing they do.


SET UP CHARACTER CONTROLLER
On the prefab for the character, add a Character Controller component and adjust its center, height, radius (scene view to view)
Then in PlayerStateMachine >> added the CharacterController as well and then in Unity added the reference to that in the prefab.
>> SkinWidth should be 10% of radius
>> Min Distance - changed to 0 to prevent jittering
Once this has been added - colissions etc are just handled for us.  

!! OF IMPORTANT NOTE - Use of Character Controller vs the Rigid Body / Physics which most other people use - Character Controller requires writing your own physics !!

INTERACT WITH MOVEMENT CODE TO BLEND WITH ANIMATION
Mixamo animations will actually move the model.  We don't want that.
Open up the Running animation.  Remove any keyframes that modify the Z position.

When clicking into the animator controller in unity, in other projects we've mapped the Idle to Running, to jumping, etc.  Using it as a state-machine.
We no longer need to do that.  We can do all of this in code. 
Delete all of the states in the animator controller and create a new Blend Tree (we called it FreeLookBlendTree)
In the Blend Tree created two motions and drug up Idle and Running.  The blend is the parameter we need to control in code.
Now in parameters in the same Blend Tree Animator area, we see it created "Blend".  We changed that to FreeLookSpeed.

State Machine for the player was updated to set these parameters.

CINEMACHINE CAMERA
We add a free look camera.  We then create an empty game object in the player and call it camera focus and then drag that transform over to the Follow portion.
Then we move the hips Transform to the lookat.
This gives us a starting view.

However things blow up still because the camera scripts are using the old input system.  So in the camera we remove references to the X and Y Axis (the old input system)'
and instead create a new mapping for the camera.  Last >> we add a Cinemachine Input Provider and populate the axis it wants with the new input we just created in the XY Axis.
At this point, you will need to adjust the properties for the axis speed, etc... to get it to feel right.
(you'll also want to Invert the Y and stop Inverting the X which is as of now the default)

>>ORBITS
Next need to adjust the Orbits property of the camera.  This works how the camera behaves when it is orbiting the cahracter.  If you look at the camera in scene
view you will see red rings which show where the camera is orbiting at the top, middle, and bottom.
>> Binding Mode = Simple Follow with World Up (change to World Space)
>> Spine Curvature = 0.2
>> Top Rig = height 4.5 radius 1.75  (change to 2.5 / 5)
>> Middle = 2.5 / 3 (change to 0.5 / 6)
>> Bottom = 0.4 / 1.3 (change to -0.75 / 5)

If you want camera to look down more on the player, increase the top rig
***There are a ton of settings in the camera that should be looked up.***

PROBLEM - if you go behind a wall... the camera just sits at the wall and you can't see.
Need camera to zoom in and get behind the character.
>> Camera >> Go to bottom with Extensions >> Select CinemachineCollider

Collide Against: Default
Ignore Tag: (change to Player) **make sure player has the player tag!**
Other settings will dampen / smooth the camera if you collide.  Default will snap to

CAMERA RELATIVE MOVEMENT
We want to move forward relative to where the camera is pointed, not relative to the player position.

CREATING THE ENVIRONMENT
If any materials imported are a purple color, select the materials >> Edit >> Rendering >> Materials >> Convert Selected Built-In Materials to URP
Still busted?  Go to the material >> Change shader to whatever it is to Universal Render Pipeline / lit

Some meshes will still be a bit weird. 
>> Click on Base Map (thats the texture, click on the little dot NOT the color) and reset the texture (because it may have cleared it and it willb e white)
Then set Surface Type = Transparent
Then uncheck Preserve Specular Lighting
Also look at smoothness = 0 so the skybox doesn't reflect off of them


TARGETING
Add new game object to the player prefab called Targeter.  It will have a sphere collider that extends out and anything in the radius of this sphere can be targeted
(using the target and targeter scripts)
>> radius = 15
>> enable IsTrigger so it doesn't push them it just detects overlaps
>> add Rigidbody because this is needed for a trigger (set the stats to 0) turn on IsKinematic (physics system won't try to mess with it)

TARGETING CAMERA
Create a Cinemachine >> Target Group Camera for target view.  This uses a Target Group to treat multiple GameObjects as a single
LookAt target.

TargetGroup has a list of targets which we add in code, and it will try to make sure all targets are on screen.

>> Adds Player's "CameraFocus" object into the TargetGroup List
The enemy targeting will be the second element.
Targeting Camera >> Follow CameraFocus, not the TargetingGroup
>> Camera renamed to TargetingCamera
>> Renames it to be TargetingGroup and childs to TargetingCamera

TargetingCamera/Group >> And added the sphere to the list just to see what the game view would do.  Dragging in the cube shows all 3 on screen at the same time.
Setting Radius makes a bigger area around the player and target will basically pad the screen.
Weight - The more you focus on an element with weight the more it zooms on it.  So
>> radius of 2 for all targets
>> player weight = 0.25
>> sphere weight was 1

Camera >> Body (expand it) >> Follow Offset Y = 1.5 (above the player) and Z = -4
Camera >> Aim (Expand it) >> Makes Minimum FOV 60 and Maximum is 65 or so

The camera keeps the target in the center of the screen and the player will still stay in the screen.

In code we want to always face the target.


SETTING UP STATE DRIVEN CAMERA
Player Rig Prefab >> add the State-Driven Camera
It manages the cameras
Delete the camera that comes along with it

Make the two existing cameras child of this new cam object.  
>>Rename to StateDrivenCamera

Set Priority of FreeLookCamera = 11 (higher priority that we start to)
AnimatedTarget:  drag in the player prefab into - otherwise its throwing a warning

State List
>> FreeLookBlendTree (add)
>> Camera - FreeLookCamera

Go to Animator for Player
Make sure you click on Base Layer so you can add a new one
Create New BlendTree called TargetingBlendTree

At this point we can go back to the state-driven camera
Add new state >> TargetingBlendTree
Camera = TargetingCamera

Will tweak later

In code - when we enter FreeLookState - play FreeLookBlendTree and in target state - play TargetingBlendTree
Initially this will be very snappy since there is no real blend and the player will go into T pose etc.

CINEMACHINE TARGET GROUP
Programmatically add items to the Target list in the TargetingGroup child object of the TargetingCamera (which is a child of the StateDrivenCamera object on the PlayerRig prefab)
The Targeter object will hold a reference to this Targeting Group object.

FORCE RECEIVER
handles gravity, knockbacks, etc anything that strikes the player

TARGETING ANIMATIONS
PlayerRig >> Animator - find the Targeting Animator (moving forward backward and straffing left & right)
Targeting Blend Tree (Blend Type is 1D, change 2D Freeform Directional) - now it wants 2 parameters (2 dimensional)
Go to parameters >> add float >> TargetingForwardSpeed (1 and -1)
>> add float >> TargetingRightSpeed (1 and -1)

WEIRD EDITOR ISSUE - the second item is listed as NaN - add a number in that field and that fixes it

Then add your motions
>>Idle when Pos X and Y are 0
>>Running when Y is 1
>>Running when Y is -1 (need a backpedal animation) and change animation speed to -1 (plays running animation in reverse)
>>Animations folder >> Grab Left & Right Strafe and duplicate them to delete the box so the animations are now out 
!!(make sure LoopTime is true so it loops)!!

ISSUE TO FIX WITH MIXAMO
The animation (both straffes) will move off to the side.  We don't want to move the player via animation so we have to go to Window >> Animation >> Animation
>> Hips: Position >> Remove movement of x and z so it doesn't move

BACK TO BLEND TREE FOR TARGETING
>> New Motion >> LeftStrafe >> x = -1 Y = 0
>> New MOtion >> RightStrafe >> x = 1 Y = 0

Your blend tree will now show up like a diamond with a point up, down, left and right.  This is a 2D Blend tree.  You can even have items in between those points.


MIXAMO RIGGING FIX (not sure I needed this)
!! WHY he brought this up - if you want to re-use the same animations for multiple models, they have to have the same bone structure
Some of mixamo's models have different bone structure so you have to pull those model-specific animations !!
Downloading the animation we get under the player:
Hips 
>>LeftUpLeg
>>RightUpLeg
>>Spine

Apparently this is bad and something got changed.

Mixamo - search "Standing Melee Attack"
Standing Melee Attack Downward
Standing Melee Attack 360 Low
Standing Melee Attack Backhand

Download Options - keep defaults but make sure there is no skin (Without Skin)

Pull out the Animations into their base objects.  
Go into the Animator on the character.
Drag the three animations onto the Animator.  They dont need hooked up to anything just arrange them somewhere it makes sense.

COMBO ATTACKS
Need to normalize the time between each animation (meaning a 5 second and a 3 second animation - I need to know what % I am through one).
Problem is that the animator can have us in a blend state of multiple animations so we need to only care about certain ones.  In the animator
for each animation you have a Tag property.

>>Tag = Attack

This lets me know that these are Attack animations.

Additionally the animations can be sped up (or down). This could be good for big weapons that need to slow down.

ADJUSTING DEFAULT STARTING CAMERA
>> Click on the Free Look Camera
>> Adjust the X Axis to swing the camera to wherever you want it to start
You can look at it in the Game view to see where the camera starts itself.

!! FREE LOOK CAMERA JANKY AND SNAPPING AROUND !!
When I add a weapon to the hand of the model, whenever that weapon crosses the Look At target (the "hips") it would snap really close.  This was horrifyingly ugly.
The fix is to either change the Minimum Distance From property of the Collider on the Cinemachine to something bigger than default of 0.1 (I set it to 1 and it seemed to mostly resolve)
OR it was suggested by Gregoryl at Unity to add the weapon to its own layer and then in Collide Against (Defaults to "Default") set that to the layer of the weapon to ignore.  "Exclude that layer
from the "Collide Against" mask.""


**WEAPON HITBOXES**
Animations will turn the hit box on when its swinging and off when its done

Creates a capsule collider which is a child of the Right Hand and surrounds the sword with it.
capsule is a bit fat - radius is 0.2.  Also IsTrigger is set.
Last - uncheck the collider to make sure its turned off.

The animator can call functions in scripts to turn this on.

>> Create new Script - Weapon
>> add Serialize field GameObject weapon
>> public void EnableWeapon()  weapon.SetActive(true);
>> public void DisableWeapon weapon.SetActive(false);

Can only call methods at the same level as the animator which is at the root of the player in this example.
Add Weapon script to the Player.  
>> Drag collider to the weapon script for the GameObject.

>>Animation tab >> find the point where you want to turn it on in the Animation keyframes.
If animation tab is not open its Window >> Animation >> Animation
From here in the SCENE view as you scroll through the animation keyframes you'll see it move in scene view - so choose your animations and this is where we set where things get enabled or not.
>> Put the needle to the frame where you want to turn it on.
>> Click the AddEvent button (its on the left... its a rectangle with a + sign - can be hard to see its under the frame key box.
>> In the function dropdown on the right - it will say Function:  and this is where you enable and disable.


!! THINGS I LEARNED SO FAR !!
I put the weapon damage on the Player game object instead of the weapon collider object.  That was a mistake as it was not registering when I was hitting the enemy and rather was
registering when the player body hit the enemy.

!!Make sure that the collider of your weapon has code to detect when it triggers.!!

!! NAV MESH !!
Window >> AI >> Navigation
Make sure terrain is static!  Make sure all houses and items are static for the nav mesh!
!! Make sure you have gizmos turned on to see it! !!
!! Objects must be marked static for the nav mesh to go around it!!

>> Enemy prefabs add a nav mesh agent
Agent will calculate where we should move but we will actually move them so we can apply the external forces

>> Nav Mesh Agent speed = 1 (we dont care)
>> Angular Speed = 0, Acceleration = 0, Stopping Distance = 1 (so we stop when we need)

