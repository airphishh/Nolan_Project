In order for this project to work properly, ALL scenes (the HomeScene, 
HTPScene, and GameScene) MUST be in loaded into the build scene.

To accomplish this, follow the steps below:
1. Drag HomeScene, HTPScene, and GameScene into the Hierarchy window, with
HomeScene being at the top and GameScene being at the bottom
2. Go to File -> Build Settings, and click "Add Open Scenes"
3. In "Scenes in Build" box, right click on Scenes/SampleScene and click
"Remove Selection"
4. HomeScene, HTPScene, and GameScene should have an index of 0, 1, and 2
respectively, if not drag them around in the "Scenes in Build" box so they do 
have their proper index
5. Go back to the Hierarchy window, and unload/remove SampleScene, HTPScene, 
and GameScene, leaving only the HomeScene loaded into the Hierarchy window
6. Click the Game tab and underneath the Resolution dropdown (the button
between the Display dropdown and Scale slider), choose Full HD (1920x1080)
7. You're ready to play the game!