# Unity Project: Still uncertain racing game

## how to use git:

Note: These instructions use git from the command line. If you're on windows,
you should install [git bash](https://gitforwindows.org/). On Linux or Mac git should work
in the default terminal.

1. Clone this repository to your local machine

	git clone https://github.com/schadenfreude2030/unity-racing-game.git

2. Create a branch you want to work on. Remember to create a seperate branch for every 
feature that you'd like to work on (don't create a branch "development-by-tobias" and use 
it for all your work)

	git checkout -b <meaningful branch name like "add-ai-player">

or switch to an existing branch
	
	git checkout <branch name>

3. After finishing work, check your changes

	git status

4. If everything seems fine, commit those changes

	git commit -m <meaningful commit message like "added ai player">

For style points, use emojis according to the [gitmoji guide](https://gitmoji.dev/)

5. Push your changes to the repository

	git push origin <branch name>

6. If you finished working on your branch, create a pull request via the Github interface. 
Then other contributers can have a look at your changes before merging them into the master branch.

If you have any further questions regarding working with git, just hit me up

## current state of this project

I created an empty unity project and added some directories and meta files for future structuring according to the
[example repository](https://github.com/Westdrive-Workgroup/Corona-Chaos). I guess we should meet again and discuss
how to start this project