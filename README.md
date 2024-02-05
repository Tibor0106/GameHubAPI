# GameHubAPI
----
API for http://kadarmarcell.hu/GameHub
## CONTROLLERS
---
### USER
---
- register | Registers the user
- verify/**verifykey** | Verifies the user's account
- login | Logs the user in
- getUserByName/**username** | Gets a user by their name(usable in: search, etc)
- getUserById/**userid** | Gets a user by their id, for example: banning them, etc
- getFriends/**userid** | Gets a user's friends, this only returns userid and friendid
- getFriendsData/**userid** | Gets a user's friends, this returns the friends' user data
- getStats/**userid** | Gets all game stats from a user
- getStat/**userid**/**gameId** | Gets the stat of a game from user
- updateHeartBeat/**userid** | Updates the heartbeat of a user
- getLastHeartBeat/**userid** | Gets the last heartbeat of a user
- updateHeartBeats | Updates online statuses of all users.

### FRIEND
---
These don't need any explanation as they are self explanatory.

- sendFriendRequest
- getSentFriendRequest
- acceptFriendRequest
- declineFriendRequest
- removeFriend

### CHAT
---
- getUserMesasges/**id**/**page** | Gets all messages from a user
- getMessagesWithUser/**id**/**friendId** | Gets a user's messages with a specific(**friendId**) user
- sendMessage/**senderId**/**receiverId**/**message** | {senderId} user sends a message to **receiverId** user, **message** being the messageBody
- editMessage | Edits a message; Have yet to be implemented in Frontend.
- DeleteMessage/**id** | Deletes a message with messageId of **id**, yet to be implemented in Frontend.

### GAME
---
- addGame | Adds a game to the database.
- getGame/**gameId** | Gets a game from the database.
- editGameStatistics/**gameId** | Edits a game's information.
- removeGame | Removes a game from the database.
