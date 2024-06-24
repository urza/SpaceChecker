#!/bin/bash

# This script creates cron job (as root) that executes space_checker.sh periodically

script_dir="$(pwd)"

# Define the path to the space_checker.sh script
script_path="$script_dir/space_checker.sh"

# Download the space_checker.sh script
sudo curl -sSL "https://raw.githubusercontent.com/urza/SpaceChecker/main/space_checker.sh" -o "$script_path"

# Make the space_checker.sh script executable
sudo chmod +x "$script_path"

# Get the API URL from command-line argument
api_url="$1"

# Define the cron job entry
cron_entry="* * * * * $script_path $api_url"

# Add the cron job to the root user's crontab
(sudo crontab -l 2>/dev/null; echo "$cron_entry") | sudo crontab -

echo "Cron job has been successfully created to run $script_path every minute as root."