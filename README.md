# About

This randomizer randomizes enemies in the Encount.TBL so that overworld encounters are randomized.

- Enemies in regular encounters are replaced with random enemies, rivals or bosses

There are exceptions to replaced encounters, most notably
- Boss encounters are not randomized to prevent deadlocking or crashes due to scripted fights
- Rival encouters are not randomized since some are necessary for story progression
- Special encounters (e.g. 5 ghosts at crystal in the cathedral)
- The Homo Flameus encounters since they are quest-relevant and scripted

If special and rival encounters should also be randomized, the program can be executed using the --randomizeSpecialEncounters and --randomizeRivalEncounters argument

### Special Thanks

https://github.com/cdanek/KaimiraWeightedList
