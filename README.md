## SS-Game: Solar System construction game

This application, with the help of an Oculus device, allow the user to get into a room within the universe. 
At the center of the room there are the planets of the solar system placed in a disorderly manner: the users must rebuild the solar system putting the planet in the right orbit.

### Requirements

This project has been realized with Unity version  `2018.2.9f1` and the [Oculus SDK](https://developer.oculus.com/downloads/).

### Project scenes

There are 2 main scenes: the `Intro` and `Solar system -- version demo 2`:
* `Intro` gives an introduction of the game to the user
* `Solar system -- version demo 2` is the real game where the user can interact with the planets to re-build the solar system. 

### Project structure

```
├───Assets
│   ├───audio ## not used
│   ├───CasualGameSounds ## https://assetstore.unity.com/packages/audio/sound-fx/free-casual-game-sfx-pack-54116
│   ├───IntroImg ## first scene introduction image of the game
│   ├───Logo ## OAAB logo 
│   ├───Materials ## planets materials 
│   ├───MilkyWay ## skybox
│   ├───model ## oculus sdk room models
│   ├───Oculus ## oculus sdk
│   ├───Pannelli ## room furniture
│   ├───Pianeti ## other planets features i.e. rings
│   ├───PowerUp ## not used
│   ├───Pre-made ## for planets
│   ├───Prefabs ## solar system 
│   ├───Resources ## oculus sdk
│   ├───Scenes
│   ├───Script ## for user interactions
│   ├───Shaders ## for planets
│   ├───Standard Assets ## unity standard
│   ├───Standard Assets (Mobile) ## unity standard
│   ├───Texture ## planet textures
│   ├───Universal UI, Game & Notification Sound Effects
│   ├───_Creepy_Cat ## https://assetstore.unity.com/publishers/9029
│   └───_MK ## https://assetstore.unity.com/packages/vfx/shaders/mk-glass-100711
├───Packages ## unity standard
├───ProjectSettings ## unity standard
└───UIElementsSchema ## unity standard
```
