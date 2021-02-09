# Rock climber slingshot

## Game design
In **Rock-climber slingshot** you have one goal - *get your rock-climber to the peak of the stone wall*, where he can camp for the night in the mountain cabin. You do that by shooting him out of your giant slingshot at the bottom of the stone wall. You need to make sure other rock-climbers don't get there sooner than you, since there is only one free bed in the cabin. Your rivals won't be the only thing making the accend harder - there is an eagle defending his nest on the route to the top of the stone wall and there are even rumors about some uneven rocks.

[Link to the game screen sketch](https://imgur.com/a/YsTqOv4)
## Components
The main part of the controller will be the Arduino UNO in combination with a accelerometer that will monitor the angle and position of a handle that will be on top of a spring - something like a spring door stop, with the accelerometer being on in the handle, with the cables going down to the encased arduino and breadboard through the empty middle section of the spring (inspired by [Line Wobbler](http://wobblylabs.com/projects/wobbler)).  
The casing for the breadboard and Arduino will be made out of Lego's to protect the circuitry and to make sure the project survives the potential plane travel back to UK.
Basic sketch of the controller:

[Link to a picture of the controller sketch](https://imgur.com/a/u5AGYOe)
## Expected challenges during the development
The biggest challenges in this project I see are the calibration of the accelerometer to work with the Unity system where the game will be designed and putting the accelerometer in the knob on the of the spring safely.


## User stories

 - "I want to be able to launch the rock-climbers with a feedback from the controller"
	 - The spring on the contoller will make sure there is an evident feedback and player feels like they really are shooting from a slingshot.
 - "I want to have fun competing with a friend in this game"
	 - Adding a secondary controller will mean that both of the players will be able to play on one screen.
 - "I don't want the spring to bounce back too fast"
	 - I will be using a thicker, shorter spring to reduce the rebound of the spring so it does not reach the player's hand when coming back and wiggling.

