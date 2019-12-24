# RL Decrypt Extract
This project aims to provide a means for the easy decryption and extraction of encrypted Rocket League UPK files.  

## Reason for creation
When modding the game RocketLeague, we decrypt and extract assets from UPK files in order to use them in the maps that we create. This process was a manual execution of the decryption which takes a linear amount of time depending on file size (with the general expected time to be between 1-10 seconds for RocketLeague asset files), then extracting those files into folders which takes about the same amount of time. Since this would mean waiting for a while then waiting again, I created a binary which decrypts then extracts the asset package in one go.

## Method
The user drags an encyrpted UPK file onto the binary, which then decrypts it and extracts the assets using UModel.

## Tech used
- C#
- Visual Studio CLI

## Definition of done
This project will be defined as completed when it:
- [x] Decrypts the asset package successfully.
- [x] Extracts the asset package and places the output in a child folder.

## Retrospective
- Games change their encryption methods regularly, but not on all files. This causes decryption issues if you don't know which files use what.
- Extraction time sped up a nice amount once this was completed.