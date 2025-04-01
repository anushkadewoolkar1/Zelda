README For Display System - Paul Paciorek:

The most important property to uphold in the display system is that only one display must be drawn and updated at a time.
This will allow for menus to remain separate from the rest of the game, as well as prevent any conflicting updates between
two displays.

Game1 should call Update() and Draw() on its currDisplay object, which holds this unique display property. Management of this
display object is the responsibility of ICommand, which will switch the currDisplay property of Game1 to the desired display

There exists two sets of Input->Command sets, used based on whether or not a menu is active. The control flow for this is
stored in an IController boolean field (should have few access points as it could be a messy fix if a potential conflict occurs)

BaseMenu class will hold the common behaviors of the menus: the command execution system, the display switching system,

Subclasses will draw a unique SpriteBatch, as well as hold a unique set of commands. User input is handled in Game1, but 

IDisplay classes:
	-StartMenu (Enter from *programstart, Options, Score, Win, Death    Exit to Options, Score, NewGame, *programexit)
	-OptionsMenu (Enter from Level, Start, Credits     Exit to Level, Start, Credits, NewGame, *programexit)
	-ScoreMenu (Enter from Death, Score, Start     Exit to Start) - Maybe?
	-InventoryMenu (Enter from Level    Exit to Level)
	-WinMenu (Enter from Level    Exit to Start, NewGame, Score, *programexit)
	-DeathMenu (Enter from Level    Exit to Start, NewGame, Score, *programexit)
	-CreditsMenu (Enter from Options    Exit to Options)
	-Level (Enter from Start, Options, Inventory, Win, Death    Exit to Options, Inventory, Win, Death)
	(NewGame reset should be able to be triggered by Start, Options, Win, and Death menu)