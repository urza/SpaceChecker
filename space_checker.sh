#!/bin/bash

# this script is called by crontab (sudo crontab -e)

# where to post data
api="$1"

# get space of the root("/") filesystem and send it to remote api as hostname and percentage of use
# if you do df -H there is one line that has "Mounted on" as "/" - it selects value from this line as the percent, other filesystems/disks are ignored
df -P / | tail -1 | awk '{sub(/%/, ""); print $5}' | xargs -I{} curl -X GET "$api/post/machine/$(hostname)/percent_full/{}"