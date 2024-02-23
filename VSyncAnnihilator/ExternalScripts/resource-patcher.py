#!/usr/bin/env python3

"""
Please make sure you understand what you're doing BEFORE running this script!

This is a script to (un)patch the Musynx's preset for highest quality settings
to disable vSync before the game even starts. Changing this setting at runtime
(in the mod dll itself) may cause lags, so using this script is preferred.

Usage: point this at MUSYNX_Data/globalgamemanagers file, where the presets are defined.
Note that the target file has no extension.

Run the script again on a patched file to revert the changes.

Having a backup of the file is recommended.
Steam's "Verify integrity of game files" feature may also help if things break. 

The script is not very sophisticated, and the safety checks are very simple.
Please refrain from running it on other files.

Part of bnfour's mx-mods project, more info at:
https://github.com/bnfour/mx-mods
"""

import argparse
import mmap
import sys

# it's important these two have the same length
original_caption = b"Extreme Quality (V-Sync ON)"
patched_caption =  b"(NO V-Sync) Extreme Quality"

int32_zero = b"\x00\x00\x00\x00"
int32_one = b"\x01\x00\x00\x00"

# this is offset from the start of the caption text to the start of 4 bit int32
# which contains vSyncCount value
# most of the stuff in between are 4 bit int32s or floats,
# but there's 4 single bytes (or bools?) directly before the vSyncCount
# other thing to note is that the caption includes \0 at the end
vsynccount_offset = 96
# if you'd like to know more, you can use AssetRipper (or something like it)
# to see for yourself

def get_args():
    parser = argparse.ArgumentParser(description="Companion script for the VSync annihilator mod for Musynx. Patches the quality settings preset to disable vSync before the game is launched for allegedly better performance.",
        epilog="Please please make sure you read the docs and understand what you're doing.")
    
    parser.add_argument("resfile", type=argparse.FileType("rb+"),
        help="path to MUSYNX_Data/globalgamemanagers file to patch data in. Reverts already patched file to default")
    
    return parser.parse_args()

def main(args):
    file = mmap.mmap(args.resfile.fileno(), 0)
    args.resfile.close()

    vanilla_offset = file.find(original_caption, 0)
    if vanilla_offset != -1:
        print("Found vanilla data, applying the patch...")
        
        file[vanilla_offset:vanilla_offset + len(original_caption)] = patched_caption
        
        full_offset = vanilla_offset + vsynccount_offset
        file[full_offset:full_offset + 4] = int32_zero
    else:
        patched_offset = file.find(patched_caption, 0)
        if patched_offset != -1:
            print("Found already patched data, REMOVING the patch...")
            
            file[patched_offset:patched_offset + len(patched_caption)] = original_caption
            
            full_offset = patched_offset + vsynccount_offset
            file[full_offset:full_offset + 4] = int32_one
        else:
            print("Could not locate data to patch. Is the file correct?")
            file.close()
            sys.exit(1)

    print("OK! ('-^)b")
    file.close()

if __name__ == "__main__":
    main(get_args())
