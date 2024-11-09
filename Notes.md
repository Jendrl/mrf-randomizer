### About encounters
Encounters in the Encount.TBL seem to be separated into 28 bytes each, except for the header which is 20 bytes long.

The start of these encounters can is somewhat consistently indicated by certain 2 byte formations
* FF-FF
* C1-01
* 5A-02

#### Examples
|start sequence|payload|
|--------------|-------|
|FF-FF|00-00-00-00-00-00-4B-00-00-00-00-00-00-00-1E-01-44-00-3E-00-36-00-2E-00-00-00|
|5A-02|00-00-30-04-01-00-29-01-00-00-00-00-00-00-97-01-00-00-00-00-00-00-00-00-00-00|

An enemy is always two bytes long (little endian). Let's consider the first example and try to find the enemies by converting the bytes to a hexadecimal and then to a decimal number. After that, we just need to look into Enemy.dat to find the corresponding enemy name.
- 1E-01 &rarr; 011E &rarr; 286 &rarr; EC_SOLDIER_MAGIC_13
- 44-00 &rarr; 0044 &rarr; 68 &rarr; EC_magic_Skeleton01
- 3E-00 &rarr; 003E &rarr; 62 &rarr; EC_arrow_Skeleton03
- 36-00 &rarr; 0036 &rarr; 54 &rarr; EC_lancer_Skeleton03
- 2E-00 &rarr; 002E &rarr; 46 &rarr; EC_sword_Skeleton03

What the start of the payload signifies is not completely clear, but with some tests I could change the encounter music by changing certain bytes at the start.

### Boss Fight Notes
Boss fights are tricky. I was against replacing them in this game since they are often heavily scripted and I did not want the player to crash or softlock during the game.
In the following is a list of tested bosses. Bosses that were seen as not critical were included in the set of enemies a regular enemy in an encounter could be replaced with.

* 502 - fire dragon, works, but without animation
* 503 - poison dragon, works, but without animation
* 504 - super boss dragon, would maybe work in fight, but model "sinks" and enemy disappears on map
* 512 - Klinger
* 513 - Klinger Soldier
* 514 - Undead Grius
* 515 - Zorba
* 516 - egg human - not targetable
* 520 - worm human - not targetable
* 521 - heismay - can deadlock with clones
* 522 - same heismay stuff
* 523 - baby human
* 525 - tpose invincible glodell
* 526 - invincible glodell dog
* 527 - crashing glodell
* 528 - glodell dog (working!)
* 529 - kraken, does not work
* 530 - dragon god human - crashes
* 531 - island human - works, but without color effect
* 532 - louis - kinda works, but without animation
* 536 - mine (?) dragon - works
* 537 - dragon - devourer of nations, same as all others
* 540 - gideaux - invincible
* 542 - rella alone - kinda works? but no reward
* 543 - rella dragon - works like other dragons
* 544 - second louis fight - no rewards
* 545 - melancholia zorba attack form - does not crash during form shift, but it does not work
* 548 - king louis
* 549 - More
* 550 - Monster louis - crashes
* 551 - shadow baby human
* 552 - work human - somehow targetable, but no animations
* 553 - some dog? - works
* 554 - heismay? - not showing any enemies
* 555 - glodell guard form, tposing, kinda working?
* 556-559 monster louis with masks; sinking through map
* 560-565 - EX humans, work except egg which just looks too big in fight
* 569 - hero shadow; does not appear
