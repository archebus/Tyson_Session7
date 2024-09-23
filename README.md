# Tyson_Session7

## Task 1 - Spooky Hotel (Large Task)
You are tasked with writing a program that will allow the player to navigate and explore a hotel that is described with a level file.
A level file will specify the rooms on the first line delimited by a space, following that, each line will describe a room's pathway to another room.
The first room listed on the first line is the starting point of the level.
 
The level file structure :
```
START FOYER ELEVATOR 
START > NORTH > FOYER 
FOYER > SOUTH > START 
START > WEST > ELEVATOR
```
 
Each pathway is broken up by spaces and the > symbols. <roomA> > WEST > <roomB> this translates to: roomA's west's pathway connects to roomB
 
Annotation for the test level file:
```
START FOYER ELEVATOR //START is the first room of the hotel. 
START > NORTH > FOYER //START's north pathway connects to the FOYER 
FOYER > SOUTH > START //FOYER's south pathway connects to the START 
START > WEST > ELEVATOR //START's west pathway connects to the ELEVATOR
```

The commands that a player can input:
```
NORTH
SOUTH
EAST
WEST
QUIT
```
QUIT command allows the user to quit the program, please ensure that you clean up any memory you have allocated.
 
The NORTH, SOUTH, EAST and WEST commands will allow the player to move between the rooms via the room's pathways. Each room has a maximum of 4 pathways.
 
A room will outline if the paths that are available to it by specifying the direction at the side of the room. For example if the room has a path to another room by going north it will show N on the north side of the room. Example:
```
 ---N--- 
|       | 
|       |
|       | 
|       | 
|       | 
 -------
```
If it does not have a path, it will be a - or | depending on the side it is on.
If a user specifies a direction that does not have a pathway the program should output: **No Path This Way**

If the user inputs an invalid command the program should respond with: **What?**
 
The room is always 9 x 5 (with a space at the start and end of the top and bottom of the room);
 
The name should also be outputted before drawing the room.
```
NORTH 
START
 ---N--- 
|       | 
|       |
|       | 
|       | 
|       | 
 -------
```
Examples: Example 1 with test_dungeon.dg
```
START
 ---N---
|       |
|       |
W       |
|       |
|       |
 -------
NORTH
FOYER
 -------
|       |
|       |
|       |
|       |
|       |
 ---S---
SOUTH
 
START
 ---N---
|       |
|       |
W       |
|       |
|       |
 ------- 
QUIT
```
Example 2 with test_hotel.dg
```
START
 ---N---
|       |
|       |
W       |
|       |
|       |
 -------
WEST
ELEVATOR
 -------
|       |
|       |
|       |
|       |
|       |
 -------
NORTH
No Path This Way
ELEVATOR
 -------
|       |
|       |
|       |
|       |
|       |
 -------
EAST
No Path This Way
ELEVATOR
 -------
|       |
|       |
|       |
|       |
|       |
 -------
QUIT
```
 
## Task 2 - Multiplayer Browser Battleships
 
You are going to create a game called Battleships, this is a classic baord game where two players take turns to see if they can sink each other's battle ships. Each player gets the following ships that they can place on their board, they can facing horizontally or vertically: 
Carrier, 5 spaces
Battleship, 4 spaces
Destroyer, 3 spaces
Submarine, 3 spaces
Patrol Boat, 2 spaces
Randomise the location of each ship on the grid and provide the information to the player.
 
Each turn consists of a player taking a shot at the other player's board and attempting to hit their ship. The other player will need to hit all spaces occupied by enemy ships to win. Consider how you could check to see if a player has won or lost. The players will enter commands using the ranges 0-9 for both X and Y components of the board
 
To get started, you will need to setup the following endpoints.
 
GET /game/new
Will create a new game, this will return: A new game id and a generated token

GET /game/:id/:playerid?token=<token>
This will return the state of the game for the given id and for the player given the playerid (which is either 1 or 2). The token will be attached to the query string.
This should return a JSON object with hit locations and miss locations and if the game has finished or not (and who won)
If the game does not exist, the server should respond with an appropriate error.

POST /game/:id/:playerid/move
The player should be able to send a JSON object that provides the X, Y coordinates of the board, the server should respond with an update 
If the move was valid, it should update and confirm either hit or miss
If the move was invalid, the server should let the player try again
If the move was made during the other player's turn, the server should respond with an error

Once your server has implemented this, you can test your server using curl or you can build a web-frontend that will draw the board and allow the user to input using a form and poll the server.
 
Task 3 - Make it a mobile game!
 
Transform the game into a mobile app, use React Native to build a mobile version of the game.